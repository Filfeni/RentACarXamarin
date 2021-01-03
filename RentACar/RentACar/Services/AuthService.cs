using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RentACar.Services
{
    public class AuthService : IAuthService
    {
        public INavigationService NavigationService { get; set; }
        public bool IsAuthorized { get; set; }
        public AuthService(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public async Task Authorize()
        {
            try
            {
                string token = await SecureStorage.GetAsync("Token");
                string expires = await SecureStorage.GetAsync("Expires");
                if (string.IsNullOrEmpty(token) || DateTime.Parse(expires) <= DateTime.Now)
                {
                    await NavigationService.NavigateAsync(Config.LoginNavigation);
                }

            }
            catch (Exception)
            {
                await NavigationService.NavigateAsync(Config.LoginNavigation);
            }
            
        }

        public void SignOut()
        {
            SecureStorage.Remove("Token");
            SecureStorage.Remove("Expires");
        }
    }
}
