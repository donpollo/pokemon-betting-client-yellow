using Newtonsoft.Json;

namespace PokemonBetting.Client.Models
{
    public class Battle : ModelBase<Battle>
    {
        public int Id { get; private set; }

        public string EndTime { get; private set; }

        public string StartTime { get; private set; }

        public Team Team1 { get; private set; }

        public Team Team2 { get; private set; }

        public Winner Winner { get; private set; }
    }
}
