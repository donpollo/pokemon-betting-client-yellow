using PokemonBetting.Client.Backend.BattleSockets;

namespace PokemonBetting.Client.iOS.BattleSockets
{
    public class LiveBattleSocketFactory : AbstractLiveBattleSocketFactory
    {
        public override AbstractLiveBattleSocket GetSocket(int battleId)
        {
            return new LiveBattleSocket(battleId);
        }
    }
}