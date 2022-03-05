using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace RobotControllerLib
{
    public class ManagerController
    {
        private HandLeapManager HlManager;
        private CalculateurDeplacement calculateurDeplacement;
        private Leap.Controller ctrl;
        private Timer timer;


        public ManagerController()
        {
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
            while (true) { }
            timer.Stop();
            ctrl.StopConnection();
        }


        private void Boucle()
        {
            IEnumerable<HandLeap> hands = new List<HandLeap>(HlManager.Hands);
            
            if(hands.Count() != 0)
            {
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

                if (hands.Count() == 0)
                {
                    calculateurDeplacement.Reset();
                }
            }
            
        }


        private void DetectionSurMainDroite(HandLeap hand)
        {
            if (hand.GrabStrength == 1 && !HlManager.IsStartPositionLock)
            {
                HlManager.SetAllStartPosition(hand);
            }
            if (hand.GrabStrength == 0 && HlManager.IsStartPositionLock)
            {
                HlManager.SetAllStartPosition(null);
                calculateurDeplacement.ListeCommande.Clear();
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

        public List<Commande> GetListeCommande()
        {
            return calculateurDeplacement.ListeCommande;
        }

        public int GetVitesse()
        {
            return calculateurDeplacement.Vitesse;
        }
    }
}
