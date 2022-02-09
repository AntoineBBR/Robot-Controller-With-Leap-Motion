using Lego.Ev3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoControler
{
    class Ports
    {
        public static Dictionary<char, OutputPort> ports = new Dictionary<char, OutputPort>()
        {
            {'A', OutputPort.A},
            {'B', OutputPort.B},
            {'C', OutputPort.C},
            {'D', OutputPort.D}
        };

        public static Dictionary<char, InputPort> inputPorts = new Dictionary<char, InputPort>()
        {
            {'A', InputPort.A},
            {'B', InputPort.B},
            {'C', InputPort.C},
            {'D', InputPort.D}
        };
    }
}
