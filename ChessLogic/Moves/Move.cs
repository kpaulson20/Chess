﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Position From { get; }
        public abstract Position To { get; }

        public abstract void Excecute(ChessBoard board);

        public virtual bool MoveIsLegal(ChessBoard board)
        {
            Player player = board[From].Color;
            ChessBoard boardCopy = board.Copy();
            Excecute(boardCopy);
            return !boardCopy.IsInCheck(player);
        }
    }
}
