using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin;
using System.Threading.Tasks;
using Prism.Services;

namespace PokemonBetting.Client.ViewModels
{
    public class UserFormViewModel : BindableBase
    {
        public DelegateCommand PostUserCommand { get; set; }
        public string UserNameText { get; set; }
        public string EMailText { get; set; }
        public string PasswordText { get; set; }
        public string PasswordCheckText { get; set; }

        IPageDialogService _dialogService;


        public class User
        {
            public String UserName { get; set; }
            public String EMail { get; set; }
            public String Password { get; set; }
        }


        public UserFormViewModel(IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            PostUserCommand = new DelegateCommand(PostUser);
        }

        String AccountToJson(User a)
        {
            //var json = JsonConvert.SerializeObject(a);
            //return json;
            return null;
        }

        private async void PostUser()
        {
            User a = new User { UserName = this.UserNameText, Password = this.PasswordText, EMail = this.EMailText };


            //HttpClient httpClient = new HttpClient();

            //HttpResponseMessage response = await httpClient.GetAsync("https://blogs.msdn.microsoft.com/bclteam/p/httpclient/");

            //await DisplayAlert("HTTP response status", response.StatusCode.ToString(), "accept");
            await _dialogService.DisplayAlertAsync("You entered:", "UserName: " + a.UserName + " | Email: " + a.EMail + " | Password: " + a.Password + " | PasswordConf: " + a.Password, "accept");
        }

    }
}
