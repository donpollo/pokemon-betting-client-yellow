using PokemonBetting.Client.Models;

namespace PokemonBetting.Client.Backend.CallResults
{
    public class GetAuthenticatedUserCallResult :
        GenericCallResult<GetAuthenticatedUserCallResult.GetAuthenticatedUserResultEnum>
    {
        public GetAuthenticatedUserCallResult(GetAuthenticatedUserResultEnum result) : base(result) { }

        public GetAuthenticatedUserCallResult(User user)
            : base(GetAuthenticatedUserResultEnum.Ok)
        {
            User = user;
        }

        public User User { get; }

        public enum GetAuthenticatedUserResultEnum
        {
            Ok,
            UnknownError
        }
    }
}