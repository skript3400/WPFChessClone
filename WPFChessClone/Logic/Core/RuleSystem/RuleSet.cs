using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFChessClone.Model;

namespace WPFChessClone.Logic.Core.RuleSystem
{
    public abstract class RuleSet
    {
        protected Board _board;
        protected PathGetter _pathGetter = new PathGetter();
        protected RuleSet(Board board) { _board = board; }
        public abstract Piece.Type Type { get; }
        public abstract List<Move> getMoves(Field field, bool isValidation);

        public abstract bool isChecked(Board board);
    }
}
