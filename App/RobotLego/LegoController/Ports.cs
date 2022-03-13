using Lego.Ev3.Core;
using System.Collections.Generic;

namespace LegoController
{
    public class Ports
    {
        /// <summary>
        /// Motors
        /// </summary>
        public static Dictionary<char, OutputPort> ports = new Dictionary<char, OutputPort>()
        {
            {'A', OutputPort.A},
            {'B', OutputPort.B},
            {'C', OutputPort.C},
            {'D', OutputPort.D}
        };

        /// <summary>
        /// Sensors
        /// </summary>
        public static Dictionary<char, InputPort> inputPorts = new Dictionary<char, InputPort>()
        {
            {'A', InputPort.A},
            {'B', InputPort.B},
            {'C', InputPort.C},
            {'D', InputPort.D}
        };
    }
}
