using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFChessClone.Data;

namespace WPFChessClone.Model
{
    public class Pawn : Piece
    {
        public Pawn(ChessColor c) : base(c)
        {
            color = c;
            type = Type.Pawn;
        }
    }
}
