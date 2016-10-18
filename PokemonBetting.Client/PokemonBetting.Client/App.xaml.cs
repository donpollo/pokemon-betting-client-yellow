using Xamarin.Forms;

namespace PokemonBetting.Client
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            //this.MainPage = new MainPage();
            this.MainPage = new AccountForm();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
