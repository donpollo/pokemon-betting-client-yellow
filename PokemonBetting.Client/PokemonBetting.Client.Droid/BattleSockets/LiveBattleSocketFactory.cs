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

namespace PokemonBetting.Client.Droid.BattleSockets
{
    public class LiveBattleSocketFactory : AbstractLiveBattleSocketFactory
    {
        public override AbstractLiveBattleSocket GetSocket(int battleId)
        {
            return new LiveBattleSocket(battleId);
        }
    }
}