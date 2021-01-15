using Prism.Commands;
using Prism.Navigation;
using RentACar.Services;

namespace RentACar.ViewModels
{
    public class MorePageViewModel : BaseViewModel
    {
        public IAuthService AuthService { get; set; }
        public DelegateCommand SignOutCommand { get; set; }
        public MorePageViewModel(IAuthService authService, INavigationService navigationService) : base(navigationService)
        {
            AuthService = authService;
            SignOutCommand = new DelegateCommand(SignOut);
        }

        public void SignOut()
        {
            AuthService.SignOut();
        }
    }
}
