namespace PokemonBetting.Client.Models
{
    public class User : ModelBase<User>
    {
        public int? Id { get; }
        public string Username { get; }
        public string Email { get; }
        public int Balance { get; }
    }
}