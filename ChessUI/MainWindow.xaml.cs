using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLogic;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieces = new Image[8, 8];

        private Status status;

        public MainWindow()
        {
            InitializeComponent();
            IntializeBoard();
            status = new Status(Player.white, ChessBoard.Initial());
            displayBoard(status.Board);
        }

        private void IntializeBoard()
        {
            for (int r = 0; r < 0; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieces[r, c] = image;
                    PieceGrid.Children.Add(image);
                }
            }
        }

        private void displayBoard(ChessBoard board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];
                    pieces[r, c].Source = Images.getImage(piece);
                }
            }
        }
    }
}