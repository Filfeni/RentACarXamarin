using Prism.Commands;
using RentACar.Services;
using System;
using System.Collections.Generic;
using System.Text;

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
