using Microsoft.Practices.Unity;
using PokemonBetting.Client.Backend;
using Prism.Mvvm;
using Prism.Navigation;

namespace PokemonBetting.Client.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService,
            IUnityContainer container)
        {
            AuthProvider = container.Resolve<IAuthProvider>("AuthProvider");
            _navigationService = navigationService;
        }

        public IAuthProvider AuthProvider { get; }

        public void EnsureAuthenticated()
        {
            if (AuthProvider.IsAuthenticated)
                return;

            _navigationService.NavigateAsync("LoginPage", useModalNavigation: true);
        }
    }
}