namespace PokemonBetting.Client.Backend.CallResults
{
    public class DepositCallResult : GenericCallResult<DepositCallResult.DepositCallResultEnum>
    {
        public DepositCallResult(DepositCallResultEnum result) : base(result) {}

        public enum DepositCallResultEnum
        {
            NoContent = 204,
            InternalServerError = 500
        }
    }
}
