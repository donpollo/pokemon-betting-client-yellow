using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBetting.Client.Models
{
    public class Team : ModelBase<Team>
    {
        public Trainer Trainer { get; private set; }

        public List<Pokemon> Pokemons { get; private set; }
    }
}
