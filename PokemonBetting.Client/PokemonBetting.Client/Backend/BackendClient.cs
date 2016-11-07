using System;
using System.Net;
using System.Threading.Tasks;
using PokemonBetting.Client.Models;
using System.Net.Http;
using PokemonBetting.Client.Backend.CallResults;

namespace PokemonBetting.Client.Backend
{
    public class BackendClient : IBackendClient
    {
        private readonly IAuthProvider _authProvider;

        private readonly HttpClient _httpClient;

        public BackendClient(IAuthProvider authProvider,
            string baseAddress)
        {
            _authProvider = authProvider;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromSeconds(15)
            };
        }

        public async Task<LoginCallResult> Login(UserLogin userLogin)
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync("login", userLogin.ToJson());
            }
            catch (Exception)
            {
                return new LoginCallResult(LoginCallResult.LoginResultEnum.UnknownError);
            }

            if (!response.IsSuccessStatusCode)
            {
                return response.StatusCode == HttpStatusCode.Unauthorized
                    ? new LoginCallResult(LoginCallResult.LoginResultEnum.IncorrectCredentials)
                    : new LoginCallResult(LoginCallResult.LoginResultEnum.UnknownError);
            }

            var responseString = await response.Content.ReadAsStringAsync();
            return new LoginCallResult(responseString);
        }

        public async Task<CreateUserCallResult> CreateUser(NewUser userData)
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync("users", userData.ToJson());
            }
            catch (Exception)
            {
                return new CreateUserCallResult(CreateUserCallResult.CreateUserResultEnum.UnknownError);
            }

            if (!response.IsSuccessStatusCode)
                return new CreateUserCallResult(CreateUserCallResult.CreateUserResultEnum.UnknownError);

            return new CreateUserCallResult(CreateUserCallResult.CreateUserResultEnum.Ok);
        }

        public async Task<GetAuthenticatedUserCallResult> GetAuthenticatedUser()
        {
            if (!_authProvider.IsAuthenticated)
                throw new Exception("User is not authenticated.");

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync("users/me");
            }
            catch (Exception)
            {
                return new GetAuthenticatedUserCallResult(GetAuthenticatedUserCallResult.GetAuthenticatedUserResultEnum.UnknownError);
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var user = User.FromJson(responseString);
            return new GetAuthenticatedUserCallResult(user);
        }
    }
}