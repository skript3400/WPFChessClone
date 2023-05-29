using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFChessClone.Logic.Core;
using WPFChessClone.Data;
using WPFChessClone.Display;
using WPFChessClone.Model;

namespace WPFChessClone.Logic
{

    public class Player
    {
        public ChessColor color { get; private set; }
        public PlayerMode mode { get; private set; }

        public bool isPlaying { get; private set; }

        public event EventHandler<EngineQueryEventArgs> EngineQuery;
        private void onEngineQuery (object sender, EngineQueryEventArgs args)
        {
            EventHandler<EngineQueryEventArgs> handler = EngineQuery;
            handler.Invoke(this, args);
        }
        public Player(ChessColor colorIn, PlayerMode modeIn)
        {
            color = colorIn;
            mode = modeIn;
            if (color == ChessColor.White) isPlaying = true;
            else isPlaying = false;
        }
        public void boardEventHandler(object sender, EngineQueryEventArgs args)
        {
            switch (args.type) {
                case QueryType.QUERY_DONE:
                    if (isPlaying) onEngineQuery(this, args);
                    break;
                case QueryType.MAKE_MOVE:
                    if (isPlaying) onEngineQuery(this, args);
                    break;
                case QueryType.GET_POSSIBLE_MOVES:
                    if(isPlaying) onEngineQuery(this,args);
                    break;
                default:
                    break;
            };
        }
        public void engineEventHandler(object sender, EngineEventData args)
        {
            switch (args.type)
            {
                case EngineEventType.CHANGE_TURNS:
                    if (args.color == color)
                    {
                        isPlaying = true;
                    }
                    else isPlaying = false;
                    break;
                default:
                    break;
            }
        }
    }
}
