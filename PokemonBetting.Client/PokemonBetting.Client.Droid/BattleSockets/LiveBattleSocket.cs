using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PokemonBetting.Client.Backend.BattleSockets;
using Quobject.SocketIoClientDotNet.Client;

namespace PokemonBetting.Client.Droid.BattleSockets
{
    class LiveBattleSocket : AbstractLiveBattleSocket
    {
        public LiveBattleSocket(int battleId) : base(battleId)
        {
        }

        protected override void InitializeSocket()
        {
            var socket = IO.Socket(GetSocketUrl());
            socket.On(Socket.EVENT_MESSAGE, data => OnMessage(data.ToString()));
        }
    }
}