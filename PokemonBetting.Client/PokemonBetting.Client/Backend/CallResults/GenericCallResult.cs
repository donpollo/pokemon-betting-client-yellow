namespace PokemonBetting.Client.Backend.CallResults
{
    public class GenericCallResult<T> where T : struct
    {
        protected GenericCallResult(T result)
        {
            Result = result;
        }

        public T Result { get; }
    }
}