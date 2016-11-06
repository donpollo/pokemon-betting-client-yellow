using System;
using System.Linq;
using FluentValidation;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Microsoft.Practices.Unity;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Models;
using Prism.Services;

namespace PokemonBetting.Client.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        public string UserNameText { get; set; }
        public string PasswordText { get; set; }

        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand RegisterCommand { get; private set; }

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IAuthProvider _authProvider;

        public LoginPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService, IUnityContainer container)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
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
                await _dialogService.DisplayAlertAsync("Alert", e.Errors.First().ErrorMessage, "OK");
                return;
            }

            var authResult = await _authProvider.TryAuth(userLogin);
            switch (authResult)
            {
                case AuthResultEnum.Ok:
                    await _navigationService.GoBackAsync(useModalNavigation: true);
                    return;
                case AuthResultEnum.IncorrectCredentials:
                    await _dialogService.DisplayAlertAsync("Alert",
                        "The login attempt was unsuccessful. Check if the username and password are correct.", "OK");
                    return;
                case AuthResultEnum.UnknownError:
                    await _dialogService.DisplayAlertAsync("Alert",
                        "The login attempt was unsuccessful. Please try again.", "OK");
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}