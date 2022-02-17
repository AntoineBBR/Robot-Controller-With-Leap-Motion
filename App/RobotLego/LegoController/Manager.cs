using AsyncEV3MotorCommandsLib;
using RobotController;

namespace LegoController
{
    class Manager
    {
        public static BrickManager brickManager = new BrickManager();
        public static Commands commands = new Commands();
    }
}
