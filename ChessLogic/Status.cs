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
    }
}
