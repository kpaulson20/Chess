using ChessLogic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Promotion : Move
    {
        public override MoveType Type => MoveType.pawnPromote;
        public override Position From { get; }
        public override Position To { get; }
        
        public readonly  PieceType newChoiceType;

        public Promotion(Position from, Position to, PieceType newChoiceType)
        {
            From = from;
            To = to;
            this.newChoiceType = newChoiceType;
        }

        private Piece PromotionPiece(Player color)
        {
            return newChoiceType switch
            {
                PieceType.Knight => new Knight(color),
                PieceType.Bishop => new Bishop(color),
                PieceType.Rook => new Rook(color),
                _ => new Queen(color)
            };
        }

        public override void Excecute(ChessBoard board)
        {
            Piece pawn = board[From];
            board[From] = null;

            Piece promotionPiece = PromotionPiece(pawn.Color);
            promotionPiece.HasMoved = true;
            board[To] = promotionPiece;
        }
    }
}
