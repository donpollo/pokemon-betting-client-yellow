using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using PokemonBetting.Client.Providers;
using Xamarin.Forms;

namespace PokemonBetting.Client.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly IAuthProvider _authProvider;

        public MainPage(IUnityContainer container)
        {
            _authProvider = container.Resolve<IAuthProvider>();

            InitializeComponent();
        }
    }
}
