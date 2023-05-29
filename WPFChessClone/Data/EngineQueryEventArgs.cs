using System;
using WPFChessClone.Data;
using WPFChessClone.Logic;
using WPFChessClone.Model;

namespace WPFChessClone.Data
{
    public enum QueryType
    {
        MAKE_MOVE,
        GET_POSSIBLE_MOVES,
        QUERY_DONE,
    }
    public class EngineQueryEventArgs : EventArgs
    {
        public Field field { get; private set; }
        public Move move { get; private set; }
        public QueryType type { get; private set; }
        public Piece.Type pieceType { get; private set; }

        public EngineQueryEventArgs(Field fIn, QueryType typ)
        {
            field = fIn;
            type = typ;
        }
        public EngineQueryEventArgs(Move mIn, QueryType typ)
        {
            move = mIn;
            type = typ;
        }
        public EngineQueryEventArgs(Piece.Type typ ,QueryType tp)
        {
            type = tp;
            pieceType = typ;
        }
    }
}