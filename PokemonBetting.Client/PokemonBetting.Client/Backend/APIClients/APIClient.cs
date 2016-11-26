using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBetting.Client.Backend
{
    public abstract class APIClient
    {
        private HttpClient httpClient;

        public APIClient()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> GetAsync(string route)
        {
            var response = await httpClient.GetAsync(BaseAddress + route);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        protected abstract string BaseAddress { get; }
    }
}
