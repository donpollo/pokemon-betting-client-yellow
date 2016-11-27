using System;
using System.Collections.Specialized;
using System.Linq;
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

        private readonly BattleLogProvider _batteBattleLogProvider;
        private string _infoText;
        private string _battleHistory;

        public BattleLogPageViewModel()
        {
            _batteBattleLogProvider = new BattleLogProvider();

            InfoText = "Not connected.";
            BattleHistory = "Here is the battle history.";

            Task.Run(async () => await ConnectToNextBattle());
        }

        public string InfoText
        {
            get { return _infoText; }
            set { SetProperty(ref _infoText, value); }
        }

        public string BattleHistory
        {
            get { return _battleHistory; }
            set { SetProperty(ref _battleHistory, value); }
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

            _batteBattleLogProvider.LogElements.CollectionChanged += LogProviderOnPropertyChanged;
            await _batteBattleLogProvider.ProvideLogForBattle(battleId);
        }

        private void LogProviderOnPropertyChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var completeLog = string.Join("\n", _batteBattleLogProvider.LogElements.ToArray());
            BattleHistory = completeLog;
        }

    }
}
