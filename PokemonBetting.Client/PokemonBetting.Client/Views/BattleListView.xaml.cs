using Xamarin.Forms;

namespace PokemonBetting.Client.Views
{
    public partial class BattleListView : ContentView
    {
        public BattleListView()
        {
            InitializeComponent();
        }

        private void BattlesView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null) return;

            listView.SelectedItem = null;
        }
    }
}