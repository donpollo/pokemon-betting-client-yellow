using Microsoft.Practices.Unity;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace PokemonBetting.Client.ViewModels
{
    public class UserDisplayPageViewModel : BindableBase
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

            // TODO Get actual user with api call
            User = new User("Garry Host", "garry@host.com", "pw", "pw");
        }

        private async void MainPage()
        {
            await _navigationService.NavigateAsync(nameof(MainPage), useModalNavigation: true);
        }
    }
}
