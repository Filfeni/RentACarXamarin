using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RentACar.Models;
using RentACar.Services;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace RentACar.ViewModels
{
    public class MyCarsPageViewModel : BaseViewModel, IInitialize
    {
        public INavigationService NavigationService;
        public IApiService ApiService;
        public IPageDialogService DialogService;
        public IAuthService AuthorizationService;
        public Car SelectedCar { get; set; }
        public Response Response { get; set; }
        public ObservableCollection<Car> CarList { get; set; }
        public DelegateCommand<Car> DeleteCarCommand { get; private set; }
        public DelegateCommand AddCarCommand { get; private set; }
        public DelegateCommand<Car> CarDetailsCommand { get; private set; }
        public DelegateCommand<Car> CreatePostCommand { get; private set; }
        public DelegateCommand<Car> DeletePostCommand { get; private set; }
        public MyCarsPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService, IAuthService authorizationService)
        {
            NavigationService = navigationService;
            ApiService = apiService;
            DialogService = dialogService;
            AuthorizationService = authorizationService;
            CarList = new ObservableCollection<Car>();
            DeleteCarCommand = new DelegateCommand<Car>(DeleteCar);
            CarDetailsCommand = new DelegateCommand<Car>(CarDetails);
            CreatePostCommand = new DelegateCommand<Car>(CreatePost);
            DeletePostCommand = new DelegateCommand<Car>(DeletePost);
        }

        public async void AddCar()
        {
            await NavigationService.NavigateAsync(Config.NewCarPage);
        }

        public async void DeleteCar(Car car)
        {
            bool confirmed = await DialogService.DisplayAlertAsync("Alert", "Are you sure you want to delete this car?", "Yes", "No");
            if (!confirmed)
            {
                return;
            }
            await AuthorizationService.Authorize();
            HttpResponseMessage carsRequest = await ApiService.DeleteMyCar(car.CarId);
            if (carsRequest.IsSuccessStatusCode)
            {
                string responseContent = await carsRequest.Content.ReadAsStringAsync();
                Response = JsonConvert.DeserializeObject<Response>(responseContent);
                await DialogService.DisplayAlertAsync($"{Response.Status}", $"{Response.Message}", "OK");
                Refresh();
            }
            else
            {
                await DialogService.DisplayAlertAsync("Error", "Could not delete car", "OK");
            }
        }

        public async void CreatePost(Car car)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(nameof(car.CarId),car.CarId);
            await NavigationService.NavigateAsync(Config.NewPostPage, parameters);
        }

        public async void DeletePost(Car car)
        {
            bool confirmed = await DialogService.DisplayAlertAsync("Alert", "Are you sure you want to delete this car's post?", "Yes", "No");
            if (!confirmed)
            {
                return;
            }
            await AuthorizationService.Authorize();
            HttpResponseMessage postResponse = await ApiService.DeletePost(car.CarId);
            if (postResponse.IsSuccessStatusCode)
            {
                string responseContent = await postResponse.Content.ReadAsStringAsync();
                Response = JsonConvert.DeserializeObject<Response>(responseContent);
                await DialogService.DisplayAlertAsync($"{Response.Status}", $"{Response.Message}", "OK");
                Refresh();
            }
            else
            {
                await DialogService.DisplayAlertAsync("Error", "Could not delete Post", "OK");
            }
        }
        public async void CarDetails(Car car)
        {
            string transmission = car.Transmission != true ? "Manual" : "Automatic";
            await DialogService.DisplayAlertAsync("Car Details", $"Model : {car.Model}\nModel : {car.Year}\nLicense plate : {car.LicensePlate}\nColor : {car.Color}\nTransmission type : {transmission}", "OK");
        }

        public async void Initialize(INavigationParameters parameters)
        {
            bool isAuthorized = await AuthorizationService.Authorize();
            if (!isAuthorized)
            {
                return;
            }
            HttpResponseMessage carsRequest = await ApiService.GetMyCars();
            if (carsRequest.IsSuccessStatusCode)
            {
                string responseContent = await carsRequest.Content.ReadAsStringAsync();
                CarList = JsonConvert.DeserializeObject<ObservableCollection<Car>>(responseContent);
            }
            else
            {
                await DialogService.DisplayAlertAsync("Error", "There was a connection error, Try again later", "OK");
            }
        }
        public async void Refresh()
        {
            bool isAuthorized = await AuthorizationService.Authorize();
            if (!isAuthorized)
            {
                return;
            }
            HttpResponseMessage carsRequest = await ApiService.GetMyCars();
            if (carsRequest.IsSuccessStatusCode)
            {
                string responseContent = await carsRequest.Content.ReadAsStringAsync();
                CarList = JsonConvert.DeserializeObject<ObservableCollection<Car>>(responseContent);
            }
            else
            {
                await DialogService.DisplayAlertAsync("Error", "There was a connection error, Try again later", "OK");
            }
        }
    }
}