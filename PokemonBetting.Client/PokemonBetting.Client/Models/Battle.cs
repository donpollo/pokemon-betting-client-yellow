using System;
using Newtonsoft.Json;

namespace PokemonBetting.Client.Models
{
    public class Battle : ModelBase<Battle>
    {
        public int Id { get; private set; }

        public string EndTime { get; private set; }

        public string StartTime { get; private set; }

        public DateTime StartDateTime => DateTime.Parse(StartTime, null);

        public Team Team1 { get; private set; }

        public Team Team2 { get; private set; }

        public Winner Winner { get; private set; }
	
		public int Pot1 { get; set; } = 0;
	
		public int Pot2 { get; set; } = 0;

		public string potRatio {get{
			int min = Math.Min(Pot1, Pot2);
			int p1 = Pot1;
			int p2 = Pot2;
			if (!min.Equals(0))
			{
				p1 = p1 / min;
				p2 = p2 / min;
			}
			return "" + p1 + " : " + p2;
		}}
    }
}
