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
        public IApiService ApiService { get; set; }
        public IPageDialogService DialogService { get; set; }

        public DelegateCommand LogInCommand { get; private set; }
        public DelegateCommand SignUpCommand { get; private set; }
        public Jwt CurrentJwt { get; set; }
        public UserLogin User { get; set; }
        public HttpResponseMessage LogInResponse { get; set; }
        public HttpResponseMessage UserIdResponse { get; set; }
        public LogInPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
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
            LogInResponse = await ApiService.LoginUserAsync(User);
            if (LogInResponse.IsSuccessStatusCode)
            {
                
                string responseContent = await LogInResponse.Content.ReadAsStringAsync();
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
    }
}
