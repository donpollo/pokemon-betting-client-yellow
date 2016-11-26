using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBetting.Client.Backend.APIClients
{
    class BettingAPIClient : APIClient
    {
        protected override string BaseAddress => App.BaseAddress;
    }
}
