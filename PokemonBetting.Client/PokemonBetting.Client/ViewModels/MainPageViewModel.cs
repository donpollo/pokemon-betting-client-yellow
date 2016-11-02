using Microsoft.Practices.Unity;
using PokemonBetting.Client.Providers;
using Prism.Mvvm;
using Prism.Navigation;

namespace PokemonBetting.Client.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthProvider _authProvider;

        public MainPageViewModel(INavigationService navigationService,
            IUnityContainer container)
        {
            _authProvider = container.Resolve<IAuthProvider>("AuthProvider");
            _navigationService = navigationService;
        }

        public void EnsureAuthenticated()
        {
            if (_authProvider.IsAuthenticated)
                return;

            _navigationService.NavigateAsync("LoginPage", useModalNavigation: true);
        }
    }
}