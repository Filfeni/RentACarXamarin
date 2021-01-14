using Prism.Commands;
using RentACar.Services;

namespace RentACar.ViewModels
{
    public class MorePageViewModel : BaseViewModel
    {
        IAuthService AuthService;
        public DelegateCommand SignOutCommand { get; set; }
        public MorePageViewModel(IAuthService authService)
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
