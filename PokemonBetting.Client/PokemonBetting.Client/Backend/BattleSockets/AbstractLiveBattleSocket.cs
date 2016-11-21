using System;

namespace PokemonBetting.Client.Backend.BattleSockets
{
    public abstract class AbstractLiveBattleSocket
    {
        public event EventHandler<string> NewMessageArrived;

        public AbstractLiveBattleSocket(int battleId)
        {
            BattleId = battleId;
        }

        public abstract void InitializeSocket();

        protected void OnMessage(string message)
        {
            NewMessageArrived?.Invoke(this, message);
        }

        protected string GetSocketUrl()
        {
            return $"ws://pokemon-battle.bid/battles/{BattleId}/socket.io/?EIO=2&transport=websocket";
        }

        private int BattleId { get; }
    }
}
