using Prism.Navigation;
using Prism.Services;
using RentACar.Models;
using RentACar.Services;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Prism.Commands;

namespace RentACar.ViewModels
{
    public class NewCarPageViewModel : BaseViewModel, IInitialize
    {
        public IApiService ApiService { get; set; }
        public IPageDialogService DialogService { get; set; }
        public IAuthService AuthorizationService { get; set; }
        public Car NewCar { get; set; }
        public Brand SelectedBrand { get; set; }
        public Category SelectedCategory { get; set; }
        public FuelType SelectedFuelType { get; set; }
        public ObservableCollection<Brand> BrandList { get; set; }
        public ObservableCollection<Category> CategoryList { get; set; }
        public ObservableCollection<FuelType> FuelTypeList { get; set; }
        public UserIdResponse CurrentUserId { get; set; }
        public DelegateCommand AddCarCommand { get; set; }

        public NewCarPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService, IAuthService authorizationService) : base(navigationService)
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
            bool isAuthorized = await AuthorizationService.Authorize();
            if (!isAuthorized)
            {
                return;
            }
            var useridTask = ApiService.GetUserIdAsync();
            var brandsTask = ApiService.GetBrandsAsync();
            var categoriesTask = ApiService.GetCategoriesAsync();
            var fuelTask = ApiService.GetFuelTypesAsync();
            
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
            bool isAuthorized = await AuthorizationService.Authorize();
            if (!isAuthorized)
            {
                return;
            }
            NewCar.OwnerId = CurrentUserId.UserId;
            NewCar.BrandId = SelectedBrand.Id;
            NewCar.CategoryId = SelectedCategory.Id;
            NewCar.FuelTypeId = SelectedFuelType.Id;
            HttpResponseMessage registerCarResponse = await ApiService.PostCarAsync(NewCar);
            if (registerCarResponse.IsSuccessStatusCode)
            {
                string responseContent = await registerCarResponse.Content.ReadAsStringAsync();
                Response submitResponse = JsonConvert.DeserializeObject<Response>(responseContent);
                await DialogService.DisplayAlertAsync($"{submitResponse.Status}", $"{submitResponse.Message}", "OK");
                await NavigationService.NavigateAsync(Config.HomeTabbedPageNavigation);
            }
            else
            {
                string responseContent = await registerCarResponse.Content.ReadAsStringAsync();
                Response submitResponse = JsonConvert.DeserializeObject<Response>(responseContent);
                await DialogService.DisplayAlertAsync($"{submitResponse.Status}", $"{submitResponse.Message}", "OK");
            }
        }
    }
}
