using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokemonBetting.Client.Views
{
    class EmailValidatorBehaviour:Behavior<Entry>
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";


        //bool isValid = false;
        public static readonly BindableProperty isValidProperty = BindableProperty.Create("isValid", typeof(bool), typeof(EmailValidatorBehaviour), false);

        public bool isValid
        {
            get { return (bool)GetValue(isValidProperty); }
            set { SetValue(isValidProperty, value); }
        }


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
            isValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;

        }



    }
}
