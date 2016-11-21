using Newtonsoft.Json.Linq;
using PokemonBetting.Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;

namespace PokemonBetting.Client.ViewModels
{
    public class BattlesPageViewModel : BindableBase
    {
        INavigationService _navigationService;

        public ObservableCollection<Battle> Battles { get; set; }
       
        public DelegateCommand PreviousCommand { get; set; }
        public DelegateCommand NextCommand { get; set; }
        public DelegateCommand GoBackCommand { get; set; }

        public int PageNumber { get { return offset / limit; } }
		protected int offset = 0;
		protected int limit = 10;
		protected bool isFinished=true;


        public BattlesPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Battles = new ObservableCollection<Battle>();

            PreviousCommand = new DelegateCommand(previousBattles);
            NextCommand = new DelegateCommand(nextBattles);
            GoBackCommand = new DelegateCommand(GoBack);

            getBattles();
        }

		protected void nextBattles()
        {
            offset += limit;
            OnPropertyChanged("PageNumber");
            getBattles();
        }

		protected void previousBattles()
        {
            offset -= limit;
            if (offset < 0)
            {
                offset = 0;
            }
            OnPropertyChanged("PageNumber");
            getBattles();
        }

		protected async void GoBack()
        {
            await _navigationService.GoBackAsync();
        }


        protected async void getBattles()
        {
            HttpClient httpClient = new HttpClient();
            //HttpResponseMessage response = await httpClient.GetAsync(("http://pokemon-battle.bid/api/v1/battles/?limit="+limit+"&offset="+offset+"&is_finished=true"));
            HttpResponseMessage response = await httpClient.GetAsync("http://163.172.151.151:5000/battles/limit=" + limit + "&offset=" + offset + "&is_finished="+isFinished);

            string responseString = await response.Content.ReadAsStringAsync();
            JArray jArray = JArray.Parse(responseString);
            Battle[] battleArray = jArray.ToObject<Battle[]>();

            Battles.Clear();
            foreach (Battle b in battleArray)
            {
                Battles.Add(b);
            }
        }

        /*protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }*/
    }
}
