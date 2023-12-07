using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Normal : Move
    {
        public override MoveType Type => MoveType.normal;
        public override Position From { get; }
        public override Position To { get; }

        public Normal(Position from, Position to)
        {
            From = from;
            To = to;
        }

        public override void Excecute(ChessBoard board)
        {
            Piece piece = board[From];
            board[To] = piece;
            board[From] = null;
            piece.HasMoved = true;
        }
    }
}
