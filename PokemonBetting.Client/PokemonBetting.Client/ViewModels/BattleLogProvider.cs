using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Backend.BattleLog;
using PokemonBetting.Client.Models;
using Prism.Mvvm;
using PropertyChanged;

namespace PokemonBetting.Client.ViewModels
{
    [ImplementPropertyChanged]
    internal class BattleLogProvider : BindableBase
    {
        private const string GetBattleRequest = "battles/";

        public BattleLogProvider()
        {
            LogElements = new ObservableCollection<BattleLogItem>();
        }

        public async Task ProvideLogForBattle(Battle battle)
        {
            //var battleApiClient = new BattleAPIClient();
            //var responseString = await battleApiClient.GetAsync(GetBattleRequest + battleId);

            //var battle = JsonConvert.DeserializeObject<Battle>(responseString);

            if (IsBattleFinished(battle))
            {
                var logProvider = new FinishedBattleLogProvider(LogElements);
                await logProvider.GetLogForFinishedBattle(battle.Id);
            }
            else
            {
                var logProvider = new LiveBatteLogProvider(LogElements);
                Task.Run(async () => await logProvider.StartPollingLogForBattle(battle));
            }
        }

        private static bool IsBattleFinished(Battle battle)
        {
            return !string.IsNullOrEmpty(battle.EndTime);
        }

        public ObservableCollection<BattleLogItem> LogElements {get; }
    }
}