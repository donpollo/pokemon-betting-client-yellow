using Prism.Commands;
using Prism.Mvvm;

using Prism.Services;
using PokemonBetting.Client.Models;
using FluentValidation;
using System.Net.Http;
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

        IPageDialogService _dialogService;
        INavigationService _navigationService;

        public UserFormViewModel(IPageDialogService dialogService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            PostUserCommand = new DelegateCommand(PostUser);
            GoBackCommand = new DelegateCommand(GoBack);
        }
        
        private async void GoBack()
        {
            await _navigationService.NavigateAsync("MainPage");
        }

        private async void PostUser()
        {
            try {
                User user = new User(this.UserNameText, this.EMailText, this.PasswordText, this.PasswordCheckText);
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.PostAsync("http://httpbin.org/post", user.ToJson() );
                string responseString = await response.Content.ReadAsStringAsync();
                await _dialogService.DisplayAlertAsync("HTTP response status: "+ response.StatusCode.ToString(), responseString, "accept");
                await _navigationService.NavigateAsync("MainPage");
            }
            catch(ValidationException e)
            {
                string errorString="";
                foreach (var failure in e.Errors)
                {
                    errorString = failure.ToString() + "\n";
                }

                await _dialogService.DisplayAlertAsync("Invalid input:", errorString, "back");
            }
        }
    }
}
