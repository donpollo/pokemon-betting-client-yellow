using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Backend.BattleSockets;
using PokemonBetting.Client.Models;
using Prism.Mvvm;

namespace PokemonBetting.Client.ViewModels
{
    public class LiveBattlePageViewModel : BindableBase
    {
        //private const string NextBattleQueryString =
        //  "http://163.172.151.151:5000/battles/limit=1&offset=0&is_finished=false";
        private const string NextBattleQueryString =
            "http://10.0.2.2:5000/battles/limit=1&offset=0&is_finished=false";

        private string infoText;
        private string battleHistory;
        private AbstractLiveBattleSocketFactory socketFactory;

        public LiveBattlePageViewModel()
        {
            InfoText = "Not connected.";
            
            socketFactory = AbstractLiveBattleSocketFactory.Instance;

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
            InfoText = "Connecting to next battle...";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(NextBattleQueryString);

            var responseString = await response.Content.ReadAsStringAsync();
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
