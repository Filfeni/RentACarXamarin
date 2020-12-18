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
            containerRegistry.RegisterForNavigation<HomeTabbedPage>(Config.HomePage);
            containerRegistry.RegisterForNavigation<AddCarPage>(Config.AddCarPage);
            containerRegistry.RegisterForNavigation<ExplorePage>(Config.ExplorePage);
            containerRegistry.RegisterForNavigation<LogInPage>(Config.LogInPage);
            containerRegistry.RegisterForNavigation<MorePage>(Config.MorePage);
            containerRegistry.RegisterForNavigation<RegisterPage>(Config.RegisterPage);
            containerRegistry.RegisterForNavigation<RentPage>(Config.RentPage);
        }

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync(Config.LoginNavigation);
        }
    }
}
