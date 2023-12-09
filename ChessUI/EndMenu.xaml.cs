using ChessLogic;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for EndMenu.xaml
    /// </summary>
    public partial class EndMenu : UserControl
    {
        public event Action<Option> SelectedOption;
        public EndMenu(Status status)
        {
            InitializeComponent();

            Result result = status.result;
            WinnerText.Text = getWinnerText(result.Winner);
            ReasonText.Text = ExplainationText(result.Reason, status.CPlayer);
        }

        private static string getWinnerText(Player Winner)
        {
            return Winner switch
            {
                Player.white => "WHITE WINS",
                Player.black => "BLACK WINS",
                _ => "DRAW"
            };
        }

        private static string PlayerName(Player player)
        {
            return player switch
            {
                Player.white => "WHITE",
                Player.black => "BLACK",
                _ => ""
            };
        }

        private static string ExplainationText(Ending reason, Player CPlayer)
        {
            return reason switch
            {
                Ending.Stalemate => $"STALEMATE: {PlayerName(CPlayer)} CANNOT MOVE",
                Ending.Checkmate => $"CHECKMATE: {PlayerName(CPlayer)} WINS",
                _ => ""
            };
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            SelectedOption?.Invoke(Option.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            SelectedOption?.Invoke(Option.Exit);
        }
    }
}
