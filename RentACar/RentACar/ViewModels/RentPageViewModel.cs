using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RentACar.Models;
using RentACar.Services;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentACar.ViewModels
{
    public class RentPageViewModel : BaseViewModel, IInitialize
    {
        public INavigationService NavigationService;
        public IApiService ApiService;
        public IPageDialogService DialogService;
        public IAuthService AuthorizationService;
        public Post Post { get; set; }
        public DateTime MaxDate { get { return DateTime.Now.Date.AddMonths(2); } }
        public DateTime MinDate { get { return DateTime.Now.Date; } }
        public ObservableCollection<DateTime> BlackoutDates { get; set; }
        public SelectionRange SelectedRange { get; set; }
        public ObservableCollection<ReservationType> ReservationTypeList { get; set; }
        public DelegateCommand RentCommand { get; set; }
        public Reservation NewReservation { get; set; }
        public ReservationType SelectedReservationType { get; set; }
        public RentPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService, IAuthService authorizationService)
        {
            NavigationService = navigationService;
            ApiService = apiService;
            DialogService = dialogService;
            AuthorizationService = authorizationService;
            BlackoutDates = new ObservableCollection<DateTime>();
            ReservationTypeList = new ObservableCollection<ReservationType>();
            NewReservation = new Reservation();
            Post = new Post();
            SelectedReservationType = new ReservationType();
            RentCommand = new DelegateCommand(Rent);
        }

        public async void Initialize(INavigationParameters parameters)
        {
            bool isAuthorized = await AuthorizationService.Authorize();
            if (!isAuthorized)
            {
                return;
            }
            Post = parameters.GetValue<Post>("post");
            var useridTask = ApiService.GetUserId();
            var datesTask = ApiService.GetReservationDates(Post.CarId);
            var reserveTypeTask = ApiService.GetReservationTypes();
            
            await Task.WhenAll(useridTask, datesTask, reserveTypeTask);
            HashSet<HttpResponseMessage> list =
                new HashSet<HttpResponseMessage>() { useridTask.Result, datesTask.Result, reserveTypeTask.Result };
            if (list.Any(t => !t.IsSuccessStatusCode))
            {
                await DialogService.DisplayAlertAsync("Error", "Failed to load data", "Ok");
                await NavigationService.GoBackAsync();
            }
            else
            {
                string useridJson = await useridTask.Result.Content.ReadAsStringAsync();
                string datesJson = await datesTask.Result.Content.ReadAsStringAsync();
                string reserveTypesJson = await reserveTypeTask.Result.Content.ReadAsStringAsync();
                BlackoutDates = JsonConvert.DeserializeObject<ObservableCollection<DateTime>>(datesJson);
                ReservationTypeList = JsonConvert.DeserializeObject<ObservableCollection<ReservationType>>(reserveTypesJson);
                UserIdResponse userIdResponse = JsonConvert.DeserializeObject<UserIdResponse>(useridJson);
                NewReservation.ClientId = userIdResponse.UserId;
                NewReservation.CarId = Post.CarId;
            }

        }

        public async void Rent()
        {
            bool confirmed = await DialogService.DisplayAlertAsync("Alert", "Are you sure you want to rent this car?", "Yes", "No");
            if (!confirmed)
            {
                return;
            }
            NewReservation.ReservationTypeId = SelectedReservationType.Id;
            NewReservation.StartDate = SelectedRange.StartDate;
            NewReservation.EndDate = SelectedRange.EndDate;
            HttpResponseMessage registerCarResponse = await ApiService.PostReservation(NewReservation);
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
