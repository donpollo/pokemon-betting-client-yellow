using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PokemonBetting.Client.Backend.APIClients;
using PokemonBetting.Client.Models;

namespace PokemonBetting.Client.Backend.BattleLog
{
    public class LiveBatteLogProvider
    {
        private const string LogQuery = "battleLogs/live/";
        private const int TimeoutMinutes = 2;
        private const int OffsetBeforeBattleStartInSeconds = 10;

        private readonly BettingAPIClient _bettingApiClient;
        private readonly ObservableCollection<string> _targetCollection;

        public LiveBatteLogProvider(ObservableCollection<string> targetCollection)
        {
            this._targetCollection = targetCollection;

            _bettingApiClient = new BettingAPIClient(new TimeSpan(0, 0, TimeoutMinutes, 0));
        }

        public async Task StartPollingLogForBattle(Battle battle)
        {
            var startTime = DateTime.Parse(battle.StartTime, null);
            var now = DateTime.Now;

            var diff = startTime - now;
            var delay = diff - new TimeSpan(0, 0, OffsetBeforeBattleStartInSeconds);

            if (delay < TimeSpan.Zero)
            {
                delay = TimeSpan.Zero;
            }

            await Task.Delay(delay);

            await PollLogElements(battle);
        }

        private async Task PollLogElements(Battle battle)
        {
            while (true)
            {
                try
                {
                    var logElement = await _bettingApiClient.GetAsync(LogQuery + battle.Id);
                    _targetCollection.Add(logElement);
                }
                catch (TaskCanceledException)
                {
                    // request timed out
                    break;
                }
            }
        }
    }
}