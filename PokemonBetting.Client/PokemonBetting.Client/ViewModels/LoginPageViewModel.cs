using System;
using System.Linq;
using FluentValidation;
using PokemonBetting.Client.Providers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Microsoft.Practices.Unity;
using PokemonBetting.Client.Helpers;
using PokemonBetting.Client.Models;

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

        public delegate void AlertEventHandler(object sender, AlertEventArgs e);

        public event AlertEventHandler AlertEvent;

        public LoginPageViewModel(INavigationService navigationService,
            IUnityContainer container)
        {
            _navigationService = navigationService;
            _authProvider = container.Resolve<IAuthProvider>("AuthProvider");

            LoginCommand = new DelegateCommand(Login);
            RegisterCommand = new DelegateCommand(Register);
        }

        private async void Register()
        {
            await _navigationService.NavigateAsync("UserForm", useModalNavigation: true);
        }

        private async void Login()
        {
            UserLogin userLogin;
            try
            {
                userLogin = new UserLogin(UserNameText, PasswordText);
            }
            catch (ValidationException e)
            {
                AlertEvent?.Invoke(this, new AlertEventArgs(
                    "Alert", e.Errors.First().ErrorMessage));
                return;
            }

            var authResult = await _authProvider.TryAuth(userLogin);
            switch (authResult)
            {
                case AuthResultEnum.Ok:
                    await _navigationService.GoBackAsync(useModalNavigation: true);
                    return;
                case AuthResultEnum.IncorrectCredentials:
                    AlertEvent?.Invoke(this, new AlertEventArgs(
                        "Alert", "The login attempt was unsuccessful. Check if the username and password are correct."));
                    return;
                case AuthResultEnum.UnknownError:
                    AlertEvent?.Invoke(this, new AlertEventArgs(
                        "Alert", "The login attempt was unsuccessful. Please try again."));
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}