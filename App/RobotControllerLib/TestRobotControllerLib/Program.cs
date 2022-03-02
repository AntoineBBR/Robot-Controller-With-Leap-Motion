using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotControllerLib;

namespace TestRobotControllerLib
{
    internal class Program
    {
        private static ManagerController manager;

        static void Main(string[] args)
        {
            manager = new ManagerController(true);
            manager.Detection();
        }
    }
}
