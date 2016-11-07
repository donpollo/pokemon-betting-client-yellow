using PokemonBetting.Client.Models;

namespace PokemonBetting.Client.Backend.CallResults
{
    public class GetAuthenticatedUserCallResult
    {
        public GetAuthenticatedUserCallResult(
            GetAuthenticatedUserResultEnum getAuthenticatedUserResult)
        {
            GetAuthenticatedUserResult = getAuthenticatedUserResult;
        }

        public GetAuthenticatedUserCallResult(User user)
            : this(GetAuthenticatedUserResultEnum.Ok)
        {
            User = user;
        }

        public GetAuthenticatedUserResultEnum GetAuthenticatedUserResult { get; }

        public User User { get; }

        public enum GetAuthenticatedUserResultEnum
        {
            Ok,
            UnknownError
        }
    }
}