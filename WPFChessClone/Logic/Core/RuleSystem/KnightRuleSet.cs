using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WPFChessClone.Data;
using WPFChessClone.Model;

namespace WPFChessClone.Logic.Core.RuleSystem
{
    public class KnightRuleSet : RuleSet
    {
        public KnightRuleSet(Board board) : base(board) { }
        public override bool isChecked(Board board)
        {
            throw new NotImplementedException("Can't call isChecked for Knight");
        }
        public override List<Move> getMoves(Field field, bool isValidation)
        {
            List<Move> result = new List<Move>();
            Piece piece = field.piece;
            int x = field.x;
            int y = field.y;
            List<Coordinates> coords = new List<Coordinates>();
            coords.Add(new Coordinates(x + 2, y + 1));
            coords.Add(new Coordinates(x + 2, y - 1));
            coords.Add(new Coordinates(x - 2, y + 1));
            coords.Add(new Coordinates(x - 2, y - 1));

            coords.Add(new Coordinates(x + 1, y + 2));
            coords.Add(new Coordinates(x - 1, y + 2));
            coords.Add(new Coordinates(x + 1, y - 2));
            coords.Add(new Coordinates(x - 1, y - 2));

            foreach (Coordinates c in coords)
            {
                if (inBounds(c.x, c.y))
                {
                    Field tmpF = _board.getField(c);
                    if (isValid(tmpF, piece))
                    {
                        result.Add(new Move(field, tmpF, piece));
                    }
                }
            }

            if (isValidation) return Validator.validate(_board, result);
            return result;
        }
        private bool inBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x <= 7 && y <= 7;
        }

        private bool isValid(Field f, Piece piece)
        {
            return (f.isEmpty || (!f.isEmpty && f.piece.color != piece.color));
        }

        public override Piece.Type Type { get { return Piece.Type.Knight; } }
    }
}
