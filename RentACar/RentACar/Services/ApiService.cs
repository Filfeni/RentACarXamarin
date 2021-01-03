using Newtonsoft.Json;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RentACar.Services
{
    public class ApiService : IApiService
    {
        public string BaseUrl { get; set; }
        public ApiService()
        {
            
        }
        // AUTHENTICATE
        
        public async Task<ObservableCollection<Brand>> GetBrandAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> LoginUser(UserLogin user)
        {
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.LoginRoute }), content);
            return response;
        }

        public async Task<HttpResponseMessage> RegisterUser(UserRegister user)
        {
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.LoginRoute }), content);
            return response;
        }

        // CATALOG
        // MYCARS
        // RESERVATIONS
        // CONSTANTS
    }
}
