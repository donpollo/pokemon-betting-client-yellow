using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Xamarin.Forms;

namespace PokemonBetting.Client
{

    public class User
    {
        public String UserName { get; set; }
        public String EMail { get; set; }
        public String Password { get; set; }
    }

    public partial class AccountForm : ContentPage
    {
        public AccountForm()
        {
            InitializeComponent();
        }

        String AccountToJson(User a)
        {
            var json = JsonConvert.SerializeObject(a);
            return json;
        }

        async void OnSend(object sender, EventArgs e)
        {
            User a = new User { UserName = userNameText.Text, Password = passwordText.Text ,EMail = eMailText.Text };


            HttpClient httpClient = new HttpClient();
            
            HttpResponseMessage response = await httpClient.GetAsync("https://blogs.msdn.microsoft.com/bclteam/p/httpclient/");

            //await DisplayAlert("HTTP response status", response.StatusCode.ToString(), "accept");
            await DisplayAlert("You entered:","UserName: " +userNameText.Text+ " | Email: "+ eMailText.Text+" | Password: "+ passwordText.Text+" | PasswordConf: "+ passwordCheckText.Text, "accept");
        }
    }
}
