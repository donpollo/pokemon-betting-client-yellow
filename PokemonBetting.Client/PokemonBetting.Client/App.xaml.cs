//using PokemonBetting.Client.ViewModels;

using Microsoft.Practices.Unity;
using PokemonBetting.Client.Providers;
using PokemonBetting.Client.Views;
using Prism.Unity;

namespace PokemonBetting.Client
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("MainPage", animated: false);
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<UserForm>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<UserDisplayPage>();

            Container.RegisterInstance<IAuthProvider>("AuthProvider", new AuthProvider());
        }
    }
}