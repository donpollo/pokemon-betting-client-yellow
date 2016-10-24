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
            NavigationService.NavigateAsync("LoginPage", animated: false);
            //NavigationService.NavigateAsync("MyNavigationPage/MyTabbedPage", animated: false);
        }

        protected override void RegisterTypes()
        {
            //Container.RegisterTypeForNavigation<MainPage, SomeOtherViewModel>(); //override viewmodel convention[not sure what this is]
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<UserForm>();
            Container.RegisterTypeForNavigation<MainPage>();

            Container.RegisterInstance<IAuthProvider>("AuthProvider", new AuthProvider());
        }

        protected override void ConfigureModuleCatalog()
        {
            //ModuleCatalog.AddModule(new ModuleInfo(typeof(ModuleA.ModuleAModule)));
            //ModuleCatalog.AddModule(new ModuleInfo("ModuleA", typeof(ModuleA.ModuleAModule), InitializationMode.OnDemand));
        }
    }
}