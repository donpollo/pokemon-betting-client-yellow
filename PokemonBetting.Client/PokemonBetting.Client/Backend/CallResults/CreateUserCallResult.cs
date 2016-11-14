namespace PokemonBetting.Client.Backend.CallResults
{
    public class CreateUserCallResult :
        GenericCallResult<CreateUserCallResult.CreateUserResultEnum>
    {
        public CreateUserCallResult(CreateUserResultEnum result) : base(result) { }

        public enum CreateUserResultEnum
        {
            Ok,
            UnknownError
        }
    }
}