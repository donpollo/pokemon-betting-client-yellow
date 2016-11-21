using PokemonBetting.Client.Backend.BattleSockets;

namespace PokemonBetting.Client.iOS.BattleSockets
{
    public class LiveBattleSocketFactory : AbstractLiveBattleSocketFactory
    {
        public override AbstractLiveBattleSocket GetSocket(int battleId)
        {
            var socket = new LiveBattleSocket(battleId);
            socket.InitializeSocket();
            return socket;
        }
    }
}