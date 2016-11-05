﻿using System;
using System.Net;
using System.Threading.Tasks;
using PokemonBetting.Client.Models;
using PokemonBetting.Client.Providers;
using System.Net.Http;
using PokemonBetting.Client.Backend.Models;

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

            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
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
    }
}