using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBetting.Client.Models
{
    public class Team
    {
        [JsonProperty("trainer")]
        public Trainer Trainer { get; set; }
        [JsonProperty("pokemons")]
        public Object Pokemons { get; set; }
    }
}
