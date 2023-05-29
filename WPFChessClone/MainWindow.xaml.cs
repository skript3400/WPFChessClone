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
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using WPFChessClone.Logic;
using WPFChessClone.Model;
using WPFChessClone.Display;
using WPFChessClone.Data;

namespace WPFChessClone
{
    
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StartMenu _startMenu = new StartMenu();
        private Window _window;
        private Canvas _canvas;
        private BoardView _boardView;
        private Player _player1,_player2;
        private Game _game;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GameAreaLoaded(object sender, RoutedEventArgs e)
        {
            _window = sender as Window;
            _canvas = e.Source as Canvas;
            _startMenu.MenuDone += onMenuDone;
            _canvas.Children.Add(_startMenu);
        }

        private void onMenuDone (object sender,  GameSettings settings) 
        {
            //todo loader that loads game or board
            switch (settings.Mode)
            {
                case GameMode.SINGLE_DUAL:
                    _boardView = new BoardView(_canvas);
                    _player1 = new Player(settings.Player1Color, settings.Player1Mode);
                    _player2 = new Player(settings.Player2Color, settings.Player2Mode);
                    _game = new Game(settings);
                    _game.Engine.EngineEvent += _boardView.engineEventHandler;
                    _game.Engine.EngineEvent += _player1.engineEventHandler;
                    _game.Engine.EngineEvent += _player2.engineEventHandler;
                    _boardView.BoardEvent += _player1.boardEventHandler;
                    _boardView.BoardEvent += _player2.boardEventHandler;
                    _player1.EngineQuery += _game.Engine.engineQueryHandler;
                    _player2.EngineQuery += _game.Engine.engineQueryHandler;

                    _game.Engine.Begin();
                    break;
                default:
                    throw new NotImplementedException();
            }


            GameArea.Children.Remove(_startMenu);
            GameArea.Children.Add(_boardView);
        }

    }   


}
