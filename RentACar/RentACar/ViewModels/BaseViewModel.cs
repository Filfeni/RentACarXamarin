using Prism.Navigation;
using System.ComponentModel;

namespace RentACar.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        protected INavigationService NavigationService { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
