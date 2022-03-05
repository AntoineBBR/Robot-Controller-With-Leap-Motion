using AsyncEV3MotorCommandsLib;
using RobotControllerLib;
using System.Collections.Generic;
using System.Threading;

namespace LegoController
{

    public class Manager
    {
        public Thread t = new Thread(Boucle);
        public Thread t2 = new Thread(BoucleCommande);
        private static bool isStopOneTime = false;

        public static ManagerController managerCtrl;
        public static BrickManager brickManager = new BrickManager();
        public static Commands commands = new Commands();

        public void LaunchDetection()
        {
            t.Start();
            Thread.Sleep(1000);
            t2.Start();
        }

        public static void Boucle()
        {
            managerCtrl = new ManagerController();
            managerCtrl.Detection();
        }

        public static void BoucleCommande()
        {
            while(true)
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

                    if (lcommande.Count == 0 && isStopOneTime == false) { commands.EmergencyStop(); isStopOneTime = true; }
                    if (!lcommande.Contains(Commande.TOURNERGAUCHE) && !lcommande.Contains(Commande.TOURNERDROITE))
                    {
                        if (lcommande.Count == 1)
                        {
                            if (lcommande.Contains(Commande.HAUT)) { commands.MoveLinearX(vitesse); }
                            if (lcommande.Contains(Commande.BAS)) { commands.MoveLinearX(-vitesse); }
                            if (lcommande.Contains(Commande.GAUCHE)) { commands.MoveLinearY(vitesse); }
                            if (lcommande.Contains(Commande.DROITE)) { commands.MoveLinearY(-vitesse); }
                            isStopOneTime = false;
                        }
                        if (lcommande.Count == 2)
                        {
                            if (lcommande.Contains(Commande.HAUT) && lcommande.Contains(Commande.GAUCHE)) { commands.MoveDiagonal1(vitesse); }
                            if (lcommande.Contains(Commande.HAUT) && lcommande.Contains(Commande.DROITE)) { commands.MoveDiagonal2(vitesse); }
                            if (lcommande.Contains(Commande.BAS) && lcommande.Contains(Commande.GAUCHE)) { commands.MoveDiagonal2(-vitesse); }
                            if (lcommande.Contains(Commande.BAS) && lcommande.Contains(Commande.DROITE)) { commands.MoveDiagonal1(-vitesse); }
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
