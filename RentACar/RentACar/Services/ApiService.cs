using Newtonsoft.Json;
using RentACar.Models;
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

        public async Task<HttpResponseMessage> CreatePost(Post post)
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            StringContent content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.MyCarsRoute, "/", post.CarId.ToString(), "/post" }), content);
            return response;
        }

        public async Task<HttpResponseMessage> DeletePost(int id)
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.DeleteAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.MyCarsRoute, "/", id.ToString(), "/post" }));
            return response;
        }

        // CATALOG
        // api/catalog/search?search=
        public async Task<HttpResponseMessage> SearchCatalog(string query)
        {
            query = query.Replace(" ", "%20");
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            string route = string.Join("", new string[] { Config.BaseApiUrl, Config.CatalogRoute, "/search?search=", query });
            var response = await client.GetAsync(route);
            return response;
        }

        // RESERVATIONS
        public async Task<HttpResponseMessage> GetMyReservations()
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.GetAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.ReservationsRoute }));
            return response;
        }
        public async Task<HttpResponseMessage> GetReservationDates(int carid)
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.GetAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.ReservationsRoute,"/", carid.ToString(), "/reservation-dates" }));
            return response;
        }

        public async Task<HttpResponseMessage> PostReservation(Reservation reservation)
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.ReservationsRoute}), content);
            return response;
        }
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

        public async Task<HttpResponseMessage> GetReservationTypes()
        {
            HttpClient client = new HttpClient();
            string token = await SecureStorage.GetAsync("Token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.GetAsync(string.Join("", new string[] { Config.BaseApiUrl, Config.ReservationTypesRoute }));
            return response;
        }
    }
}
