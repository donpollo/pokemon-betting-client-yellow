using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBetting.Client.Backend.APIClients
{
    public class BettingAPIClient : APIClient
    {
        public BettingAPIClient()
        {
        }

        public BettingAPIClient(TimeSpan timeout) : base(timeout)
        {
        }

        protected override string BaseAddress => App.BaseAddress;
    }
}
