using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessLogic;

namespace ChessUI
{
    public static class Images
    {
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            { PieceType.Pawn, Load("Assets/whitePawn.png") },
            { PieceType.Rook, Load("Assets/whiteRook.png") },
            { PieceType.Knight, Load("Assets/whiteKnight.png") },
            { PieceType.Bishop, Load("Assets/whiteBishop.png") },
            { PieceType.Queen, Load("Assets/whiteQueen.png") },
            { PieceType.King, Load("Assets/whiteKing.png") }
        };

        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            { PieceType.Pawn, Load("Assets/blackPawn.png") },
            { PieceType.Rook, Load("Assets/blackRook.png") },
            { PieceType.Knight, Load("Assets/blackKnight.png") },
            { PieceType.Bishop, Load("Assets/blackBishop.png") },
            { PieceType.Queen, Load("Assets/blackQueen.png") },
            { PieceType.King, Load("Assets/blackKing.png") }
        };
        private static ImageSource Load(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        public static ImageSource getImage(Player color, PieceType type)
        {
            return color switch
            {
                Player.white => whiteSources[type],
                Player.black => blackSources[type],
                _ => null
            };
        }

        public static ImageSource getImage(Piece piece)
        {
            if (piece == null)
            {
                return null;
            }
            return getImage(piece.Color, piece.Type);
        }
    }
}
