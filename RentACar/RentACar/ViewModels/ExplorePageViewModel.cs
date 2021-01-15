using Prism.Commands;
using Prism.Navigation;

namespace RentACar.ViewModels
{
    public class ExplorePageViewModel : BaseViewModel
    {
        public string SearchFilter { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public ExplorePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigationService = navigationService;
            SearchCommand = new DelegateCommand(Search);
            SearchFilter = string.Empty;
        }

        public async void Search()
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(Config.SearchPage, SearchFilter);
            await NavigationService.NavigateAsync(Config.CatalogPage, parameters);
        }
    }
}
