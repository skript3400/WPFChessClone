using WPFChessClone.Data;
using WPFChessClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WPFChessClone.Logic.Core.RuleSystem
{
    static class Castle
    {
        public static List<Move> currCastleMoves = new List<Move>();

        private static bool isLongCastle(Move move) {
            return (move.startField.x - move.targetField.x) > 0;
        }
        public static bool isCastle(Move move)
        {
            foreach (Move m in currCastleMoves)
            {
                if (m == move)
                {
                    return true;
                }
            }
            return false;
        }
        public static void execute(Board board, Move move)
        {

            if(move.piece.type == Piece.Type.King)
            {
                //Move King
                int y = move.startField.y;
                int x = 7;
                int x2 = 5;
                if ((move.startField.x - move.targetField.x) > 0) //is long castle
                {
                    x = 0;
                    x2 = 3;
                }
                Utils.move(board, move, false);

                //Move Rook
                Field rookF = board.getField(x, y);
                Move rookMove = new Move(rookF, board.getField(x2, y), rookF.piece);
                Utils.move(board, rookMove, false);
            }
            if (move.piece.type == Piece.Type.Rook)
            {
                //Move King
                Field kingF = board.whiteKing;
                if(move.piece.color == ChessColor.Black) kingF = board.blackKing;
                int y = move.startField.y;
                int x = 6;
                int x2 = 5;
                if ((move.startField.x - move.targetField.x) < 0) //is long castle
                {
                    x = 2;
                    x2 = 3;
                }
                Move kingMove = new Move(kingF, board.getField(x, y), kingF.piece);
                Utils.move(board, kingMove, false);

                //Move Rook
                Move rookMove = new Move(move.startField, board.getField(x2, y), move.piece);
                Utils.move(board, rookMove, false);
            }
        }

        public static List<Move> getMoves(Board board, ChessColor color, Piece piece)
        {
            List<Move> result = new List<Move>();
            Move m = shortValid(board, color, piece);
            if(m != null) result.Add(m);
            m = longValid(board, color, piece);
            if (m != null) result.Add(m);
            return result;
        }

        private static Move shortValid(Board board, ChessColor color, Piece piece)
        {
            if (piece.type != Piece.Type.Rook && piece.type != Piece.Type.King) return null;
            if (color == ChessColor.Black && board.blackChecked)
            {
                return null;
            }
            if (color == ChessColor.White && board.whiteChecked)
            {
                return null;
            }

            int y = 0;
            if (color == ChessColor.Black) y = 7;
            Field rookF = board.getField(7, y);
            Field kingF = board.whiteKing;
            if(color == ChessColor.Black) kingF = board.blackKing;

            if (kingF.piece.hasMoved) return null;
            if (rookF.isEmpty) return null;
            if (rookF.piece.hasMoved) return null;

            Field f1 = board.getField(6, y);
            Field f2 = board.getField(5, y);

            if (!f1.isEmpty) return null;
            if (!f2.isEmpty) return null;

            Move m1 = new Move(kingF, f1, kingF.piece);
            Move m2 = new Move(kingF, f2, kingF.piece);

            if (!Validator.isMoveValid(board, m1)) return null;
            if (!Validator.isMoveValid(board, m2)) return null;

            if (piece.type == Piece.Type.King)
            {
                return m1;
            }

            if (piece.type == Piece.Type.Rook)
            {
                Move m3 = new Move(rookF, kingF, rookF.piece);
                return m3;
            }

            throw new Exception("error determining Castle validity");
        }
        private static Move longValid(Board board, ChessColor color, Piece piece)
        {
            if(piece.type != Piece.Type.Rook && piece.type != Piece.Type.King) return null;
            if (color == ChessColor.Black && board.blackChecked)
            {
                return null;
            }
            if(color == ChessColor.White && board.whiteChecked)
            {
                return null;
            }

            
            int y = 0;
            if (color == ChessColor.Black) y = 7;
            Field rookF = board.getField(0, y);
            Field kingF = board.whiteKing;
            if (color == ChessColor.Black) kingF = board.blackKing;

            if (kingF.piece.hasMoved) return null;
            if (rookF.isEmpty) return null;       
            if (rookF.piece.hasMoved) return null;

            Field f1 = board.getField(2, y);
            Field f2 = board.getField(3, y);

            if (!f1.isEmpty) return null;
            if (!f2.isEmpty) return null;


            Move m1 = new Move(kingF, f1, kingF.piece);
            Move m2 = new Move(kingF, f2, kingF.piece);

            if (!Validator.isMoveValid(board, m1)) return null;
            if (!Validator.isMoveValid(board, m2)) return null;

            if (piece.type == Piece.Type.King) {
                return m1;
            }

            if (piece.type == Piece.Type.Rook)
            {
                Move m3 = new Move(rookF, kingF, rookF.piece);
                return m3;
            }
            throw new Exception("error determining Castle validity");
        }
    }
}
