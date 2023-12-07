using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic.Pieces
{
    public class Queen : Piece
    {
        public override PieceType Type => PieceType.Queen;
        public override Player Color { get; }

        public static readonly Direction[] dirs = new Direction[]
        {
            Direction.Up,
            Direction.Down,
            Direction.Left,
            Direction.Right,
            Direction.Diagonal1,
            Direction.Diagonal2,
            Direction.Diagonal3,
            Direction.Diagonal4
        }; 

        public Queen(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move> getMoves(Position from, ChessBoard board)
        {
            return MoveInDirection(from, board, dirs).Select(to => new Normal(from, to));
        }
    }
}
