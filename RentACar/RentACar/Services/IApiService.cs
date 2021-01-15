using System.Threading.Tasks;
using RentACar.Models;
using System.Net.Http;

namespace RentACar.Services
{
    public interface IApiService
    {
        // AUTHENTICATE
        Task<HttpResponseMessage> LoginUserAsync(UserLogin user);
        Task<HttpResponseMessage> RegisterUserAsync(UserRegister user);
        Task<HttpResponseMessage> GetUserIdAsync();
        // CATALOG
        Task<HttpResponseMessage> SearchCatalogAsync(string query);
        // MYCARS
        Task<HttpResponseMessage> GetMyCarsAsync();
        Task<HttpResponseMessage> PostCarAsync(Car car);
        Task<HttpResponseMessage> DeleteMyCarAsync(int id);
        Task<HttpResponseMessage> CreatePostAsync(Post post);
        Task<HttpResponseMessage> DeletePostAsync(int id);

        // RESERVATIONS
        Task<HttpResponseMessage> GetMyReservationsAsync();
        Task<HttpResponseMessage> GetReservationDatesAsync(int carid);
        Task<HttpResponseMessage> PostReservationAsync(Reservation reservation);

        // CONSTANTS
        Task<HttpResponseMessage> GetBrandsAsync();
        Task<HttpResponseMessage> GetReservationTypesAsync();
        Task<HttpResponseMessage> GetFuelTypesAsync();
        Task<HttpResponseMessage> GetCategoriesAsync();
        
    }
}
