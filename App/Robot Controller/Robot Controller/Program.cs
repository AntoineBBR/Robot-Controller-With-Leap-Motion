using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Timers;

namespace Robot_Controller
{
    class Program
    {
        private static ManagerController manager;



        static void Main(string[] args)
        {
            manager = new ManagerController(false);
            manager.Detection();
        }


        
    }
}
