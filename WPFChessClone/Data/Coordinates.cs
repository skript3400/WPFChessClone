using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFChessClone.Data
{
    public struct Coordinates
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordinates(Coordinates cIn) { x = cIn.x; y = cIn.y; }
        public Coordinates(int xIn, int yIn) { x = xIn; y = yIn; }
        public static bool operator ==(Coordinates a, Coordinates b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Coordinates a, Coordinates b)
        {
            return a.x != b.x || a.y != b.y;
        }
        public string toStrCoord()
        {
            xCoordChars xChar = (xCoordChars)x;
            string res = xChar.ToString().ToUpper() + (y+1).ToString().ToUpper();
            return res;
        }
        private enum xCoordChars
        {
            A = 0, B = 1, C = 2, D = 3, E = 4, F = 5, G = 6, H = 7
        }
    }
}
