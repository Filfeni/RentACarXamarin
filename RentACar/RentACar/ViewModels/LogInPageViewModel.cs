using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RentACar.Models;
using RentACar.Services;
using System.Net.Http;
using Xamarin.Essentials;

namespace RentACar.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        public INavigationService NavigationService;
        public IApiService ApiService;
        public IPageDialogService DialogService;

        public DelegateCommand LogInCommand { get; private set; }
        public DelegateCommand SignUpCommand { get; private set; }
        public Jwt CurrentJwt { get; set; }
        public UserLogin User { get; set; }
        public HttpResponseMessage LogInRequest { get; set; }
        public LogInPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            ApiService = apiService;
            DialogService = dialogService; 
            LogInCommand = new DelegateCommand(LogIn);
            SignUpCommand = new DelegateCommand(SignUp);
            User = new UserLogin();
        }

        

        public async void LogIn()
        {
            LogInRequest = await ApiService.LoginUser(User);
            if (LogInRequest.IsSuccessStatusCode)
            {
                string responseContent = await LogInRequest.Content.ReadAsStringAsync();
                CurrentJwt = JsonConvert.DeserializeObject<Jwt>(responseContent);
                await SecureStorage.SetAsync(nameof(CurrentJwt.Token), CurrentJwt.Token);
                await SecureStorage.SetAsync(nameof(CurrentJwt.Expires), CurrentJwt.Expires.ToString());
                await NavigationService.NavigateAsync(Config.HomeTabbedPageNavigation);
            }
            else
            {
                await DialogService.DisplayAlertAsync("Error", "Log In Failed, Try again later", "OK");
            }
        }

        public async void SignUp()
        {
            await NavigationService.NavigateAsync(Config.RegisterNavigation);
        }

        public void Initialize(INavigationParameters parameters)
        {
            
        }
    }
}
