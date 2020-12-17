using Prism;
using Prism.Ioc;
using Prism.Unity;
using RentACar.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentACar
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomeTabbedPage>("HomeTabs");
            containerRegistry.RegisterForNavigation<AddCarPage>("AddCar");
            containerRegistry.RegisterForNavigation<ExplorePage>("Explore");
            containerRegistry.RegisterForNavigation<LogInPage>("Login");
            containerRegistry.RegisterForNavigation<MorePage>("More");
            containerRegistry.RegisterForNavigation<RegisterPage>("Register");
            containerRegistry.RegisterForNavigation<RentPage>("Rent");
        }

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync(Config.LoginNavigation);
        }
    }
}
