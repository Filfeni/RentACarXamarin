using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RentACar.Models;
using RentACar.Services;
using System.Net.Http;

namespace RentACar.ViewModels
{
    public class NewPostPageViewModel : BaseViewModel, IInitialize
    {
        public Post NewPost { get; set; }
        public DelegateCommand CreatePostCommand { get; set; }
        public IApiService ApiService { get; set; }
        public IPageDialogService DialogService { get; set; }
        public IAuthService AuthorizationService { get; set; }

        public NewPostPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService, IAuthService authorizationService) : base(navigationService)
        {
            NavigationService = navigationService;
            ApiService = apiService;
            DialogService = dialogService;
            AuthorizationService = authorizationService;
            NewPost = new Post();
            CreatePostCommand = new DelegateCommand(CreatePost);
        }

        private async void CreatePost()
        {
            HttpResponseMessage registerPostResponse = await ApiService.CreatePostAsync(NewPost);
            if (registerPostResponse.IsSuccessStatusCode)
            {
                string responseContent = await registerPostResponse.Content.ReadAsStringAsync();
                Response submitResponse = JsonConvert.DeserializeObject<Response>(responseContent);
                await DialogService.DisplayAlertAsync($"{submitResponse.Status}", $"{submitResponse.Message}", "OK");
                await NavigationService.NavigateAsync(Config.HomeTabbedPageNavigation);
            }
            else
            {
                string responseContent = await registerPostResponse.Content.ReadAsStringAsync();
                Response submitResponse = JsonConvert.DeserializeObject<Response>(responseContent);
                await DialogService.DisplayAlertAsync($"{submitResponse.Status}", $"{submitResponse.Message}", "OK");
            }
        }

        public async void Initialize(INavigationParameters parameters)
        {
            bool isAuthorized = await AuthorizationService.Authorize();
            if (!isAuthorized)
            {
                return;
            }
            NewPost.CarId = parameters.GetValue<int>(nameof(NewPost.CarId));
        }

    }
}
