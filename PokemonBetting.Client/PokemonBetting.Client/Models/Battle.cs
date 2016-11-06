using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBetting.Client.Models
{
    public class Battle
    {
        [JsonProperty("end_time")]
        public string EndTime { get; set; }
        [JsonProperty("start_time")]
        public string StartTime { get; set; }
        [JsonProperty("team1")]
        public Team Team1 { get; set; }
        [JsonProperty("team2")]
        public Team Team2 { get; set; }
        [JsonProperty("winner")]
        public Winner Winner { get; set; }

        public string toString { get { return Team1.Trainer.Name + " vs " + Team2.Trainer.Name; }  }
    }
}
