using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PokemonBetting.Client.Models
{
    public class BattleLog
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
