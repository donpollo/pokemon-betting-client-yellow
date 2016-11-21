namespace PokemonBetting.Client.Backend.BattleSockets
{
    public abstract class AbstractLiveBattleSocketFactory
    {
        public abstract AbstractLiveBattleSocket GetSocket(int battleId);

        public static AbstractLiveBattleSocketFactory Instance { get; set; }
    }
}
