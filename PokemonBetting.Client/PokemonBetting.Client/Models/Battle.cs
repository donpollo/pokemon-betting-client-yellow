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
        [JsonProperty("id")]
        public int Id { get; set; }
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

		public int Pot1 { get; set; } = 0;
		public int Pot2 { get; set; } = 0;

		public string potRatio {get
			{
				int min = Math.Min(Pot1, Pot2);
				int p1 = Pot1;
				int p2 = Pot2;
				if (!min.Equals(0))
				{
					p1 = p1 / min;
					p2 = p2 / min;
				}
				return "" + p1 + " : " + p2;
			}
		}

		public string toString { get { return Team1.Trainer.Name + " vs " + Team2.Trainer.Name; }  }
    }
}
