using AsyncEV3MotorCommandsLib;
using RobotControllerLib;
using System.Timers;

namespace LegoController
{

    public class Manager
    {
        private Timer timer = new Timer(100);



        public static ManagerController managerCtrl;
        public static BrickManager brickManager = new BrickManager();
        public static Commands commands = new Commands();

        public void LaunchDetection()
        {

            timer.Elapsed += (source, ElapsedEventArgs) => Boucle();
            timer.Start();
            
        }

        public void Boucle()
        {
            managerCtrl = new ManagerController();
            managerCtrl.Detection();
        }
    }
}
