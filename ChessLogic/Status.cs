using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Status
    {
        public ChessBoard Board { get; }
        public Player CPlayer { get; private set; }

        public Status(Player player, ChessBoard board)
        {
            CPlayer = player;
            Board = board;
        }
        public IEnumerable<Move> LegalMoves(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            IEnumerable<Move> moveCandidates = piece.getMoves(pos, Board);
            return moveCandidates.Where(move => move.MoveIsLegal(Board));
        }

        public void MakeMove(Move move)
        {
            move.Excecute(Board);
            CPlayer = CPlayer.Opponent();
        }
    }
}
