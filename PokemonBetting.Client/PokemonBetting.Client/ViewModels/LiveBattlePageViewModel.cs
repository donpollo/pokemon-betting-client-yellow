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
    public class LiveBattlePageViewModel : BindableBase
    {
        private const string NextBattleQueryString =
            "http://163.172.151.151:5000/battles/limit=1&offset=0&is_finished=false";

        public LiveBattlePageViewModel()
        {
            InfoText = "Not connected.";

            ConnectToNextBattle();
        }

        public string InfoText { get; set; }
        public string BattleHistory { get; set; }

        private async Task ConnectToNextBattle()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(NextBattleQueryString);

            var responseString = await response.Content.ReadAsStringAsync();
            var battles = JArray.Parse(responseString).ToObject<Battle[]>();

            var battleId = battles[0].Id;

            var liveSocket = new LiveBattleSocket(battleId);
            liveSocket.NewMessageArrived += LiveSocketOnNewMessageArrived;
        }

        private void LiveSocketOnNewMessageArrived(object sender, string message)
        {
            BattleHistory = BattleHistory + message;
        }
    }
}
