using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFChessClone.Data
{
    public enum GameMode
    {
        SINGLE_DUAL,
        SINGLE_NPC,
        MULTI

    }
    public enum PlayerMode
    {
        NPC,
        LOCAL,
        REMOTE
    }
    public class GameSettings
    {
        public GameMode Mode { get; private set; }
        public ChessColor Player1Color { get; private set; }
        public PlayerMode Player1Mode { get; private set; }
        public ChessColor Player2Color { get; private set; }
        public PlayerMode Player2Mode { get; private set; }


        public GameSettings() 
        {
            Mode = GameMode.SINGLE_DUAL;
            Player1Color = ChessColor.White;
            Player1Mode = PlayerMode.LOCAL;
            Player2Color = ChessColor.Black;
            Player2Mode = PlayerMode.LOCAL;

        }
        public GameSettings(GameMode mod, ChessColor plColor)
        {
            Mode = mod;
            Player1Color = plColor;
        }
    }
}
