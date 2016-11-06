using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBetting.Client.Models
{
    public class Winner
    {
        [JsonProperty("trainer_id")]
        public int Id { get; set; }
        [JsonProperty("trainer")]
        public string Name { get; set; }
    }
}
