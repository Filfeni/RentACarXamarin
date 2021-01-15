using Prism;
using Prism.Ioc;
using Prism.Unity;
using RentACar.Services;
using RentACar.ViewModels;
using RentACar.Views;
using Xamarin.Forms;

namespace RentACar
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            base.OnSleep();

            // TODO: This is the time to save app data in case the process is terminated.
            // This is the perfect timing to release exclusive resources (camera, I/O devices, etc...)
        }

        protected override void OnResume()
        {
            base.OnResume();

            // TODO: Refresh network data, perform UI updates, and reacquire resources like cameras, I/O devices, etc.
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomeTabbedPage, HomeTabbedPageViewModel>(Config.HomePage);
            containerRegistry.RegisterForNavigation<MyCarsPage, MyCarsPageViewModel>(Config.MyCarsPage);
            containerRegistry.RegisterForNavigation<CatalogPage, CatalogPageViewModel>(Config.CatalogPage);
            containerRegistry.RegisterForNavigation<NewCarPage, NewCarPageViewModel>(Config.NewCarPage);
            containerRegistry.RegisterForNavigation<NewPostPage, NewPostPageViewModel>(Config.NewPostPage);
            containerRegistry.RegisterForNavigation<ExplorePage, ExplorePageViewModel>(Config.ExplorePage);
            containerRegistry.RegisterForNavigation<LogInPage, LogInPageViewModel>(Config.LogInPage);
            containerRegistry.RegisterForNavigation<MorePage, MorePageViewModel>(Config.MorePage);
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>(Config.RegisterPage);
            containerRegistry.RegisterForNavigation<ReservationsPage, ReservationsPageViewModel>(Config.ReservationsPage);
            containerRegistry.RegisterForNavigation<RentPage, RentPageViewModel>(Config.RentPage);
            containerRegistry.Register<EmptyReservationsView>();
            containerRegistry.Register<IApiService,ApiService>();
            containerRegistry.Register<IAuthService, AuthService>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync(Config.HomeTabbedPageNavigation);
        }
    }
}
