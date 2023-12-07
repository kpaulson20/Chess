using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override Player Color { get; }

        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.Diagonal1,
            Direction.Diagonal2,
            Direction.Diagonal3,
            Direction.Diagonal4
        };

        public Bishop(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move>getMoves(Position from, ChessBoard board)
        {
            return MoveInDirection(from, board, dirs).Select(to => new Normal(from, to));
        }
    }
}
