using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace PokemonBetting.Client.Backend
{
    public class LiveBattleSocket
    {
        public event EventHandler<string> NewMessageArrived;

        public LiveBattleSocket(int battleId)
        {
            var webSocket = new WebSocket(GetSocketUrl(battleId));
            webSocket.OnMessage += WebSocketOnOnMessage;
            webSocket.ConnectAsync();
        }

        private void WebSocketOnOnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            NewMessageArrived?.Invoke(this, messageEventArgs.Data);
        }

        private string GetSocketUrl(int battleId)
        {
            return $"ws://pokemon-battle.bid/battles/{battleId}/socket.io/?EIO=2&transport=websocket";
        }
    }
}
