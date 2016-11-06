using PokemonBetting.Client.Helpers;
using PokemonBetting.Client.ViewModels;
using Xamarin.Forms;

namespace PokemonBetting.Client.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            ((LoginPageViewModel) BindingContext).AlertEvent += OnAlert;
        }

        private async void OnAlert(object sender, AlertEventArgs e)
        {
            await DisplayAlert(e.Title, e.Message, "OK");
        }
    }
}