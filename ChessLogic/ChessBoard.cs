using ChessLogic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class ChessBoard
    {
        private readonly Piece[,] pieces = new Piece[8, 8];
        
        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }

        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }

        public static ChessBoard Initial()
        {
            ChessBoard board = new ChessBoard();
            board.AddStartPieces();
            return board;
        }

        private void AddStartPieces()
        {
            // Adding pieces (except pawns) to the board
            this[0, 0] = new Rook(Player.black);
            this[0, 1] = new Knight(Player.black);
            this[0, 2] = new Bishop(Player.black);
            this[0, 3] = new Queen(Player.black);
            this[0, 4] = new King(Player.black);
            this[0, 5] = new Bishop(Player.black);
            this[0, 6] = new Knight(Player.black);
            this[0, 7] = new Rook(Player.black);

            this[7, 0] = new Rook(Player.white);
            this[7, 1] = new Knight(Player.white);
            this[7, 2] = new Bishop(Player.white);
            this[7, 3] = new Queen(Player.white);
            this[7, 4] = new King(Player.white);
            this[7, 5] = new Bishop(Player.white);
            this[7, 6] = new Knight(Player.white);
            this[7, 7] = new Rook(Player.white);

            // Adding all pawns to the board
            for (int c = 0; c < 8; c++)
            {
                this[1, c] = new Pawn(Player.black);
                this[6, c] = new Pawn(Player.white);
            }
        }

        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }

        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }
    }
}
