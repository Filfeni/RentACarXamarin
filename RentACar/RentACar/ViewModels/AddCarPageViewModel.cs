using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RentACar.Models;
using RentACar.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace RentACar.ViewModels
{
    public class AddCarPageViewModel : BaseViewModel, INavigatedAware, IInitialize
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
        public HttpResponseMessage CarsRequest { get; set; }
        public AddCarPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService, IAuthService authorizationService)
        {
            NavigationService = navigationService;
            ApiService = apiService;
            DialogService = dialogService;
            AuthorizationService = authorizationService;
            CarList = new ObservableCollection<Car>();
            DeleteCarCommand = new DelegateCommand<Car>(DeleteCar);
            CarDetailsCommand = new DelegateCommand<Car>(CarDetails);
            AddCarCommand = new DelegateCommand(AddCar);
        }

        public async void AddCar()
        {
            await NavigationService.NavigateAsync(Config.NewCarNavigation);
        }

        public async void DeleteCar(Car car)
        {
            bool confirmed = await DialogService.DisplayAlertAsync("Alert", "Are you sure you want to delete this car?", "Yes", "No");
            if (!confirmed)
            {
                return;
            }
            await AuthorizationService.Authorize();
            CarsRequest = await ApiService.DeleteMyCar(car.CarId);
            if (CarsRequest.IsSuccessStatusCode)
            {
                string responseContent = await CarsRequest.Content.ReadAsStringAsync();
                Response = JsonConvert.DeserializeObject<Response>(responseContent);
                await DialogService.DisplayAlertAsync($"{Response.Status}",$"{Response.Message}", "OK");
                Refresh();
            }
            else
            {
                await DialogService.DisplayAlertAsync("Error", "Could not delete car", "OK");
            }
        }
        public async void CarDetails(Car car)
        {
            string transmission = car.Transmission != true ? "Automatic" : "Manual";
            await DialogService.DisplayAlertAsync("Car Details", $"Model : {car.Model}\nModel : {car.Year}\nLicense plate : {car.LicensePlate}\nColor : {car.Color}\nTransmission type : {transmission}", "OK");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }

        public async void Initialize(INavigationParameters parameters)
        {
            bool isAuthorized = await AuthorizationService.Authorize();
            if (!isAuthorized)
            {
                return;
            }
            CarsRequest = await ApiService.GetMyCars();
            if (CarsRequest.IsSuccessStatusCode)
            {
                string responseContent = await CarsRequest.Content.ReadAsStringAsync();
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
            CarsRequest = await ApiService.GetMyCars();
            if (CarsRequest.IsSuccessStatusCode)
            {
                string responseContent = await CarsRequest.Content.ReadAsStringAsync();
                CarList = JsonConvert.DeserializeObject<ObservableCollection<Car>>(responseContent);
            }
            else
            {
                await DialogService.DisplayAlertAsync("Error", "There was a connection error, Try again later", "OK");
            }
        }
    }
}
