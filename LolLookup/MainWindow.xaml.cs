using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RiotSharp;
using RiotSharp.SummonerEndpoint;

namespace LolLookup
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // aqui logaria, mas tou sem vps nem nenhum tipo de database...
            if (PwBox.Password == "admin")
            {
                RiotApi riot = new RiotApi(new RiotSharp.Http.RateLimitedRequester(
                    Globals.RiotAPIKey, 
                    new Dictionary<TimeSpan, int> {
                        { TimeSpan.FromSeconds(1), 20 },
                        { TimeSpan.FromMinutes(2), 100 }
                    }
                ));

                Summoner player = riot.GetSummonerByName(RiotSharp.Misc.Region.br, UserBox.Text);
                Globals.summoner = player;
                Hide();
                Window1 newWindow = new Window1();
                List<RiotSharp.LeagueEndpoint.LeaguePosition> leagues = riot.GetLeaguePositions(RiotSharp.Misc.Region.br, player.Id);
                Dictionary<string, RiotSharp.LeagueEndpoint.LeaguePosition> leaguedict = new Dictionary<string, RiotSharp.LeagueEndpoint.LeaguePosition>();
                foreach(RiotSharp.LeagueEndpoint.LeaguePosition league in leagues)
                {
                    leaguedict.Add(league.QueueType, league);
                }

                
                
                newWindow.profileIconImage.Source = new BitmapImage(new Uri(string.Format("http://ddragon.leagueoflegends.com/cdn/8.20.1/img/profileicon/{0}.png", new[] { player.ProfileIconId.ToString() })));
                newWindow.SummonerNameLabel.Text = string.Format("{1}\nLevel {0}", new [] {player.Level.ToString(),player.Name});
                newWindow.Show();
                
                
            }                
            else
            {
                MaterialDesignThemes.Wpf.DialogHost.Show("Erro: senha incorreta.");
            }
            MaterialDesignThemes.Wpf.DialogHost.Show("Erro: senha incorreta.");

        }
    }
}
