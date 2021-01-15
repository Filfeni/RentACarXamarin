using Prism.Navigation;
using RentACar.Services;

namespace RentACar.ViewModels
{
    public class HomeTabbedPageViewModel : BaseViewModel, INavigatedAware
    {
        public IAuthService AuthService { get; set; }
        public HomeTabbedPageViewModel(INavigationService navigationService, IAuthService authService) : base(navigationService)
        {
            NavigationService = navigationService;
            AuthService = authService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            AuthService.Authorize();
        }
    }
}
