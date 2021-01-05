using Prism.Navigation;
using RentACar.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace RentACar.ViewModels
{
    public class HomeTabbedPageViewModel : BaseViewModel, INavigatedAware
    {
        public INavigationService NavigationService;
        public IAuthService AuthService;
        public HomeTabbedPageViewModel(INavigationService navigationService, IAuthService authService)
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
