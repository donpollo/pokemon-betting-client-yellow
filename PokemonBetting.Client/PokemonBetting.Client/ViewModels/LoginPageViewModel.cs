using PokemonBetting.Client.Providers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Microsoft.Practices.Unity;

namespace PokemonBetting.Client.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        public string UserNameText { get; set; }
        public string PasswordText { get; set; }

        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand RegisterCommand { get; private set; }

        private readonly INavigationService _navigationService;
        private readonly IAuthProvider _authProvider;

        public LoginPageViewModel(INavigationService navigationService,
            IUnityContainer container)
        {
            _navigationService = navigationService;
            _authProvider = container.Resolve<IAuthProvider>();

            LoginCommand = new DelegateCommand(Login);
            RegisterCommand = new DelegateCommand(Register);
        }

        private async void Register()
        {
            await _navigationService.NavigateAsync("UserForm", useModalNavigation: true);
        }

        private async void Login()
        {
            await _authProvider.TryAuth(UserNameText, PasswordText);
            if (_authProvider.IsAuthenticated)
            {
                await _navigationService.NavigateAsync("MainPage");
            }
        }
    }
}