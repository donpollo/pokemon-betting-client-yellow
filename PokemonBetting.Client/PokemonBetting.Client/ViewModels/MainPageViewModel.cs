using Microsoft.Practices.Unity;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace PokemonBetting.Client.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public DelegateCommand UserDisplayPageCommand { get; private set; }

        public DelegateCommand NextLiveBattlePageCommand { get; private set; }

        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService,
            IUnityContainer container)
        {
            AuthProvider = container.Resolve<IAuthProvider>("AuthProvider");
            _navigationService = navigationService;

            UserDisplayPageCommand = new DelegateCommand(UserDisplayPage);
            NextLiveBattlePageCommand = new DelegateCommand(NextLiveBattlePage);
        }

        public IAuthProvider AuthProvider { get; }

        public void EnsureAuthenticated()
        {
            if (AuthProvider.IsAuthenticated)
                return;

            _navigationService.NavigateAsync("LoginPage", useModalNavigation: true);
        }

        private async void UserDisplayPage()
        {
            await _navigationService.NavigateAsync(nameof(UserDisplayPage), useModalNavigation: true);
        }

        private async void NextLiveBattlePage()
        {
            await _navigationService.NavigateAsync(nameof(LiveBattlePage), useModalNavigation: true);
        }
    }
}