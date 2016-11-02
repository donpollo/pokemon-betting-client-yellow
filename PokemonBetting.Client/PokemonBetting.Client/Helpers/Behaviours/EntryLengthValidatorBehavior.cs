using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace PokemonBetting.Client.Helpers.Behaviours
{
    public class EntryLengthValidatorBehavior : Behavior<Entry>
    {
        public int MaxLength { get; set; } = 100;

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            // Perform setup
            bindable.TextChanged += CheckEntry;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            // Perform clean up
            bindable.TextChanged -= CheckEntry;
        }

        private void CheckEntry(object sender, TextChangedEventArgs e)
        {
            var isValid = e.NewTextValue != null && e.NewTextValue.Length <= MaxLength;
            if (!isValid)
                ((Entry) sender).Text = e.OldTextValue;
        }
    }
}