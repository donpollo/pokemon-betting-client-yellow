using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Models;
using Prism.Mvvm;

namespace PokemonBetting.Client.ViewModels
{
    public class BattleLogPageViewModel : BindableBase
    {
        private const string NextBattleQueryString = "battles/?is_finished=false&offset=0&limit=1";

        private string infoText;
        private string battleHistory;

        public BattleLogPageViewModel()
        {
            InfoText = "Not connected.";

            ConnectToNextBattle();
        }

        public string InfoText
        {
            get { return infoText; }
            set { SetProperty(ref infoText, value); }
        }

        public string BattleHistory
        {
            get { return battleHistory; }
            set { SetProperty(ref battleHistory, value); }
        }

        private async Task ConnectToNextBattle()
        {
            var battleApiClient = new BattleAPIClient();

            var responseString = await battleApiClient.GetAsync(NextBattleQueryString);
            var battles = JArray.Parse(responseString).ToObject<Battle[]>();

            var battle = battles[0];
            var battleId = battle.Id;

            InfoText = $"Next battle has the id {battleId} and starts at {battle.StartTime}.";

            var liveSocket = socketFactory.GetSocket(battleId);
            liveSocket.NewMessageArrived += LiveSocketOnNewMessageArrived;
        }

        private void LiveSocketOnNewMessageArrived(object sender, string message)
        {
            BattleHistory = BattleHistory + message;
        }
    }
}
