using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFChessClone.Network
{
    public enum NetworkMode
    {
        Local,
        Remote,
        NPC
    }
    class NetworkModule
    {
        NetworkMode _mode;
        NetworkModule(NetworkMode mode)
        {
            _mode = mode;
        }
        
    }
}
