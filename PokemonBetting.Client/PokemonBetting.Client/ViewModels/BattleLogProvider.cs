using System.Collections.ObjectModel;
using Newtonsoft.Json;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Backend.BattleLog;
using PokemonBetting.Client.Models;
using Prism.Mvvm;

namespace PokemonBetting.Client.ViewModels
{
    class BattleLogProvider : BindableBase
    {
        private ObservableCollection<string> _logElements;
        private const string GetBattleRequest = "battles/";

        public BattleLogProvider()
        {
            LogElements = new ObservableCollection<string>();
        }

        public async void ProvideLogForBattle(int battleId)
        {
            var battleApiClient = new BattleAPIClient();
            var responseString = await battleApiClient.GetAsync(GetBattleRequest + battleId);

            var battle = JsonConvert.DeserializeObject<Battle>(responseString);

            if (IsBattleFinished(battle))
            {
                var logProvider = new FinishedBattleLogProvider(LogElements);
                await logProvider.GetLogForFinishedBattle(battleId);
            }
            else
            {
                var logProvider = new LiveBatteLogProvider(LogElements);
                logProvider.StartPollingLogForBattle(battleId);
            }
        }

        private bool IsBattleFinished(Battle battle)
        {
            return !string.IsNullOrEmpty(battle.EndTime);
        }

        public ObservableCollection<string> LogElements
        {
            get { return _logElements; }
            set { SetProperty(ref _logElements, value); }
        }
    }
}
