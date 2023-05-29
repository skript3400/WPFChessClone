using WPFChessClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFChessClone.Logic.Core.RuleSystem
{
    static class Promotion
    {
        public static Field promotionField = null;

        public static bool canPromote(Board board, Move move)
        {
            if(move.piece.type != Piece.Type.Pawn) return false;

            int y = 0;
            if (move.piece.color == Data.ChessColor.White) y = 7;

            if (move.targetField.y == y) return true;
            return false;
        }

        public static void promote(Board board, Piece.Type type)
        {
            board.getField(promotionField.x,promotionField.y).piece.type = type;
        }
    }
}
