using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PokemonBetting.Client.Backend.APIClients;

namespace PokemonBetting.Client.Backend.BattleLog
{
    class FinishedBattleLogProvider
    {
        private const string LogQuery = "battleLogs/";

        private BettingAPIClient bettingApiClient;
        private ObservableCollection<string> targetCollection;

        public FinishedBattleLogProvider(ObservableCollection<string> targetCollection)
        {
            this.targetCollection = targetCollection;

            bettingApiClient = new BettingAPIClient();
        }

        public async Task GetLogForFinishedBattle(int battleId)
        {
            var responseString = await bettingApiClient.GetAsync(LogQuery + battleId);
            var battleLog = JsonConvert.DeserializeObject<Models.BattleLog>(responseString);
        
            using (var reader = new StringReader(battleLog.Text))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    targetCollection.Add(line);
                    line = reader.ReadLine();
                }
            }
        }
    }
}
