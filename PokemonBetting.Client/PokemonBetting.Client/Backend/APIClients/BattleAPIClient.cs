using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBetting.Client.Backend
{
    class BattleAPIClient : APIClient
    {
        protected override string BaseAddress => "http://pokemon-battle.bid/api/v1/battles/";
    }
}
