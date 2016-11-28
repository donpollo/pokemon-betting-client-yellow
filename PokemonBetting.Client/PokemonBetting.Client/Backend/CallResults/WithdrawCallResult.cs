namespace PokemonBetting.Client.Backend.CallResults
{
    public class WithdrawCallResult : GenericCallResult<WithdrawCallResult.WithdrawCallResultEnum>
    {
        public WithdrawCallResult(WithdrawCallResultEnum result) : base(result) {}

        public enum WithdrawCallResultEnum
        {
            NoContent = 204,
            BadRequest = 400,
            InternalServerError = 500
        }
    }
}
