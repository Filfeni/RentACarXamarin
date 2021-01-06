using Newtonsoft.Json;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
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
            var response = await client.PostAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.RegisterRoute }), content);
            return response;
        }

        // MYCARS
        public async Task<HttpResponseMessage> GetMyCars()
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.GetAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.MyCarsRoute }));
            return response;
        }

        public async Task<HttpResponseMessage> PostCar(Car car)
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            StringContent content = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.MyCarsRoute}), content);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteMyCar(int id)
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.DeleteAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.MyCarsRoute,"/",id.ToString()}));
            return response;
        }


        // CATALOG

        // RESERVATIONS

        // CONSTANTS
        public async Task<HttpResponseMessage> GetBrands()
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.GetAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.BrandsRoute }));
            return response;
        }

        public async Task<HttpResponseMessage> GetFuelTypes()
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.GetAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.FuelTypesRoute }));
            return response;
        }

        public async Task<HttpResponseMessage> GetCategories()
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.GetAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.CategoriesRoute }));
            return response;
        }
        public async Task<HttpResponseMessage> GetUserId()
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.GetAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.UserIdRoute }));
            return response;
        }
    }
}
