namespace PokemonBetting.Client.Backend.CallResults
{
    public class CreateUserCallResult
    {
        public CreateUserCallResult(CreateUserResultEnum createUserResult)
        {
            CreateUserResult = createUserResult;
        }

        public CreateUserResultEnum CreateUserResult { get; }

        public enum CreateUserResultEnum
        {
            Ok,
            UnknownError
        }
    }
}