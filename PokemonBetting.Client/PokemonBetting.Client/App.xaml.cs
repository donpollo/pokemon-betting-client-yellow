//using PokemonBetting.Client.ViewModels;

using Microsoft.Practices.Unity;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Providers;
using PokemonBetting.Client.Views;
using Prism.Unity;

namespace PokemonBetting.Client
{
    public partial class App : PrismApplication
    {
        private const string BaseAddress = "http://163.172.151.151/";

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
            Container.RegisterTypeForNavigation<BattlesPage>();

            var authProvider = new AuthProvider(new BackendClient(null, BaseAddress));
            Container.RegisterInstance<IAuthProvider>("AuthProvider", authProvider);
            Container.RegisterInstance<IBackendClient>("BackendClient",
                 new BackendClient(authProvider, BaseAddress));
        }
    }
}