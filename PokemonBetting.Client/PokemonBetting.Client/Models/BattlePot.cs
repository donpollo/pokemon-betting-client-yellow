using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBetting.Client.Models
{
	class BattlePot : ModelBase<BattlePot>
	{
		[JsonProperty("trainerId")]
		public int TrainerId { get; set; }
		[JsonProperty("pot")]
		public int Pot { get; set; }
	}
}
