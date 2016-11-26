using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Models;
using Prism.Mvvm;

namespace PokemonBetting.Client.ViewModels
{
    public class BattleLogPageViewModel : BindableBase
    {
        private const string NextBattleQueryString = "battles/?is_finished=false&offset=0&limit=10";

        private BattleLogProvider batteBattleLogProvider;
        private string infoText;
        private string battleHistory;

        public BattleLogPageViewModel()
        {
            batteBattleLogProvider = new BattleLogProvider();

            InfoText = "Not connected.";
            BattleHistory = "Here is the battle history.";

            ConnectToNextBattle();
        }

        public string InfoText
        {
            get { return infoText; }
            set { SetProperty(ref infoText, value); }
        }

        public string BattleHistory
        {
            get { return battleHistory; }
            set { SetProperty(ref battleHistory, value); }
        }

        private async Task ConnectToNextBattle()
        {
            var battleApiClient = new BattleAPIClient();

            var responseString = await battleApiClient.GetAsync(NextBattleQueryString);
            var battles = JArray.Parse(responseString).ToObject<Battle[]>();

            var selectedBattle = battles[0];

            foreach (var battle in battles)
            {
                var startTime = DateTime.ParseExact(battle.StartTime, "MM/dd/yyyy HH:mm:ss", null);
                var now = DateTime.Now;

                var diff = startTime - now;

                if (diff >= new TimeSpan(-1, 0, 0))
                {
                    selectedBattle = battle;
                    break;
                }
            }

            var battleId = selectedBattle.Id;

            InfoText = $"Next battle has the id {battleId} and starts at {selectedBattle.StartTime}.";

            batteBattleLogProvider.LogElements.CollectionChanged += LogProviderOnPropertyChanged;
            batteBattleLogProvider.ProvideLogForBattle(battleId);
        }

        private void LogProviderOnPropertyChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var completeLog = string.Join("\n", batteBattleLogProvider.LogElements.ToArray());
            BattleHistory = completeLog;
        }

    }
}
