using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using RentACar.Models;
using RentACar;
using System.Net.Http;

namespace RentACar.Services
{
    public interface IApiService
    {
        // AUTHENTICATE
        Task<HttpResponseMessage> LoginUser(UserLogin user);
        Task<HttpResponseMessage> RegisterUser(UserRegister user);
        Task<HttpResponseMessage> GetUserId();
        // CATALOG
        Task<HttpResponseMessage> SearchCatalog(string query);
        // MYCARS
        Task<HttpResponseMessage> GetMyCars();
        Task<HttpResponseMessage> PostCar(Car car);
        Task<HttpResponseMessage> DeleteMyCar(int id);
        Task<HttpResponseMessage> CreatePost(Post post);
        Task<HttpResponseMessage> DeletePost(int id);

        // RESERVATIONS
        Task<HttpResponseMessage> GetMyReservations();
        Task<HttpResponseMessage> GetReservationDates(int carid);
        Task<HttpResponseMessage> PostReservation(Reservation reservation);

        // CONSTANTS
        Task<HttpResponseMessage> GetBrands();
        Task<HttpResponseMessage> GetReservationTypes();
        Task<HttpResponseMessage> GetFuelTypes();
        Task<HttpResponseMessage> GetCategories();
        
    }
}
