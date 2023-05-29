using WPFChessClone.Data;
using WPFChessClone.Logic.Core.RuleSystem;
using WPFChessClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFChessClone.Logic.Core
{
    public static class Utils
    {
        

        public static bool isChecked(Board board, ChessColor color)
        {
            Coordinates kingCoord = board.whiteKing.Coordinates;
            if (color == ChessColor.Black) kingCoord = board.blackKing.Coordinates;

            List<Field> newFields = getEnemyFields(board, color);
            foreach (Field f in newFields)
            {
                RuleSet ruleSet = getRuleSet(f.piece, board);
                List<Move> enemyMoves = ruleSet.getMoves(f, false);
                foreach (Move enemyMove in enemyMoves)
                {
                    if (color == ChessColor.White)
                    {
                        if (enemyMove.targetField.Coordinates == kingCoord) return true;
                    }
                    if (color == ChessColor.Black)
                    {
                        if (enemyMove.targetField.Coordinates == kingCoord) return true;
                    }
                }
            }
            return false;
        }

        public static List<Field> getEnemyFields(Board board, ChessColor color)
        {
            List<Field> result = new List<Field>();
            foreach (Field f in board.fields)
            {
                if (!f.isEmpty && f.piece.color != color)
                {
                    result.Add(f);
                }
            }
            return result;
        }

        public static RuleSet getRuleSet(Piece piece, Board board)
        {
            RuleSet result;
            switch (piece.type)
            {
                case Piece.Type.Rook:
                    result = new RookRuleSet(board);
                    break;
                case Piece.Type.Knight:
                    result = new KnightRuleSet(board);
                    break;
                case Piece.Type.Bishop:
                    result = new BishopRuleSet(board);
                    break;
                case Piece.Type.Queen:
                    result = new QueenRuleSet(board);
                    break;
                case Piece.Type.King:
                    result = new KingRuleSet(board);
                    break;
                case Piece.Type.Pawn:
                    result = new PawnRuleSet(board);
                    break;
                default:
                    throw new ArgumentException("Piece type not found");
            }
            return result;
        }

        public static Board move(Board board, Move move, bool isSimulated)
        {
            Coordinates start = move.startField.Coordinates;
            Coordinates target = move.targetField.Coordinates;
            Piece piece = move.piece;

            //update piece and fields
            if (!isSimulated) piece.hasMoved = true;
            board.getField(target).piece = piece;
            board.getField(start).piece = null;

            //update King field
            if (piece.type == Piece.Type.King)
            {
                if (piece.color == ChessColor.Black)
                {
                    board.blackKing = board.getField(target);
                }
                if (piece.color == ChessColor.White)
                {
                    board.whiteKing = board.getField(target);
                }
            }

            return board;
        }
    }
}
