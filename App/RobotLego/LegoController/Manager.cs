using AsyncEV3MotorCommandsLib;

namespace LegoController
{
    public class Manager
    {
        public static ManagerController managerCtrl = new ManagerController();
        public static BrickManager brickManager = new BrickManager();
        public static Commands commands = new Commands();
    }
}
