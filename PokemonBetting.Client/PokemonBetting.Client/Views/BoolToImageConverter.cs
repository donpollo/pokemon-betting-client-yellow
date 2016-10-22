using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokemonBetting.Client.Views
{
    class BoolToImageConverter : IValueConverter
    {
        /*public Image FalseObject { set; get; }
        public Image TrueObject { set; get; }
        */
        public Image FalseObject = new Image { Source = ImageSource.FromResource("PokemonBetting.Client.cross.png"), IsVisible=true };
        public Image TrueObject = new Image { Source = ImageSource.FromResource("PokemonBetting.Client.tick.png"), IsVisible=true };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? this.TrueObject : this.FalseObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Image)value).Equals(this.TrueObject);
        }

    }
}
