using WPFChessClone.Data;
using WPFChessClone.Logic.Core.RuleSystem;
using WPFChessClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;



namespace WPFChessClone.Logic.Core
{
    public static class Validator
    {

        public static List<Move> validate(Board board, List<Move> moves)
        {
            if (moves.Count == 0) return moves;
            List<Move> result = moves.FindAll((Move m) => { return isMoveValid(board, m); } );
            return result;
        }

        //Get all possible moves for enemy positions in next move
        //if possible moves contain King field, original move was invalid
        public static bool isMoveValid(Board board, Move move)
        {
            Board tmpBrd = getNextBoard(board, move);
            List<Field> newFields = Utils.getEnemyFields(tmpBrd, move.startField.piece.color);
            foreach (Field f in newFields)
            {
                if (Utils.isChecked(tmpBrd, move.piece.color)) return false;
            }
            return true;
        }

        public static Board getNextBoard(Board board, Move move)
        {
            Board cpyBoard = new Board(board);
            cpyBoard = Utils.move(cpyBoard, move, true);
            return cpyBoard;
        }

    }
}
