namespace PokemonBetting.Client.Models
{
    public class Pokemon : ModelBase<Pokemon>
    {
        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}