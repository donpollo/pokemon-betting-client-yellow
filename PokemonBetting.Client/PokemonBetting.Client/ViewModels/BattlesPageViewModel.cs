using Newtonsoft.Json.Linq;
using PokemonBetting.Client.Backend.APIClients;
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
		BettingAPIClient BettingClient = new BettingAPIClient();

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
			
			//get the current pots for each battle
			foreach(Battle b in battleArray)
			{
				responseString = await BettingClient.GetAsync("battle/" + b.Id + "/pots");
				jArray = JArray.Parse(responseString);
				BattlePot[] pots = jArray.ToObject<BattlePot[]>();

				//TODO: check if trainerId matches
				b.Pot1 = pots[0].Pot;
				b.Pot2 = pots[1].Pot;
			}

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
