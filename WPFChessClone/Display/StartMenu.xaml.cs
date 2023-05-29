using System;
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
using WPFChessClone.Network;
using WPFChessClone.Model;
using WPFChessClone.Data;
using WPFChessClone.Properties;
using System.Runtime;

namespace WPFChessClone.Display
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : UserControl
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        public event EventHandler<GameSettings> MenuDone;

        private void OnMenuDone(object sender, GameSettings settings) {
            EventHandler<GameSettings> handler = MenuDone;
            handler.Invoke(sender, settings);
        }

        private void onSinglePlayerClick(object sender, RoutedEventArgs e)
        {
        }

        private void onMultiPlayerClick(object sender, RoutedEventArgs e)
        {

        }

        private void on2PlayerClick(object sender, RoutedEventArgs e)
        {
            GameSettings settings = new GameSettings();
            OnMenuDone(this, settings);

        }
    }
}
