using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFChessClone.Logic;
using WPFChessClone.Model;
using WPFChessClone.Data;
using System.ComponentModel;
using System.Data.Common;

namespace WPFChessClone.Display
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        private Canvas _parent;
        private Grid _grid;
        private Board _board;
        private FieldView[,] _fieldViews = new FieldView[8, 8];
        private BoardViewController _boardViewController;
        private bool _isInit = false;
        public BoardView(Canvas canvas)
        {
            _parent = canvas;

            InitializeComponent();
            _grid = (Grid)this.FindName("ChessBoard");
            _grid.Background = (SolidColorBrush)Application.Current.FindResource("boardBackground");
            _grid.Width = _parent.ActualWidth;
            _grid.Height = _parent.ActualHeight;
            for (int i = 0; i < 8; i++)
            {
                _grid.ColumnDefinitions.Add(new ColumnDefinition());
                _grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        public event EventHandler<EngineQueryEventArgs> BoardEvent;
        private void onBoardEvent(object sender, EngineQueryEventArgs args)
        {
            EventHandler<EngineQueryEventArgs> handler = BoardEvent;
            handler?.Invoke(this, args);
        }

        public void engineEventHandler(object sender, EngineEventData data)
        {
            switch (data.type)
            {
                case EngineEventType.INIT_BOARD:
                    initBoard(data.board);
                    break;
                case EngineEventType.POSSIBLE_MOVE:
                    if (!_isInit) throw new Exception("BoardView not Initialized");
                    _boardViewController.possibleMoves(data.fields);
                    break;
                case EngineEventType.BOARD_CHANGED:
                    if (!_isInit) throw new Exception("BoardView not Initialized");
                    _boardViewController.piecesChanged(data.board);
                    _boardViewController.handleLastSelected(data.lastMove);
                    _board = data.board;
                    break;
                case EngineEventType.PROMOTION_DIALOG:
                    if (!_isInit) throw new Exception("BoardView not Initialized");
                    PromotionDialog dialog = new PromotionDialog(data.color);
                    dialog.DialogDone += handleDialogDone;
                    _parent.Children.Add(dialog);
                    break;
            }
        }

        private void handleDialogDone(object sender, Piece.Type type)
        {
            PromotionDialog dialog = (PromotionDialog) sender;
            _parent.Children.Remove(dialog);
            onBoardEvent(this, new EngineQueryEventArgs(type, QueryType.QUERY_DONE));
        }

        private void initBoard(Board board)
        {
            _isInit = true;
            _board = board;
            foreach (Field field in _board.fields)
            {
                FieldView fieldView = new FieldView(field, this);
                _fieldViews[field.x, field.y] = fieldView;
                Grid.SetColumn(fieldView, field.x);
                Grid.SetRow(fieldView, 7 - field.y);
                _grid.Children.Add(fieldView);
            }
            _boardViewController = new BoardViewController(_fieldViews);
        }

        public void fieldClickedHandler(Field field, FieldView fieldView)
        {
            if (fieldView.IsMarked)
            {
                Move move = _boardViewController.createMove(_board, field);
                onBoardEvent(this, new EngineQueryEventArgs(move, QueryType.MAKE_MOVE));
                _boardViewController.fieldClicked(field, fieldView);
                _boardViewController.clearMarked();
                return;
            }

            _boardViewController.fieldClicked(field, fieldView);
            _boardViewController.clearMarked();
            if (!field.isEmpty && !fieldView.IsMarked)
            {
                onBoardEvent(this, new EngineQueryEventArgs(field, QueryType.GET_POSSIBLE_MOVES));
            }
        }
    }
}
