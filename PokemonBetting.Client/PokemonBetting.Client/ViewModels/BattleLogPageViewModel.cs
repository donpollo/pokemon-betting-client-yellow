using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Backend.BattleLog;
using PokemonBetting.Client.Models;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;

namespace PokemonBetting.Client.ViewModels
{
    [ImplementPropertyChanged]
    public class BattleLogPageViewModel : BindableBase, INavigationAware
    {
        private const string NextBattleQueryString = "battles/?is_finished=false&offset=0&limit=10";

        private readonly BattleLogProvider _batteBattleLogProvider;

        public BattleLogPageViewModel()
        {
            _batteBattleLogProvider = new BattleLogProvider();

            InfoText = "Not connected.";
            BattleHistory = "Here is the battle history.";
        }

        public string InfoText { get; private set; }

        public string BattleHistory { get; private set; }

        public Battle Battle { get; private set; }

        public ObservableCollection<BattleLogItem> BattleLog => _batteBattleLogProvider?.LogElements ?? new ObservableCollection<BattleLogItem>();

        private async Task ConnectTotBattle(Battle battle)
        {
            this.Battle = battle;
            var battleId = battle.Id;

            InfoText = $"Battle has the id {battleId} and starts at {battle.StartDateTime}.";

            _batteBattleLogProvider.LogElements.CollectionChanged += LogProviderOnPropertyChanged;
            await _batteBattleLogProvider.ProvideLogForBattle(battle);
        }

        private void LogProviderOnPropertyChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            //var completeLog = string.Join("\n", _batteBattleLogProvider.LogElements.ToArray());
            //BattleHistory = completeLog;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            var battle = parameters["Battle"] as Battle;
            await ConnectTotBattle(battle);
        }
    }
}
