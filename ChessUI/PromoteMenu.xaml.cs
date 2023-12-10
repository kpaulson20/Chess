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
    /// Interaction logic for PromoteMenu.xaml
    /// </summary>
    public partial class PromoteMenu : UserControl
    {
        public event Action<PieceType> UpgradePiece;
        public PromoteMenu(Player player)
        {
            InitializeComponent();

            Queen.Source = Images.getImage(player, PieceType.Queen);
            Rook.Source = Images.getImage(player, PieceType.Rook);
            Bishop.Source = Images.getImage(player, PieceType.Bishop);
            Knight.Source = Images.getImage(player, PieceType.Knight);
        }

        private void Queen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpgradePiece?.Invoke(PieceType.Queen);
        }

        private void Rook_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpgradePiece?.Invoke(PieceType.Rook);
        }

        private void Bishop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpgradePiece?.Invoke(PieceType.Bishop);
        }

        private void Knight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpgradePiece?.Invoke(PieceType.Knight);
        }
    }
}
