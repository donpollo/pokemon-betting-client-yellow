using PokemonBetting.Client.ViewModels;
using Xamarin.Forms;

namespace PokemonBetting.Client.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (BindingContext as MainPageViewModel)?.EnsureAuthenticated();
        }
    }
}
