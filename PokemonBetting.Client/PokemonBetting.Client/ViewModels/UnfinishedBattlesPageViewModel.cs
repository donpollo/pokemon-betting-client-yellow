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
			isFinished = false;
			Battles = new ObservableCollection<Battle>();
			getBattles();
		}
	}
}
