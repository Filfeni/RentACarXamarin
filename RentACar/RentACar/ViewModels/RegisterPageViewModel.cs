﻿using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RentACar.Models;
using RentACar.Services;
using System.Net.Http;
using Xamarin.Essentials;

namespace RentACar.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel
    {
        public INavigationService NavigationService;
        public IApiService ApiService;
        public IPageDialogService DialogService;
        public DelegateCommand SignUpCommand { get; set; }
        public UserRegister User { get; set; }
        public Response SubmitResponse { get; set; }

        public RegisterPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            ApiService = apiService;
            DialogService = dialogService;
            SignUpCommand = new DelegateCommand(SignUp);
            User = new UserRegister();
        }

        public async void SignUp()
        {
            HttpResponseMessage registerRequest = await ApiService.RegisterUser(User);
            if (registerRequest.IsSuccessStatusCode)
            {
                string responseContent = await registerRequest.Content.ReadAsStringAsync();
                SubmitResponse = JsonConvert.DeserializeObject<Response>(responseContent);
                await DialogService.DisplayAlertAsync($"{SubmitResponse.Status}",$"{SubmitResponse.Message}","OK");
                await NavigationService.NavigateAsync(Config.LoginNavigation);
            }
            else
            {
                string responseContent = await registerRequest.Content.ReadAsStringAsync();
                SubmitResponse = JsonConvert.DeserializeObject<Response>(responseContent);
                await DialogService.DisplayAlertAsync($"{SubmitResponse.Status}", $"{SubmitResponse.Message}", "OK");
            }
        }
    }
}
