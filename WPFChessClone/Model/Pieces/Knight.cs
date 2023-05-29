using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFChessClone.Data;

namespace WPFChessClone.Model
{
    public class Knight : Piece
    {
        public Knight(ChessColor c) : base(c)
        {
            color = c;
            type = Type.Knight;
        }
    }
}
