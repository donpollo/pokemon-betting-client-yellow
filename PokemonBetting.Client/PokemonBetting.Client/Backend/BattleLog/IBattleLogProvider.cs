using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBetting.Client.Backend.BattleLog
{
    interface IBattleLogProvider
    {
        ObservableCollection<string> LogElements { get; set; }
    }
}
