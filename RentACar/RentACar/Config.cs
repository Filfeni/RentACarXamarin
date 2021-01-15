using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar
{
    public class Config
    {
        //API
        public const string BaseApiUrl = "https://xamrentapi.azurewebsites.net";
        public const string BrandsRoute = "/api/const/brands";
        public const string CategoriesRoute = "/api/const/categories";
        public const string FuelTypesRoute = "/api/const/fueltype";
        public const string ReservationTypesRoute = "/api/const/reservationtype";
        public const string LoginRoute = "/api/authenticate/login";
        public const string RegisterRoute = "/api/authenticate/register";
        public const string MyCarsRoute = "/api/mycars";
        public const string CatalogRoute = "/api/catalog";
        public const string ReservationsRoute = "/api/reservations";
        public const string UserIdRoute = "/api/const/get-current-userid";



        //NAVIGATION
        public const string HomePage = "HomeTabs";
        public const string NewCarPage = "NewCar";
        public const string RentPage = "Rent";
        public const string NewPostPage = "NewPost";
        public const string MyCarsPage = "MyCars";
        public const string CatalogPage = "Catalog";
        public const string SearchPage = "Search";
        public const string ExplorePage = "Explore";
        public const string LogInPage = "Login";
        public const string MorePage = "More";
        public const string RegisterPage = "Register";
        public const string ReservationsPage = "Reservations";

        public const string HomeTabbedPageNavigation = "/HomeTabs";
        public const string NewCarNavigation = "/HomeTabs/NewCar";
        public const string LoginNavigation = "/NavigationPage/Login";
        public const string RegisterNavigation = "/NavigationPage/Login/Register";

        
    }
}
