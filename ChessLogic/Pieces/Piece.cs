using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }
        public bool HasMoved { get; set; } = false;

        public abstract Piece Copy();

        public abstract IEnumerable<Move> getMoves(Position from, ChessBoard board);

        //Movement for Rook, Bishop and Queen
        protected IEnumerable<Position> MoveInDirection(Position from, ChessBoard board, Direction dir)
        {
            for(Position pos = from + dir; ChessBoard.IsInside(pos); pos+= dir)
            {
                if (board.IsEmpty(pos))
                {
                    yield return pos;
                    continue;
                }
                Piece piece = board[pos];

                if(piece.Color != Color)
                {
                    yield return pos;
                }
                yield break;
            }
        }

        protected IEnumerable<Position>MoveInDirection(Position from, ChessBoard board, Direction[] dirs)
        {
            //find all reachable positions for the chosen piece
            return dirs.SelectMany (dir => MoveInDirection(from, board, dir));
        }

        public virtual bool PossibleKingCapture(Position from, ChessBoard board)
        {
            return getMoves(from, board).Any(move =>
            {
                Piece piece = board[move.To];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
