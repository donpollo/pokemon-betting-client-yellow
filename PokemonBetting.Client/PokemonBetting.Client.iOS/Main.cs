using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using PokemonBetting.Client.Backend.BattleSockets;
using PokemonBetting.Client.iOS.BattleSockets;
using UIKit;

namespace PokemonBetting.Client.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            AbstractLiveBattleSocketFactory.Instance = new LiveBattleSocketFactory();
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
