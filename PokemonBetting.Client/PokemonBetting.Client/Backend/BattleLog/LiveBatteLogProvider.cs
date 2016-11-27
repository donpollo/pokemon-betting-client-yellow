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

        private BettingAPIClient bettingApiClient;
        private ObservableCollection<string> targetCollection;

        public LiveBatteLogProvider(ObservableCollection<string> targetCollection)
        {
            this.targetCollection = targetCollection;

            bettingApiClient = new BettingAPIClient(new TimeSpan(0, 0, TimeoutMinutes, 0));
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

            Task.Run(() => PollLogElements(battle));
        }

        private async Task PollLogElements(Battle battle)
        {
            while (true)
            {
                try
                {
                    var logElement = await bettingApiClient.GetAsync(LogQuery + battle.Id);
                    targetCollection.Add(logElement);
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