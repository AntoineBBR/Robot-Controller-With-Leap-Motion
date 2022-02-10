using AsyncEV3MotorCommandsLib;
using BluetoothDevicesScanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LegoControler
{
    class Manager
    {
        public static BrickManager brickManager = new BrickManager();
        public static Commands commands = new Commands();
        //private static MainWindow mainWindow = new MainWindow();
    }
}
