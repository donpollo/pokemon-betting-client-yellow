using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using PokemonBetting.Client.Models;
using System.Collections.ObjectModel;

namespace PokemonBetting.Client.ViewModels
{
	public class UnfinishedBattlesPageViewModel : BattlesPageViewModel
	{
		public UnfinishedBattlesPageViewModel(INavigationService navigationService):base(navigationService)
		{
			IsFinished = false;
			Battles = new ObservableCollection<Battle>();
			GetBattles();
		}
	}
}
