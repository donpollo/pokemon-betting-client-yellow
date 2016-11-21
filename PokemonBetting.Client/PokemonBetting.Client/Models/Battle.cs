using Newtonsoft.Json;

namespace PokemonBetting.Client.Models
{
    public class Battle : ModelBase<Battle>
    {
        public string EndTime { get; set; }

        public string StartTime { get; set; }

        public Team Team1 { get; set; }

        public Team Team2 { get; set; }

        public Winner Winner { get; set; }
    }
}
