﻿using Xamarin.Forms;

namespace PokemonBetting.Client.Views
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            //this.MainPage = new MainPage();
            this.MainPage = new UserForm();
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
