using WPFChessClone.Data;
using WPFChessClone.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WPFChessClone.Display
{
    /// <summary>
    /// Interaction logic for PromotionDialog.xaml
    /// </summary>
    public partial class PromotionDialog : UserControl
    {
        private Grid _grid;
        private Path _p0,_p1,_p2,_p3;

        private void p0_Clicked_Handler(object sender, MouseButtonEventArgs e)
        {
            onDialogDone(this, Piece.Type.Queen);
        }
        private void p1_Clicked_Handler(object sender, MouseButtonEventArgs e)
        {
            onDialogDone(this, Piece.Type.Rook);
        }
        private void p2_Clicked_Handler(object sender, MouseButtonEventArgs e)
        {
            onDialogDone(this, Piece.Type.Bishop);
        }
        private void p3_Clicked_Handler(object sender, MouseButtonEventArgs e)
        {
            onDialogDone(this, Piece.Type.Knight);
        }

        public PromotionDialog(ChessColor color)
        {
            InitializeComponent();
            _grid = (Grid)this.FindName("OptionsGrid");
            _p0 = (Path)this.FindName("p0");
            _p1 = (Path)this.FindName("p1");
            _p2 = (Path)this.FindName("p2");
            _p3 = (Path)this.FindName("p3");

            if(color == ChessColor.White)
            {
                p0.Style = (Style)FindResource("Queen-White");
                p1.Style = (Style)FindResource("Rook-White");
                p2.Style = (Style)FindResource("Bishop-White");
                p3.Style = (Style)FindResource("Knight-White");
            }
            if (color == ChessColor.Black)
            {
                p0.Style = (Style)FindResource("Queen-Black");
                p1.Style = (Style)FindResource("Rook-Black");
                p2.Style = (Style)FindResource("Bishop-Black");
                p3.Style = (Style)FindResource("Knight-Black");
            }

        }



        public event EventHandler<Piece.Type> DialogDone;
        private void onDialogDone(object sender, Piece.Type type)
        {
            EventHandler<Piece.Type> handler = DialogDone;
            handler?.Invoke(this, type);
        }
    }
}
