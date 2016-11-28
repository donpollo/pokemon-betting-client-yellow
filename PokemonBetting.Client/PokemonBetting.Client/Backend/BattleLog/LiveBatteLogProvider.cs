using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PokemonBetting.Client.Backend.APIClients;
using PokemonBetting.Client.Models;
using Prism.Mvvm;

namespace PokemonBetting.Client.Backend.BattleLog
{
    public class LiveBatteLogProvider
    {
        private const string LogQuery = "battleLogs/live/";
        private const int TimeoutMinutes = 2;
        private const int OffsetBeforeBattleStartInSeconds = 10;

        private readonly BettingAPIClient _bettingApiClient;
        private readonly ObservableCollection<BattleLogItem> _targetCollection;

        public LiveBatteLogProvider(ObservableCollection<BattleLogItem> targetCollection)
        {
            this._targetCollection = targetCollection;

            _bettingApiClient = new BettingAPIClient(new TimeSpan(0, 0, TimeoutMinutes, 0));
        }

        public async Task StartPollingLogForBattle(Battle battle)
        {
            var delay = battle.StartDateTime - DateTime.Now 
                - TimeSpan.FromSeconds(OffsetBeforeBattleStartInSeconds);

            if (delay > TimeSpan.Zero)
                await Task.Delay(delay);

            Task.Run(async() => await PollLogElements(battle));
        }

        private async Task PollLogElements(Battle battle)
        {
            while (true)
            {
                try
                {
                    var logText = await _bettingApiClient.GetAsync(LogQuery + battle.Id);
                    _targetCollection.Insert(0, new BattleLogItem(DateTime.Now, logText));
                }
                catch (TaskCanceledException)
                {
                    // request timed out
                    break;
                }
            }
        }
    }

    public class BattleLogItem
    {
        public DateTime DateTime { get; set; }
        public string Text { get; set; }

        public BattleLogItem(DateTime dateTime, string text)
        {
            DateTime = dateTime;
            Text = text;
        }
    }
}