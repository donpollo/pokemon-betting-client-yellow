using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

//using Newtonsoft.Json;

using Xamarin.Forms;

namespace PokemonBetting.Client
{

    public class Person
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }

    public partial class AccountForm : ContentPage
    {
        public AccountForm()
        {
            InitializeComponent();
        }

        Object toJason(Person p)
        {
            //var json = JsonConvert.SerializeObject(p);
            //return json;
            return null;
        }

        async void OnSend(object sender, EventArgs e)
        {
            Person p = new Person { FirstName = firstNameText.Text, LastName = lastNameText.Text };


            HttpClient httpClient = new HttpClient();
            
            HttpResponseMessage response = await httpClient.GetAsync("https://blogs.msdn.microsoft.com/bclteam/p/httpclient/");

            firstNameText.Text = response.StatusCode.ToString();
            /*var client = new RestClient();
            client.BaseUrl = new Uri("https://github.com");
            var request = new RestRequest("/restsharp/RestSharp/wiki/Getting-Started", Method.GET);
            //request.AddObject(toJason(p)); noting to add, Method.POST should be used
            IRestResponse response = client.Execute(request);

            firstNameText.Text = response.StatusCode.ToString();*/

        }
    }
}
