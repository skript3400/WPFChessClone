using WPFChessClone.Data;
using WPFChessClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFChessClone.Display
{
    class BoardViewController
    {
        private FieldView _lastSelected;
        private FieldView _lastBlackKing;
        private FieldView _lastWhiteKing;
        private Move _lastMove;
        private List<FieldView> _lastMarked = new List<FieldView>();
        private FieldView[,] _fieldViews;
        public BoardViewController(FieldView[,] fieldViews)
        {
            _fieldViews = fieldViews;
        }

        public void fieldClicked(Field field, FieldView fieldView)
        {
            if(_lastSelected != null&&_lastSelected.IsSelected)
            {
                _lastSelected.IsSelected = false;
            }
            fieldView.IsSelected = true;
            _lastSelected = fieldView;
        }

        public void piecesChanged(Board board)
        {
           clearMarked();
           for (int x = 0; x < 8; x++)
            {
                for(int y = 0; y < 8; y++)
                {
                    Field f = board.getField(x, y);
                    _fieldViews[x, y].updateField(f);
                }
            }
            handleCheckColor(board);
        }

        private void handleCheckColor(Board board)
        {
            if (board.whiteChecked)
            {
                FieldView fw = _fieldViews[board.whiteKing.x, board.whiteKing.y];
                fw.IsCheckd = true;
                _lastWhiteKing = fw;
            }
            else
            {
                _fieldViews[board.whiteKing.x, board.whiteKing.y].IsCheckd = false;
                if (_lastWhiteKing != null && _lastWhiteKing.IsCheckd) {
                    _lastWhiteKing.IsCheckd = false;
                    _lastWhiteKing = null;
                }
            }
            if (board.blackChecked)
            {
                FieldView fw = _fieldViews[board.blackKing.x, board.blackKing.y];
                fw.IsCheckd = true;
                _lastBlackKing = fw;
            }
            else
            {
                _fieldViews[board.blackKing.x, board.blackKing.y].IsCheckd = false;
                if (_lastBlackKing != null && _lastBlackKing.IsCheckd)
                {
                    _lastBlackKing.IsCheckd = false;
                    _lastBlackKing = null;
                }

            }
        }

        public void handleLastSelected(Move lastMove)
        {
            if(_lastMove != null) {
                Coordinates sf = _lastMove.startField.Coordinates;
                Coordinates tf = _lastMove.targetField.Coordinates;
                _fieldViews[sf.x, sf.y].MovedLast = false;
                _fieldViews[tf.x, tf.y].MovedLast = false;
            }
            if (lastMove != null)
            {
                Coordinates sf = lastMove.startField.Coordinates;
                Coordinates tf = lastMove.targetField.Coordinates;
                _fieldViews[sf.x, sf.y].MovedLast = true;
                _fieldViews[tf.x, tf.y].MovedLast = true;
            }
            _lastMove = lastMove;
        }

        public void possibleMoves(List<Field> fields)
        {
            foreach(Field f in fields)
            {
                FieldView fView = _fieldViews[f.x, f.y];
                fView.IsMarked = true;
                _lastMarked.Add(fView);
            }
        }

        public Move createMove(Board board,Field target)
        {
            Field startField = board.getField(_lastSelected.Coordinates);
            return new Move(startField, target, startField.piece);
        }

        public void clearMarked()
        {
            foreach (FieldView fview in _lastMarked)
            {
                if (fview.IsMarked)
                {
                    fview.IsMarked = false;
                }
            }
            _lastMarked.Clear();
        }
    }
}
