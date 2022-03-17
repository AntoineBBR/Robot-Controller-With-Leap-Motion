using AsyncEV3MotorCommandsLib;
using RobotControllerLib;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace LegoController
{
    /// <summary>
    /// Manager that launch the LeapMotion Manager link it to the robot commands
    /// </summary>
    public class Manager
    {
        public Thread t = new Thread(Loop);
        public Thread t2 = new Thread(CommandsLoop);
        private static bool isStopOneTime = false;

        private static bool forwardSensor = false;
        private static bool backwardSensor = false;
        private static int marge = 20;

        public static ManagerController managerCtrl;
        public static BrickManager brickManager = new BrickManager();
        public static Commands commands = new Commands();

        /// <summary>
        /// Launch of threads
        /// </summary>
        public void LaunchDetection()
        {
            t.Start();
            Thread.Sleep(1000);
            t2.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Loop()
        {
            managerCtrl = new ManagerController();
            managerCtrl.Detection();
        }

        public static void CommandsLoop()
        {
            while (true)     // It's very ugly (yes), but since the feature is inerrant to the application, a way to pause it is unnecessary.
            {
                while (!brickManager.Connected)
                {
                    Thread.Sleep(2000);
                }

                while (brickManager.Connected)
                {
                    Thread.Sleep(100);

                    List<Commande> lcommande = managerCtrl.GetListeCommande();
                    int vitesse = managerCtrl.GetVitesse();
                    forwardSensor = brickManager.InputPort1.PercentValue < marge;
                    backwardSensor = brickManager.InputPort4.PercentValue < marge;

                    if ((forwardSensor || backwardSensor) && isStopOneTime == false) { commands.EmergencyStop(); isStopOneTime = true; }

                    if (lcommande.Count == 0 && isStopOneTime == false) { commands.EmergencyStop(); isStopOneTime = true; }
                    if (!lcommande.Contains(Commande.TOURNERGAUCHE) && !lcommande.Contains(Commande.TOURNERDROITE))
                    {
                        if (lcommande.Count == 1)
                        {
                            if (lcommande.Contains(Commande.HAUT) && !forwardSensor) { commands.MoveLinearX(vitesse); }
                            if (lcommande.Contains(Commande.BAS) && !backwardSensor) { commands.MoveLinearX(-vitesse); }
                            if (lcommande.Contains(Commande.GAUCHE)) { commands.MoveLinearY(vitesse); }
                            if (lcommande.Contains(Commande.DROITE)) { commands.MoveLinearY(-vitesse); }
                            isStopOneTime = false;
                        }
                        if (lcommande.Count == 2)
                        {
                            if (lcommande.Contains(Commande.HAUT) && lcommande.Contains(Commande.GAUCHE) && !forwardSensor) { commands.MoveDiagonal1(vitesse); }
                            if (lcommande.Contains(Commande.HAUT) && lcommande.Contains(Commande.DROITE) && !forwardSensor) { commands.MoveDiagonal2(vitesse); }
                            if (lcommande.Contains(Commande.BAS) && lcommande.Contains(Commande.GAUCHE) && !backwardSensor) { commands.MoveDiagonal2(-vitesse); }
                            if (lcommande.Contains(Commande.BAS) && lcommande.Contains(Commande.DROITE) && !backwardSensor) { commands.MoveDiagonal1(-vitesse); }
                            isStopOneTime = false;
                        }
                    }
                    else
                    {
                        if (lcommande.Contains(Commande.TOURNERGAUCHE)) { commands.Turn(vitesse); }
                        if (lcommande.Contains(Commande.TOURNERDROITE)) { commands.Turn(-vitesse); }
                        isStopOneTime = false;
                    }
                }
            }
        }
    }
}
