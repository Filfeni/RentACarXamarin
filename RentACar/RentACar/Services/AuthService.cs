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
        //public async Task<bool> IsAuthorized()
        //{
        //    string token = await SecureStorage.GetAsync("Token");
        //    string expires = await SecureStorage.GetAsync("Expires");
        //    bool result = string.IsNullOrEmpty(token) ? false : (DateTime.Parse(expires) > DateTime.UtcNow);
        //    return result;
        //}
        public AuthService(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public async Task<bool> Authorize()
        {
            try
            {
                string token = await SecureStorage.GetAsync("Token");
                string expires = await SecureStorage.GetAsync("Expires");
                if (string.IsNullOrEmpty(token))
                {
                    await NavigationService.NavigateAsync(Config.LoginNavigation);
                    return false;
                }
                if (DateTime.Parse(expires) <= DateTime.UtcNow)
                {
                    await NavigationService.NavigateAsync(Config.LoginNavigation);
                    return false;
                }

            }
            catch (Exception)
            {
                await NavigationService.NavigateAsync(Config.LoginNavigation);
            }
            return true;
        }

        public async Task SignOut()
        {
            SecureStorage.RemoveAll();
            await NavigationService.NavigateAsync(Config.LoginNavigation);
        }
    }
}
