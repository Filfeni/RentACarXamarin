using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using RentACar.Models;
using RentACar;
using System.Net.Http;
using Refit;

namespace RentACar.Services
{
    public interface IApiService
    {
        // AUTHENTICATE
        [Post(Config.LoginRoute)]
        Task<HttpResponseMessage> LoginUser(UserLogin user);

        [Post(Config.RegisterRoute)]
        Task<HttpResponseMessage> RegisterUser(UserRegister user);
        // CATALOG

        // MYCARS
        // RESERVATIONS
        // CONSTANTS
        [Get(Config.BrandsRoute)]
        Task<ObservableCollection<Brand>> GetBrandAsync();
    }
}
