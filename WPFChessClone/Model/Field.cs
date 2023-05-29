using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPFChessClone.Data;

namespace WPFChessClone.Model
{
    public class Field 
    {
        public Field(Coordinates cIn, Piece piece = null)
        {
            Coordinates = cIn;
            _piece = piece;
        }

        #region Members
        public Coordinates Coordinates { get; }

        public int x { get { return Coordinates.x; } }
        public int y { get { return Coordinates.y; } }

        private Piece _piece;
        public Piece piece { get { return _piece; } set { _piece = value;} }
        #endregion

        public bool isEmpty { get { return _piece == null; } }
    }
}
