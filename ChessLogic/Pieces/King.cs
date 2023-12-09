using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic.Pieces
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }

        private static readonly Direction[] dirs = new Direction[]
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

        public King(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private IEnumerable<Position> MovePositions(Position from, ChessBoard board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;

                if (!ChessBoard.IsInside(to))
                {
                    continue;
                }
                if (board.IsEmpty(to) || board[to].Color != Color)
                {
                    yield return to;
                }
            }
        }

        public override IEnumerable<Move> getMoves(Position from, ChessBoard board)
        {
            foreach (Position to in MovePositions(from, board))
            {
                yield return new Normal(from, to);
            }
        }

        public override bool PossibleKingCapture (Position from, ChessBoard board)
        {
            return MovePositions(from, board).Any(to =>
            {
                Piece piece = board[to];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
