using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Diagnostics;
using WPFChessClone.Logic;
using WPFChessClone.Model;
using WPFChessClone.Data;
using System.Diagnostics.Eventing.Reader;

namespace WPFChessClone.Display
{

    public partial class FieldView
    {

        private Coordinates _coordinate;
        public Coordinates Coordinates { get { return _coordinate; } }
        private Field _field;
        private SolidColorBrush _backgroundColor;
        private Path _backgroundPath;
        private Path _figurePath;
        private StackPanel _layer0;
        private StackPanel _layer1;

        private BoardView _parent;

        private bool _isSelected = false;
        public bool IsSelected { set { if (value == _isSelected) return; _isSelected = value;  updateState(); } get { return _isSelected; } }
        private bool _isMarked = false;
        public bool IsMarked { set { if (value == _isMarked) return; _isMarked = value; updateState(); } get { return _isMarked; } }
        private bool _movedLast = false;
        public bool MovedLast { set { if (value == _movedLast) return; _movedLast = value; updateState(); } get { return _movedLast; } }
        private bool _isChecked = false;
        public bool IsCheckd { set { if (value == _isChecked) return; _isChecked = value; updateState(); } get { return _isChecked; } }

        private Color Blend(Color color, Color backColor, double amount)
        {
            byte r = (byte)(color.R * amount + backColor.R * (1 - amount));
            byte g = (byte)(color.G * amount + backColor.G * (1 - amount));
            byte b = (byte)(color.B * amount + backColor.B * (1 - amount));
            return Color.FromRgb(r, g, b);
        }

        private void updateState()
        {
            SolidColorBrush brush = _backgroundColor;
            if (_movedLast) brush = (SolidColorBrush)FindResource("movedLastTile");
            if (_isMarked) brush = (SolidColorBrush)FindResource("markedTile");
            if (_isMarked && !_field.isEmpty) brush = (SolidColorBrush)FindResource("markedAtcTile");
            if (_isChecked) brush = (SolidColorBrush)FindResource("checkedTile");
            if (_isSelected) brush = (SolidColorBrush)FindResource("selectedTile");
            if (_movedLast && _isSelected) brush = (SolidColorBrush)FindResource("movedSelectedTile");
            if (_isChecked&&_isSelected) brush = (SolidColorBrush)FindResource("checkedSelectedTile");
            this._layer1.Background = brush;
        }

        public FieldView(Field fIn, BoardView parent)
        {
            InitializeComponent();
            _layer0 = (StackPanel)this.FindName("BackgroundPanel");
            _layer1 = (StackPanel)this.FindName("ForegroundPanel");
            _backgroundPath = (Path)this.FindName("BackgroundPath");
            _figurePath = (Path)this.FindName("FigurePath");

            _parent = parent;
            _field = fIn;
            _coordinate = fIn.Coordinates;
            determineColor();
            updateStyle(fIn.piece);
        }

        public void updateField(Field f)
        {
            _field = f;
            updateStyle(_field.piece);
        }


        private void updateStyle(Piece piece)
        {
            if (piece == null) { _figurePath.Style = (Style)FindResource("Empty"); return; }
            else
            {
                switch (piece.type)
                {
                    case Piece.Type.Pawn:
                        if (piece.color == ChessColor.White)
                            _figurePath.Style = (Style)FindResource("Pawn-White");
                        else _figurePath.Style = (Style)FindResource("Pawn-Black");
                        break;
                    case Piece.Type.Bishop:
                        if (piece.color == ChessColor.White)
                            _figurePath.Style = (Style)FindResource("Bishop-White");
                        else _figurePath.Style = (Style)FindResource("Bishop-Black");
                        break;
                    case Piece.Type.Queen:
                        if (piece.color == ChessColor.White)
                            _figurePath.Style = (Style)FindResource("Queen-White");
                        else _figurePath.Style = (Style)FindResource("Queen-Black");
                        break;

                    case Piece.Type.King:
                        if (piece.color == ChessColor.White)
                            _figurePath.Style = (Style)FindResource("King-White");
                        else _figurePath.Style = (Style)FindResource("King-Black");
                        break;

                    case Piece.Type.Rook:
                        if (piece.color == ChessColor.White)
                            _figurePath.Style = (Style)FindResource("Rook-White");
                        else _figurePath.Style = (Style)FindResource("Rook-Black");
                        break;

                    case Piece.Type.Knight:
                        if (piece.color == ChessColor.White)
                            _figurePath.Style = (Style)FindResource("Knight-White");
                        else _figurePath.Style = (Style)FindResource("Knight-Black");
                        break;

                    default:
                        throw new ArgumentException("Unknown Piece");
                }
            }
        }

        private void determineColor()
        {
            if ((_coordinate.x + _coordinate.y) % 2 == 0)
            {
                _backgroundColor = (SolidColorBrush)FindResource("evenTile");
                _layer0.Background = _backgroundColor;

            }
            else
            {
                _backgroundColor = (SolidColorBrush)FindResource("oddTile");
                _layer0.Background = _backgroundColor;
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _parent.fieldClickedHandler(this._field, this);
        }
    }
}

