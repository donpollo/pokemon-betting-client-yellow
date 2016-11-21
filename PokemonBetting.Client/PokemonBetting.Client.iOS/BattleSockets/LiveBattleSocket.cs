using PokemonBetting.Client.Backend.BattleSockets;
using Quobject.SocketIoClientDotNet.Client;

namespace PokemonBetting.Client.iOS.BattleSockets
{
    class LiveBattleSocket : AbstractLiveBattleSocket
    {
        public LiveBattleSocket(int battleId) : base(battleId)
        {
        }

        public override void InitializeSocket()
        {
            var socket = IO.Socket(GetSocketUrl());
            socket.On(Socket.EVENT_MESSAGE, data => OnMessage(data.ToString()));
        }
    }
}