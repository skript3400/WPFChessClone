using WPFChessClone.Logic;
using WPFChessClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace WPFChessClone.Data
{
    public enum EngineEventType
    {
        BOARD_CHANGED,
        CHANGE_TURNS,
        INIT_BOARD,
        POSSIBLE_MOVE,
        PROMOTION_DIALOG
    }
    public class EngineEventData
    {
        public List<Field> fields;
        public Move lastMove;
        public EngineEventType type { get; private set; }
        public Board board { get; private set; }
        public ChessColor color { get; private set; }
        public EngineEventData(EngineEventType typeIn, Board boardIn, Move moveIn)
        {
            type = typeIn;
            board = new Board(boardIn);
            lastMove = moveIn;
        }
        public EngineEventData(EngineEventType typeIn, List<Field> fieldsIn)
        {
            type = typeIn;
            fields = fieldsIn;
        }
        public EngineEventData(EngineEventType typeIn, ChessColor colorIn)
        {
            type = typeIn;
            color = colorIn;
        }
    }
}
