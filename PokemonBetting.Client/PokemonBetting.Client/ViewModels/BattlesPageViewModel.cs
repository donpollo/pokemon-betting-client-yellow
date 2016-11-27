using PokemonBetting.Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using PokemonBetting.Client.Views;
using PropertyChanged;
using Xamarin.Forms;

namespace PokemonBetting.Client.ViewModels
{
    [ImplementPropertyChanged]
    public class BattlesPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public ObservableCollection<Battle> Battles { get; protected set; }
       
        public DelegateCommand PreviousCommand { get; }
        public DelegateCommand NextCommand { get; }
        public DelegateCommand GoBackCommand { get; }

        public Command UnfinishedBattleOnTapCommand { get; }

        public int PageNumber => Offset / Limit;
        protected int Offset = 0;
        protected const int Limit = 10;
        protected bool IsFinished = true;


        public BattlesPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Battles = new ObservableCollection<Battle>();

            PreviousCommand = new DelegateCommand(PreviousBattles);
            NextCommand = new DelegateCommand(NextBattles);
            GoBackCommand = new DelegateCommand(GoBack);

            UnfinishedBattleOnTapCommand = new Command(async battle => await this.NavigateToBattle(battle as Battle));

            GetBattles();
        }

        private async Task NavigateToBattle(Battle battle)
        {
            var navigationParameters = new NavigationParameters { ["Battle"] = battle };
            await _navigationService.NavigateAsync(nameof(BattleLogPage), navigationParameters);
        }

        private void NextBattles()
        {
            Offset += Limit;
            OnPropertyChanged(nameof(PageNumber));
            GetBattles();
        }

        private void PreviousBattles()
        {
            Offset -= Limit;
            if (Offset < 0)
            {
                Offset = 0;
            }
            OnPropertyChanged(nameof(PageNumber));
            GetBattles();
        }

        private async void GoBack()
        {
            await _navigationService.GoBackAsync();
        }


        protected async void GetBattles()
        {
            var httpClient = new HttpClient();
            //HttpResponseMessage response = await httpClient.GetAsync(("http://pokemon-battle.bid/api/v1/battles/?limit="+limit+"&offset="+offset+"&is_finished=true"));
            var response = await httpClient.GetAsync("http://163.172.151.151:5000/battles/limit=" + Limit + "&offset=" + Offset + "&is_finished="+IsFinished);

            var responseString = await response.Content.ReadAsStringAsync();
            var battles = Battle.FromJsonList(responseString);

            Battles = new ObservableCollection<Battle>(battles);
        }

        /*protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }*/
    }
}
