using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        private readonly Direction forward;

        public Pawn(Player color)
        {
            Color = color;

            if(color == Player.white)
            {
                forward = Direction.Up;
            }
            else if(color == Player.black)
            {
                forward = Direction.Down;
            }
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static bool CanMove(Position pos, ChessBoard board)
        {
            return ChessBoard.IsInside(pos) && board.IsEmpty(pos);
        }

        private bool CanCapture(Position pos, ChessBoard board)
        {
            if(!ChessBoard.IsInside(pos) || board.IsEmpty(pos))
            {
                return false;
            }
            return board[pos].Color != Color;
        }

        //forward moves for pawns
        private IEnumerable<Move>Forward(Position from, ChessBoard board)
        {
            Position oneSquare = from + forward;
             if (CanMove(oneSquare, board))
             {
                yield return new Normal(from, oneSquare);

                Position twoSquares = oneSquare + forward;

                if(!HasMoved && CanMove(twoSquares, board))
                {
                    yield return new Normal(from, twoSquares);
                }
             }
        }

        //capture moves for pawns
        private IEnumerable<Move>Diagonal(Position from, ChessBoard board)
        {
            foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right })
            {
                Position to = from + forward + dir;

                if(CanCapture(to, board))
                {
                    yield return new Normal(from, to);
                }
            }
        }

        public override IEnumerable<Move> getMoves(Position from, ChessBoard board)
        {
            return Forward(from, board).Concat(Diagonal(from, board));
        }

        public override bool PossibleKingCapture (Position from, ChessBoard board)
        {
            return Diagonal(from, board).Any(move =>
            {
                Piece piece = board[move.To];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
