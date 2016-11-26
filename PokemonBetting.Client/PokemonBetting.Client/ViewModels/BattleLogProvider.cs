using System.Collections.ObjectModel;
using Newtonsoft.Json;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Backend.BattleLog;
using PokemonBetting.Client.Models;
using Prism.Mvvm;

namespace PokemonBetting.Client.ViewModels
{
    class BattleLogProvider : BindableBase, IBattleLogProvider
    {
        private const string GetBattleRequest = "battles/";

        public BattleLogProvider(string baseAddress)
        {
            
        }

        public async void ProvideLogForBattle(int battleId)
        {
            var battleApiClient = new BattleAPIClient();
            var responseString = await battleApiClient.GetAsync(GetBattleRequest + battleId);

            var battle = JsonConvert.DeserializeObject<Battle>(responseString);

            if (IsBattleFinished(battle))
            {
                
            }
        }

        private bool IsBattleFinished(Battle battle)
        {
            return !string.IsNullOrEmpty(battle.EndTime);
        }

        public ObservableCollection<string> LogElements { get; set; }
    }
}
