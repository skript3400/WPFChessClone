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
    public abstract class Piece 
    {


        private ChessColor _color;
        public ChessColor color
        {
            get { return _color; }
            set
            {
                if (value == _color) return;
                _color = value;
            }
        }

        private Type _type;
        public Type type
        {
            get { return _type; }
            set
            {
                if (value == _type) return;
                _type = value;
            }
        }

        public bool hasMoved { get; set; } = false;

        protected Piece(ChessColor c)
        {
            color = c;
        }
   
        public enum Type
        {
            Rook,
            Knight,
            Bishop,
            Queen,
            King,
            Pawn
        }

    }
}
