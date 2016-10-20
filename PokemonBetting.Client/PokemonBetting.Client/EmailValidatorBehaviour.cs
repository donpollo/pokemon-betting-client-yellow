using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokemonBetting.Client
{
    class EmailValidatorBehaviour:Behavior<Entry>
    {
        bool isValid = false;


        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            // Perform setup
            bindable.TextChanged += markEntry;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            // Perform clean up
            bindable.TextChanged -= markEntry;
        }

        private void markEntry(object sender, TextChangedEventArgs e)
        {
            Entry s = (Entry)sender;
            if (e.NewTextValue.Length > 5)
            {
                s.TextColor = Color.Green;
            }
            else
                s.TextColor = Color.Red;

        }



    }
}
