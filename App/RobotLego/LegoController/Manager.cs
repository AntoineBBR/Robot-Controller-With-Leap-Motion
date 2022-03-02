using AsyncEV3MotorCommandsLib;
using RobotControllerLib;

namespace LegoController
{
    public class Manager
    {
        public static ManagerController managerCtrl = new ManagerController(false);
        public static BrickManager brickManager = new BrickManager();
        public static Commands commands = new Commands();
    }
}
