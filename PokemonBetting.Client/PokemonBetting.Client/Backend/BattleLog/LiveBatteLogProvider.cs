using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PokemonBetting.Client.Backend.APIClients;

namespace PokemonBetting.Client.Backend.BattleLog
{
    public class LiveBatteLogProvider
    {
        private const string LogQuery = "battleLogs/live/";
        private const int TimeoutMinutes = 35;

        private BettingAPIClient bettingApiClient;
        private ObservableCollection<string> targetCollection;

        public LiveBatteLogProvider(ObservableCollection<string> targetCollection)
        {
            this.targetCollection = targetCollection;

            bettingApiClient = new BettingAPIClient(new TimeSpan(0, 0, TimeoutMinutes, 0));
        }

        public void StartPollingLogForBattle(int battleId)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        var logElement = await bettingApiClient.GetAsync(LogQuery + battleId);
                        targetCollection.Add(logElement);
                    }
                    catch (TaskCanceledException)
                    {
                        // request timed out
                        break;
                    }
                }
            });
        }
    }
}