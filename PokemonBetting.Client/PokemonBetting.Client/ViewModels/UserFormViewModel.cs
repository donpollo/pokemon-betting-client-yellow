using System;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;

using Prism.Services;
using PokemonBetting.Client.Models;
using FluentValidation;
using System.Net.Http;
using Microsoft.Practices.Unity;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Backend.CallResults;
using Prism.Navigation;

namespace PokemonBetting.Client.ViewModels
{
    public class UserFormViewModel : BindableBase
    {
        //these are the bindings used in the view
        public DelegateCommand PostUserCommand { get; set; }
        public DelegateCommand GoBackCommand { get; set; }
        public string UserNameText { get; set; }
        public string EMailText { get; set; }
        public string PasswordText { get; set; }
        public string PasswordCheckText { get; set; }

        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private IBackendClient _backendClient;

        public UserFormViewModel(IPageDialogService dialogService,
            INavigationService navigationService, IUnityContainer container)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            PostUserCommand = new DelegateCommand(PostUser);
            GoBackCommand = new DelegateCommand(GoBack);

            _backendClient = container.Resolve<IBackendClient>("BackendClient");
        }
        
        private async void GoBack()
        {
            await _navigationService.GoBackAsync();
        }

        private async void PostUser()
        {
            User user;
            try
            {
                user = new User(UserNameText, EMailText, PasswordText, PasswordCheckText);
            }
            catch (ValidationException e)
            {
                await _dialogService.DisplayAlertAsync("Alert", e.Errors.First().ErrorMessage, "OK");
                return;
            }

            var createUserResult = await _backendClient.CreateUser(user);
            switch (createUserResult.CreateUserResult)
            {
                case CreateUserCallResult.CreateUserResultEnum.Ok:
                    await _navigationService.GoBackAsync(useModalNavigation: true);
                    return;
                case CreateUserCallResult.CreateUserResultEnum.UnknownError:
                    await _dialogService.DisplayAlertAsync("Alert",
                        "Couldn't create a user. Please try again.", "OK");
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
