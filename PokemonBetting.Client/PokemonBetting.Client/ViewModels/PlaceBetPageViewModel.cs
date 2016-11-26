using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;

namespace PokemonBetting.Client.ViewModels
{
    [ImplementPropertyChanged]
    public class PlaceBetPageViewModel : BindableBase, INavigationAware
    {
        private readonly IAuthProvider authProvider;
        private readonly INavigationService navigationService;

        public PlaceBetPageViewModel(INavigationService navigationService, IUnityContainer container)
        {
            this.navigationService = navigationService;

            authProvider = container.Resolve<IAuthProvider>("AuthProvider");

            GoBackCommand = new DelegateCommand(GoBack);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            // empty        
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Battle = (Battle) parameters["Battle"];
        }

        public DelegateCommand GoBackCommand { get; private set; }

        public Battle Battle { get; set; }

        private async void GoBack()
        {
            await navigationService.GoBackAsync();
        }
    }
}
