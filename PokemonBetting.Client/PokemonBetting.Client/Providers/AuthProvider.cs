using System;
using System.Threading.Tasks;
using PokemonBetting.Client.Backend;
using PokemonBetting.Client.Backend.Models;
using PokemonBetting.Client.Models;
using PropertyChanged;

namespace PokemonBetting.Client.Providers
{
    [ImplementPropertyChanged]
    public class AuthProvider : IAuthProvider
    {
        private readonly IBackendClient _backendClient;

        public string AuthToken { get; private set; }

        public AuthProvider(IBackendClient backendClient)
        {
            _backendClient = backendClient;
        }

        public bool IsAuthenticated => AuthToken != null;

        public async Task<AuthResultEnum> TryAuth(UserLogin userLogin)
        {
            var result = await _backendClient.Login(userLogin);
            switch (result.LoginResult)
            {
                case LoginCallResult.LoginResultEnum.Ok:
                    AuthToken = result.AuthToken;
                    return AuthResultEnum.Ok;
                case LoginCallResult.LoginResultEnum.IncorrectCredentials:
                    AuthToken = null;
                    return AuthResultEnum.IncorrectCredentials;
                case LoginCallResult.LoginResultEnum.UnknownError:
                    AuthToken = null;
                    return AuthResultEnum.UnknownError;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}