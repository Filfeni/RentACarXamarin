using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar
{
    public class Config
    {
        //API
        public const string BaseApiUrl = "https://xamrentapi.azurewebsites.net";
        public const string BrandsApiUrl = "/api/const/brands";


        //NAVIGATION
        public const string HomePage = "HomeTabs";
        public const string AddCarPage = "AddCar";
        public const string ExplorePage = "Explore";
        public const string LogInPage = "Login";
        public const string MorePage = "More";
        public const string RegisterPage = "Register";
        public const string RentPage = "Rent";

        public const string HomeTabbedPageNavigation = "/HomeTabs";
        public const string LoginNavigation = "/Login";
        public const string RegisterNavigation = "/Login/NavigationPage/Register";
    }
}
