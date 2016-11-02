using System;
using System.Threading.Tasks;

namespace PokemonBetting.Client.Providers
{
    public class AuthProvider : IAuthProvider
    {
        public bool IsAuthenticated { get; private set; }

        public async Task TryAuth(string username, string password)
        {
            // TODO: Actual auth
            await Task.Delay(TimeSpan.FromSeconds(0.3));
            this.IsAuthenticated = true;
        }
    }
}