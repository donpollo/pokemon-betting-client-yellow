namespace PokemonBetting.Client.Backend.CallResults
{
    public class LoginCallResult
    {

        public LoginCallResult(LoginResultEnum loginResult)
        {
            LoginResult = loginResult;
        }

        public LoginCallResult(string authToken)
            : this(LoginResultEnum.Ok)
        {
            AuthToken = authToken;
        }

        public LoginResultEnum LoginResult { get; }

        public string AuthToken { get; }

        public enum LoginResultEnum
        {
            Ok,
            IncorrectCredentials,
            UnknownError
        }
    }
}