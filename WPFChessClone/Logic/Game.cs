using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFChessClone.Display;
using WPFChessClone.Data;
using WPFChessClone.Model;

namespace WPFChessClone.Logic
{

    public class Game
    {
        private Canvas _gameArea;
        private BoardView _boardView;
        public Board Board { get; private set; }
        public Engine Engine;
        private bool isOnline;

        public Game(GameSettings settings) 
        {
            Engine = new Engine(Board);
            Engine.EngineEvent += engineEventHandler;
        }

        private void engineEventHandler(object sender, EngineEventData data) 
        {
            
        }

    }
}
