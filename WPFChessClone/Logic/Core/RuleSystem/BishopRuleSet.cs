using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFChessClone.Model;

namespace WPFChessClone.Logic.Core.RuleSystem
{
    public class BishopRuleSet : RuleSet
    {
        public BishopRuleSet(Board board) : base(board) { }

        public override bool isChecked(Board board)
        {
            throw new NotImplementedException("Can't call isChecked for Bishop");
        }
        public override List<Move> getMoves(Field field, bool isValidation)
        {
            List<PathGetter.Achsis> achses = new List<PathGetter.Achsis>();
            achses.Add(PathGetter.Achsis.DIAGONAL);
            List<Move> result = _pathGetter.get(_board, field, false, achses);
            if (isValidation) return Validator.validate(_board, result);
            return result;
        }
        public override Piece.Type Type { get { return Piece.Type.Bishop; } }
    }
}
