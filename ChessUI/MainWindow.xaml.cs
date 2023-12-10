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
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move>moveCache = new Dictionary<Position, Move>();

        private Status status;
        private Position chooseMove = null;

        public MainWindow()
        {
            InitializeComponent();
            IntializeBoard();
            status = new Status(Player.white, ChessBoard.Initial());
            displayBoard(status.Board);
        }

        private void IntializeBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieces[r, c] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[r, c] = highlight;
                    HighlightPosition.Children.Add(highlight);
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

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MenuOnScreen())
            {
                return;
            }

            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquareSpot(point);

            if (chooseMove == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        private Position ToSquareSpot(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }

        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = status.LegalMoves(pos);
            if (moves.Any())
            {
                chooseMove = pos;
                CacheMoves(moves);
                displayHighlights();
            }
        }

        private void OnToPositionSelected(Position pos)
        {
            chooseMove = null;
            removeHighlights();

            if (moveCache.TryGetValue(pos,out Move move))
            {
                if (move.Type == MoveType.pawnPromote)
                {
                    HandlePromotion(move.From, move.To);
                }
                else
                {
                    HandleMove(move);
                }
            }
        }

        private void HandlePromotion(Position from, Position to)
        {
            pieces[to.Row, to.Column].Source = Images.getImage(status.CPlayer, PieceType.Pawn);
            pieces[from.Row, from.Column].Source = null;

            PromoteMenu promoMenu = new PromoteMenu(status.CPlayer);
            Menu.Content = promoMenu;

            promoMenu.UpgradePiece += type =>
            {
                Menu.Content = null;
                Move promoMove = new Promotion(from, to, type);
                HandleMove(promoMove);
            };
        }

        private void HandleMove(Move move)
        {
            status.MakeMove(move);
            displayBoard(status.Board);

            if (status.IsOver())
            {
                displayEndOfGame();
            }
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach (Move move in moves)
            {
                moveCache[move.To] = move;
            }
        }
        private void displayHighlights()
        {
            Color color = Color.FromArgb(150, 125, 225, 125);

            foreach(Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush (color);
            }
        }

        private void removeHighlights()
        {
            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }

        private bool MenuOnScreen()
        {
            return Menu.Content != null;
        }

        private void displayEndOfGame()
        {
            EndMenu endMenu = new EndMenu(status);
            Menu.Content = endMenu;

            //handle user click on end or restart
            endMenu.SelectedOption += option =>
            {
                //start a new game
                if (option == Option.Restart)
                {
                    Menu.Content = null;
                    RestartGame();
                }
                //close the game in its entirety
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }

        private void RestartGame()
        {
            chooseMove = null;
            removeHighlights();
            moveCache.Clear();
            status = new Status(Player.white, ChessBoard.Initial());
            displayBoard(status.Board);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!MenuOnScreen() && e.Key == Key.Escape)
            {
                DisplayPauseMenu();
            }
        }

        private void DisplayPauseMenu()
        {
            PauseMenu pauseMenu = new PauseMenu();
            Menu.Content = pauseMenu;

            pauseMenu.Select += Option =>
            {
                Menu.Content = null;

                if (Option == Option.Restart) 
                { 
                    RestartGame(); 
                }
            };
        }
    }
}