using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokemonBetting.Client.Backend.APIClients
{
    public abstract class APIClient
    {
        protected HttpClient httpClient;

        public APIClient()
        {
            httpClient = new HttpClient();
        }

        public APIClient(TimeSpan timeout) : base()
        {
            httpClient = new HttpClient {Timeout = timeout};
        }

        public async Task<string> GetAsync(string route)
        {
            var requestUrl = BaseAddress + route;
            var response = await httpClient.GetAsync(requestUrl);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        protected abstract string BaseAddress { get; }
    }
}
