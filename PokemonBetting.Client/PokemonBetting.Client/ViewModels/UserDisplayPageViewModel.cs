using Microsoft.Practices.Unity;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Backend.CallResults;
using PokemonBetting.Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using System;
using System.Threading.Tasks;

namespace PokemonBetting.Client.ViewModels
{
    [ImplementPropertyChanged]
    public class UserDisplayPageViewModel : BindableBase, INavigationAware
    {
        public DelegateCommand MainPageCommand { get; private set; }
        public DelegateCommand DepositCommand { get; private set; }
        public DelegateCommand WithdrawCommand { get; private set; }
        public User User { get; private set; }
        public string AmountEntry { get; set; } = "0"; 

        private readonly INavigationService _navigationService;
        private readonly IAuthProvider _authProvider;
        private readonly IPageDialogService _dialogService;
        private readonly IBackendClient _backendClient;

        public UserDisplayPageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            IUnityContainer container)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _authProvider = container.Resolve<IAuthProvider>("AuthProvider");
            _backendClient = container.Resolve<IBackendClient>("BackendClient");

            MainPageCommand = new DelegateCommand(MainPage);
            DepositCommand = new DelegateCommand(Deposit);
            WithdrawCommand = new DelegateCommand(Withdraw);
        }

        private async void MainPage()
        {
            await _navigationService.GoBackAsync();
        }

        private async void Deposit()
        {
            if (!_authProvider.IsAuthenticated)
            {
                await _dialogService.DisplayAlertAsync("Alert", "Please log in first.", "OK");
                return;
            }

            int amount;
            if (!int.TryParse(AmountEntry, out amount))
            {
                await _dialogService.DisplayAlertAsync("Alert", "Invalid amount entered.", "OK");
                return;
            }

            var depositResult = await _backendClient.Deposit(amount);
            switch (depositResult.Result)
            {
                case DepositCallResult.DepositCallResultEnum.NoContent:
                    AmountEntry = "0";
                    await reloadUser();
                    await _dialogService.DisplayAlertAsync("Success", $"Successfully deposited {amount} on your balance.", "OK");
                    return;
                default:
                    await _dialogService.DisplayAlertAsync("Error", "Couldn't get deposit the amount due to a server error.", "OK");
                    return;
            }
        }

        private async void Withdraw()
        {
            if (!_authProvider.IsAuthenticated)
            {
                await _dialogService.DisplayAlertAsync("Alert", "Please log in first.", "OK");
                return;
            }

            int amount;
            if (!int.TryParse(AmountEntry, out amount))
            {
                await _dialogService.DisplayAlertAsync("Alert", "Invalid amount entered.", "OK");
                return;
            }

            var withdrawResult = await _backendClient.Withdraw(amount);
            switch (withdrawResult.Result)
            {
                case WithdrawCallResult.WithdrawCallResultEnum.NoContent:
                    AmountEntry = "0";
                    await reloadUser();
                    await _dialogService.DisplayAlertAsync("Success", $"Successfully withdrew {amount} of your balance.", "OK");
                    return;
                case WithdrawCallResult.WithdrawCallResultEnum.BadRequest:
                    await _dialogService.DisplayAlertAsync("Insufficient funds", $"Couldn't withdraw {amount} of your balance due to insufficient funds.", "OK");
                    return;
                default:
                    await _dialogService.DisplayAlertAsync("Error", "Couldn't withdraw the amount due to a server error.", "OK");
                    return;
            }
        }

        private async Task reloadUser()
        {
            if (!_authProvider.IsAuthenticated)
            {
                await _dialogService.DisplayAlertAsync("Alert", "Please log in first.", "OK");
                return;
            }

            var userDataResult = await _backendClient.GetAuthenticatedUser();
            switch (userDataResult.Result)
            {
                case GetAuthenticatedUserCallResult.GetAuthenticatedUserResultEnum.Ok:
                    User = userDataResult.User;
                    return;
                case GetAuthenticatedUserCallResult.GetAuthenticatedUserResultEnum.UnknownError:
                    await _dialogService.DisplayAlertAsync("Alert", "Couldn't get user data from the server.", "OK");
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            User = parameters["Model"] as User;
        }
    }
}
