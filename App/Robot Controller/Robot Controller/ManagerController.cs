using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Robot_Controller
{
    class ManagerController
    {

        private HandLeapManager HlManager;
        private CalculateurDeplacement calculateurDeplacement;
        private Leap.Controller ctrl;
        private Timer timer;
        private bool debug;


        public ManagerController(bool debug)
        {
            this.debug = debug;

            ctrl = new Leap.Controller();
            ctrl.FrameReady += Ctrl_FrameReady;
            ctrl.StartConnection();

            HlManager = new HandLeapManager();
            calculateurDeplacement = new CalculateurDeplacement();

            //Définir la fonction qui tourne en boucle dans un second thread
            timer = new Timer(100);
            timer.Elapsed += (source, ElapsedEventArgs) => Boucle();
        }


        public void Detection()
        {
            //démarage du thread
            timer.Start();

            //attente d'une entrée dans la console pour terminer le programme
            Console.ReadLine();
            timer.Stop();
            ctrl.StopConnection();
            Console.WriteLine("Capteur déconnecté");
        }





        private void Boucle()
        {
            IEnumerable<HandLeap> hands = new List<HandLeap>(HlManager.Hands);

            foreach (var hand in hands)
            {
                if (hands.Count() != 0 && hand.Side == "R")
                {
                    DetectionSurMainDroite(hand);
                    if (HlManager.IsStartPositionLock)
                    {
                        calculateurDeplacement.CalculDeplacementQuatreAxe(HlManager.StartPosition, hand);
                        calculateurDeplacement.CalculRotation(hand);
                        calculateurDeplacement.CalculVitesse(HlManager.StartPosition, hand);
                    }
                    else
                    {
                        calculateurDeplacement.Reset();
                    }
                }
            }




            if(debug) AffichageCommande();
        }


        private void DetectionSurMainDroite(HandLeap hand)
        {
            if (hand.GrabStrength == 1 && !HlManager.IsStartPositionLock)
            {
                HlManager.SetAllStartPosition(hand);
                if(debug) Console.WriteLine("positionLock");
            }
            if (hand.GrabStrength == 0 && HlManager.IsStartPositionLock)
            {
                HlManager.SetAllStartPosition(null);
                calculateurDeplacement.ListeCommande.Clear();
                if (debug) Console.WriteLine("positionUnLock");
            }
        }


        private void AffichageMain(IEnumerable<HandLeap> hands)
        {
            Console.Clear();

            if (hands.Count() == 0)
            {
                Console.WriteLine("Pas de main");
            }
            else
            {

                Console.Write("Start Position : ");
                if (HlManager.IsStartPositionLock) Console.WriteLine(HlManager.StartPosition.PalmPosition);
                else Console.WriteLine("null");

                foreach (var hand in hands)
                {
                    Console.WriteLine("|" + hand.Id + hand.Side + "|  " + hand.PalmPosition + "  :  " + hand.GrabStrength);
                    Console.WriteLine(hand.Rotation);
                }
            }

        }


        private void AffichageCommande()
        {
            Console.Clear();

            Console.WriteLine(calculateurDeplacement.Vitesse);

            foreach (Commande c in calculateurDeplacement.ListeCommande)
            {
                Console.WriteLine(c);
            }
        }





        private void Ctrl_FrameReady(object sender, Leap.FrameEventArgs e)
        {
            HlManager.Hands.Clear();
            var hands = e.frame.Hands.Select(h => new HandLeap(h.Id, h.PalmPosition, h.GrabStrength, h.IsLeft, h.Rotation));
            foreach (var h in hands)
            {
                HlManager.Hands.Add(h);
            }
        }

    }
}
