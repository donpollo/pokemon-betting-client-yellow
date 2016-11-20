using Microsoft.Practices.Unity;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;

namespace PokemonBetting.Client.ViewModels
{
    [ImplementPropertyChanged]
    public class UserDisplayPageViewModel : BindableBase, INavigationAware
    {
        public DelegateCommand MainPageCommand { get; private set; }
        public User User { get; private set; }

        private readonly INavigationService _navigationService;
        private readonly IAuthProvider _authProvider;

        public UserDisplayPageViewModel(INavigationService navigationService,
            IUnityContainer container)
        {
            _navigationService = navigationService;
            _authProvider = container.Resolve<IAuthProvider>("AuthProvider");

            MainPageCommand = new DelegateCommand(MainPage);
        }

        private async void MainPage()
        {
            await _navigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            User = parameters["Model"] as User;
        }
    }
}
