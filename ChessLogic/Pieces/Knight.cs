using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Knight : Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; }

        public Knight(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static IEnumerable<Position> PotentialMoves(Position from)
        {
            foreach(Direction vertical in new Direction[] {Direction.Up, Direction.Down})
            {
                foreach (Direction height in new Direction[] {Direction.Left, Direction.Right})
                {
                    yield return from + 2 * vertical + height;
                    yield return from + 2 * height + vertical;
                }
            }
        }

        private IEnumerable<Position>MovePositions(Position from, ChessBoard board)
        {
            return PotentialMoves(from).Where(pos => ChessBoard.IsInside(pos) 
                && (board.IsEmpty(pos) || board[pos].Color != Color));
        }

        public override IEnumerable<Move> getMoves(Position from, ChessBoard board)
        {
            return MovePositions(from, board).Select(to => new Normal(from, to));
        }
    }
}
