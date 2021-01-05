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
        public const string LoginRoute = "/api/authenticate/login";
        public const string RegisterRoute = "/api/authenticate/register";
        public const string MyCarsRoute = "/api/mycars";



        //NAVIGATION
        public const string HomePage = "HomeTabs";
        public const string AddCarPage = "AddCar";
        public const string ExplorePage = "Explore";
        public const string LogInPage = "Login";
        public const string MorePage = "More";
        public const string RegisterPage = "Register";
        public const string RentPage = "Rent";

        public const string HomeTabbedPageNavigation = "/NavigationPage/HomeTabs";
        public const string NewCarNavigation = "/NavigationPage/HomeTabs/NewCar";
        public const string LoginNavigation = "/NavigationPage/Login";
        public const string RegisterNavigation = "/NavigationPage/Login/Register";
    }
}
