using Microsoft.Practices.Unity;
using PokemonBetting.Client.Providers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace PokemonBetting.Client.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public DelegateCommand UserDisplayPageCommand { get; private set; }

        private readonly INavigationService _navigationService;
        private readonly IAuthProvider _authProvider;

        public MainPageViewModel(INavigationService navigationService,
            IUnityContainer container)
        {
            _authProvider = container.Resolve<IAuthProvider>("AuthProvider");
            _navigationService = navigationService;

            UserDisplayPageCommand = new DelegateCommand(UserDisplayPage);
        }

        public void EnsureAuthenticated()
        {
            if (_authProvider.IsAuthenticated)
                return;

            _navigationService.NavigateAsync("LoginPage", useModalNavigation: true);
        }

        private async void UserDisplayPage()
        {
            await _navigationService.NavigateAsync(nameof(UserDisplayPage), useModalNavigation: true);
        }
    }
}