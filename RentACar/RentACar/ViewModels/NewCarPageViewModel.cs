using Prism.Navigation;
using Prism.Services;
using RentACar.Models;
using RentACar.Services;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Prism.Commands;

namespace RentACar.ViewModels
{
    public class NewCarPageViewModel : BaseViewModel, IInitialize
    {
        public INavigationService NavigationService;
        public IApiService ApiService;
        public IPageDialogService DialogService;
        public IAuthService AuthorizationService;
        public Car NewCar { get; set; }
        public Brand SelectedBrand { get; set; }
        public Category SelectedCategory { get; set; }
        public FuelType SelectedFuelType { get; set; }
        public ObservableCollection<Brand> BrandList { get; set; }
        public ObservableCollection<Category> CategoryList { get; set; }
        public ObservableCollection<FuelType> FuelTypeList { get; set; }
        public UserIdResponse CurrentUserId { get; set; }
        public DelegateCommand AddCarCommand { get; set; }
        public Response SubmitResponse { get; private set; }

        public NewCarPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService, IAuthService authorizationService)
        {
            NavigationService = navigationService;
            ApiService = apiService;
            DialogService = dialogService;
            AuthorizationService = authorizationService;
            AddCarCommand = new DelegateCommand(AddCar);
            NewCar = new Car();
        }

        public async void Initialize(INavigationParameters parameters)
        {
            await AuthorizationService.Authorize();
            var useridTask = ApiService.GetUserId();
            var brandsTask = ApiService.GetBrands();
            var categoriesTask = ApiService.GetCategories();
            var fuelTask = ApiService.GetFuelTypes();
            
            await Task.WhenAll(brandsTask, fuelTask, categoriesTask, useridTask);
            HashSet<HttpResponseMessage> list = 
                new HashSet<HttpResponseMessage>() { brandsTask.Result, categoriesTask.Result, fuelTask.Result, useridTask.Result};
            if (list.Any(t => !t.IsSuccessStatusCode))
            {
                await DialogService.DisplayAlertAsync("Error", "Failed to load data", "Ok");
                await NavigationService.GoBackAsync();
            } else
            {
                string brandsjson = await brandsTask.Result.Content.ReadAsStringAsync();
                string categoriesjson = await categoriesTask.Result.Content.ReadAsStringAsync();
                string fueltypesjson = await fuelTask.Result.Content.ReadAsStringAsync();
                string useridjson = await useridTask.Result.Content.ReadAsStringAsync();
                BrandList = JsonConvert.DeserializeObject<ObservableCollection<Brand>>(brandsjson);
                CategoryList = JsonConvert.DeserializeObject<ObservableCollection<Category>>(categoriesjson);
                FuelTypeList = JsonConvert.DeserializeObject<ObservableCollection<FuelType>>(fueltypesjson);
                CurrentUserId = JsonConvert.DeserializeObject<UserIdResponse>(useridjson);
            }

        }

        public async void AddCar()
        {
            NewCar.OwnerId = CurrentUserId.UserId;
            NewCar.BrandId = SelectedBrand.Id;
            NewCar.CategoryId = SelectedCategory.Id;
            NewCar.FuelTypeId = SelectedFuelType.Id;
            HttpResponseMessage registerCarResponse = await ApiService.PostCar(NewCar);
            if (registerCarResponse.IsSuccessStatusCode)
            {
                string responseContent = await registerCarResponse.Content.ReadAsStringAsync();
                SubmitResponse = JsonConvert.DeserializeObject<Response>(responseContent);
                await DialogService.DisplayAlertAsync($"{SubmitResponse.Status}", $"{SubmitResponse.Message}", "OK");
                await NavigationService.NavigateAsync(Config.HomeTabbedPageNavigation);
            }
            else
            {
                string responseContent = await registerCarResponse.Content.ReadAsStringAsync();
                SubmitResponse = JsonConvert.DeserializeObject<Response>(responseContent);
                await DialogService.DisplayAlertAsync($"{SubmitResponse.Status}", $"{SubmitResponse.Message}", "OK");
            }
        }
    }
}
