using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Result
    {
        public Player Winner { get; }
        public Ending Reason { get; }

        public Result(Player winner, Ending reason)
        {
            Winner = winner;
            Reason = reason;
        }

        //Types of ending for the game...Checkmate and Draw
        public static Result Win (Player winner)
        {
            return new Result(winner, Ending.Checkmate);
        }
        public static Result Draw (Ending reason)
        {
            return new Result(Player.none, reason);
        }

    }
}
