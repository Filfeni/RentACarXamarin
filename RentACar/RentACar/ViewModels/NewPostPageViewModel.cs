using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RentACar.Models;
using RentACar.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RentACar.ViewModels
{
    public class NewPostPageViewModel : BaseViewModel, IInitialize
    {
        public Post NewPost { get; set; }
        public DelegateCommand CreatePostCommand { get; set; }
        public INavigationService NavigationService;
        public IApiService ApiService;
        public IPageDialogService DialogService;
        public IAuthService AuthorizationService;

        public NewPostPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService, IAuthService authorizationService)
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
            HttpResponseMessage registerPostResponse = await ApiService.CreatePost(NewPost);
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

        public void Initialize(INavigationParameters parameters)
        {
            NewPost.CarId = parameters.GetValue<int>(nameof(NewPost.CarId));
        }

    }
}
