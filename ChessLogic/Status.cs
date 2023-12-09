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
        public Result result { get; private set; } = null;

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
            CheckForEnd();
        }

        public IEnumerable<Move> AllLegalPlayerMoves(Player player)
        {
            IEnumerable<Move> moveCandidates = Board.PositionForColor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.getMoves(pos, Board);
            });

            return moveCandidates.Where(move => move.MoveIsLegal(Board));
        }

        private void CheckForEnd()
        {
            if (!AllLegalPlayerMoves(CPlayer).Any())
            {
                if (Board.IsInCheck(CPlayer))
                {
                    result = Result.Win(CPlayer.Opponent()); 
                }
                else
                {
                    result = Result.Draw(Ending.Stalemate);
                }
            }
        }

        public bool IsOver()
        {
            return result != null;
        }
    }
}
