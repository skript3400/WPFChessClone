using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFChessClone.Model;
using WPFChessClone.Data;
using System.Windows.Input;

namespace WPFChessClone.Logic.Core.RuleSystem
{
    public class PawnRuleSet : RuleSet
    {
        public PawnRuleSet(Board board) : base(board) { }
        public override bool isChecked(Board board)
        {
            throw new NotImplementedException("Can't call isChecked for Pawn");
        }
        public override List<Move> getMoves(Field field, bool isValidation)
        {
            ChessColor color = field.piece.color;
            List<PathGetter.Achsis> achses = new List<PathGetter.Achsis>();
            achses.Add(PathGetter.Achsis.VERTICAL);
            List<Move> result = new List<Move>();

            //get movement straight ahead
            int d = 1;
            if (color == ChessColor.Black) d = -1;
            int ny = field.y + d;
            if (inBounds(ny))
            {
                Field tmp = _board.getField(field.x, ny);
                if (tmp.isEmpty)
                {
                    result.Add(new Move(field, tmp, field.piece));
                }
            }

            //get optional diagonal fields
            d = 1;
            if (color == ChessColor.Black) d = -1;
            ny = field.y + d;
            int nx = field.x + d;
            if(inBounds(ny) && inBounds(nx))
            {
                Field tmp = _board.getField(nx, ny);
                if (!tmp.isEmpty && tmp.piece.color != color) {
                    result.Add(new Move(field, tmp, field.piece));
                }
            }
            ny = field.y + d;
            nx = field.x - d;
            if (inBounds(nx) && inBounds(ny))
            {
                Field tmp = _board.getField(nx, ny);
                if (!tmp.isEmpty && tmp.piece.color != color) {
                    result.Add(new Move(field, tmp, field.piece));
                } 
            }

            //get optional y+-2 start field
            if (!field.piece.hasMoved)
            {
                d = 1;
                if (color == ChessColor.Black) d = -1;
                if (_board.getField(field.x, field.y + d).isEmpty)
                {
                    d = 2;
                    if (color == ChessColor.Black) d = -2;
                    ny = field.y + d;
                    if (inBounds(ny))
                    {
                        Field tmp = _board.getField(field.x, ny);
                        if (tmp.isEmpty) result.Add(new Move(field, tmp, field.piece));
                    }
                }
            }

            if(isValidation) return Validator.validate(_board,result);
            return result;
        }
        private bool inBounds(int t)
        {
            return t>=0 && t<8;
        }
        public override Piece.Type Type { get { return Piece.Type.Pawn; } }

    }
}
