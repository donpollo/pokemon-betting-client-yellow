using PropertyChanged;

namespace PokemonBetting.Client.Models
{
    public class User : ModelBase<User>
    {
        public int? Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public int Balance { get; private set; }
    }
}