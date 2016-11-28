using PokemonBetting.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokemonBetting.Client.Helpers.DataTemplateSelectors
{
    public class WinnerDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NoWinnerTemplate { get; set; }
        public DataTemplate Team1WinnerTemplate { get; set; }
        public DataTemplate Team2WinnerTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var battle = item as Battle;
            if (battle == null) return null;

            if (battle.Winner == null)
                return NoWinnerTemplate;
            else if (battle.Winner.Id == battle.Team1.Trainer.Id)
                return Team1WinnerTemplate;
            else
                return Team2WinnerTemplate;
        }
    }
}
