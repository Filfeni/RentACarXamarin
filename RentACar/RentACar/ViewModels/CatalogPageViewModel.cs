using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RentACar.Models;
using RentACar.Services;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;

namespace RentACar.ViewModels
{
    public class CatalogPageViewModel : BaseViewModel, IInitialize
    {
        public IApiService ApiService { get; set; }
        public IPageDialogService DialogService { get; set; }
        public IAuthService AuthorizationService { get; set; }
        public DelegateCommand<Post> RentCarCommand { get; set; }
        public ObservableCollection<Post> PostList { get; set; }
        public Post SelectedPost { get; set; }
        public CatalogPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService, IAuthService authorizationService) : base(navigationService)
        {
            NavigationService = navigationService;
            ApiService = apiService;
            DialogService = dialogService;
            AuthorizationService = authorizationService;
            PostList = new ObservableCollection<Post>();
            RentCarCommand = new DelegateCommand<Post>(GoToRentPage);
        }

        public async void GoToRentPage(Post post)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(nameof(post), post);
            await NavigationService.NavigateAsync(Config.RentPage, parameters);
        }

        public async void Initialize(INavigationParameters parameters)
        {
            string query = parameters.GetValue<string>(Config.SearchPage);
            bool isAuthorized = await AuthorizationService.Authorize();
            if (!isAuthorized)
            {
                return;
            }
            HttpResponseMessage catalogResponse = await ApiService.SearchCatalogAsync(query);
            if (catalogResponse.IsSuccessStatusCode || catalogResponse.StatusCode == HttpStatusCode.NotFound)
            {
                string responseContent = await catalogResponse.Content.ReadAsStringAsync();
                PostList = JsonConvert.DeserializeObject<ObservableCollection<Post>>(responseContent);
            }
            else
            {
                await DialogService.DisplayAlertAsync("Error", "There was a connection error, Try again later", "OK");
            }
        }
    }
}
