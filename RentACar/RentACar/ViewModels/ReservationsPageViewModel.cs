using Newtonsoft.Json;
using Prism.Navigation;
using Prism.Services;
using RentACar.Models;
using RentACar.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;

namespace RentACar.ViewModels
{
    public class ReservationsPageViewModel : BaseViewModel, IInitialize
    {
        public INavigationService NavigationService;
        public IApiService ApiService;
        public IPageDialogService DialogService;
        public IAuthService AuthorizationService;
        public ObservableCollection<Reservation> ReservationList { get; set; }
        public ReservationsPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService, IAuthService authorizationService)
        {
            NavigationService = navigationService;
            ApiService = apiService;
            DialogService = dialogService;
            AuthorizationService = authorizationService;
        }
        public async void Initialize(INavigationParameters parameters)
        {
            bool isAuthorized = await AuthorizationService.Authorize();
            if (!isAuthorized)
            {
                return;
            }
            HttpResponseMessage reservationsRequest = await ApiService.GetMyReservations();
            if (reservationsRequest.IsSuccessStatusCode || reservationsRequest.StatusCode == HttpStatusCode.NotFound)
            {
                string responseContent = await reservationsRequest.Content.ReadAsStringAsync();
                ReservationList = JsonConvert.DeserializeObject<ObservableCollection<Reservation>>(responseContent);
            }
            else
            {
                await DialogService.DisplayAlertAsync("Error", "There was a connection error, Try again later", "OK");
            }
        }
    }
}
