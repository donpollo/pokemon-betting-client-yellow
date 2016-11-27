using System;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using PokemonBetting.Client.Models;
using System.Net.Http;
using System.Net.Http.Headers;
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
            if (authProvider != null)
                authProvider.PropertyChanged += AuthProviderOnPropertyChanged;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromSeconds(15)
            };
        }

        private void AuthProviderOnPropertyChanged(object sender,
            PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(IAuthProvider.AuthToken))
            {
                var newToken = _authProvider.AuthToken;
                this._httpClient.DefaultRequestHeaders.Authorization = newToken == null ? null :
                    new AuthenticationHeaderValue("Bearer", _authProvider.AuthToken);
            }
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

        public async Task<DepositCallResult> Deposit(int amount)
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync($"users/me/deposit/{amount}", null);
            }
            catch (Exception)
            {
                return new DepositCallResult(DepositCallResult.DepositCallResultEnum.InternalServerError);
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return new DepositCallResult(DepositCallResult.DepositCallResultEnum.NoContent);
                default:
                    return new DepositCallResult(DepositCallResult.DepositCallResultEnum.InternalServerError);
            }
        }

        public async Task<WithdrawCallResult> Withdraw(int amount)
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync($"users/me/withdraw/{amount}", null);
            }
            catch (Exception)
            {
                return new WithdrawCallResult(WithdrawCallResult.WithdrawCallResultEnum.InternalServerError);
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return new WithdrawCallResult(WithdrawCallResult.WithdrawCallResultEnum.BadRequest);
                case HttpStatusCode.NoContent:
                    return new WithdrawCallResult(WithdrawCallResult.WithdrawCallResultEnum.NoContent);
                default:
                    return new WithdrawCallResult(WithdrawCallResult.WithdrawCallResultEnum.InternalServerError);
            }
        }
    }
}