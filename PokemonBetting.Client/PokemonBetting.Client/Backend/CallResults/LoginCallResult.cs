namespace PokemonBetting.Client.Backend.CallResults
{
    public class LoginCallResult :
        GenericCallResult<LoginCallResult.LoginResultEnum>
    {
        public LoginCallResult(LoginResultEnum loginResult) : base(loginResult) { }

        public LoginCallResult(string authToken)
            : base(LoginResultEnum.Ok)
        {
            AuthToken = authToken;
        }

        public string AuthToken { get; }

        public enum LoginResultEnum
        {
            Ok,
            IncorrectCredentials,
            UnknownError
        }
    }
}