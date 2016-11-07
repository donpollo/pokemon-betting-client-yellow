﻿using System;
using Microsoft.Practices.Unity;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Backend.CallResults;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace PokemonBetting.Client.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public DelegateCommand UserDisplayPageCommand { get; private set; }

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IAuthProvider _authProvider;
        private readonly IBackendClient _backendClient;

        public MainPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService, IUnityContainer container)
        {
            _authProvider = container.Resolve<IAuthProvider>("AuthProvider");
            _backendClient = container.Resolve<IBackendClient>("BackendClient");
            _navigationService = navigationService;
            _dialogService = dialogService;

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
            if (!_authProvider.IsAuthenticated)
            {
                await _dialogService.DisplayAlertAsync("Alert", "Please log in first.", "OK");
                return;
            }

            var userDataResult = await _backendClient.GetAuthenticatedUser();
            switch (userDataResult.GetAuthenticatedUserResult)
            {
                case GetAuthenticatedUserCallResult.GetAuthenticatedUserResultEnum.Ok:
                    var navigationParameters = new NavigationParameters {["Model"] = userDataResult.User};
                    await _navigationService.NavigateAsync(nameof(UserDisplayPage), navigationParameters);
                    return;
                case GetAuthenticatedUserCallResult.GetAuthenticatedUserResultEnum.UnknownError:
                    await _dialogService.DisplayAlertAsync("Alert", "Couldn't get user data from the server.", "OK");
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}