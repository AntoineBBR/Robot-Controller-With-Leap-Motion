using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace RobotControllerLib
{
    public class ManagerController
    {
        private readonly HandLeapManager HlManager;
        private readonly ControlCalculator controlCalculator;
        private readonly Leap.Controller ctrl;
        private readonly Timer timer;


        public ManagerController()
        {
            ctrl = new Leap.Controller();
            ctrl.FrameReady += Ctrl_FrameReady;
            ctrl.StartConnection();

            HlManager = new HandLeapManager();
            controlCalculator = new ControlCalculator();

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
                    if (hand != null && hands.Count() != 0 && hand.Side == "R")
                    {
                        DetectionSurMainDroite(hand);
                        if (HlManager.IsStartPositionLock)
                        {
                            controlCalculator.CalculDeplacementQuatreAxe(HlManager.StartPositionRight, hand, !HlManager.IsLeftAvailable);
                            controlCalculator.CalculRotation(hand);
                        }
                        else
                        {
                            controlCalculator.ResetAll();
                        }
                    }
                    if (hand != null && hands.Count() > 1 && hand.Side == "L")
                    {
                        DetectionSurMainGauche(hand);
                        if (HlManager.IsLeftAvailable)
                        {
                            controlCalculator.CalculSpeed(HlManager.StartPositionLeft, hand);
                        }
                        else
                        {
                            controlCalculator.ResetSpeed();
                        }
                    }
                }

            }
            if (hands.Count() == 0)
            {
                controlCalculator.ResetAll();
            }

        }

        private void DetectionSurMainDroite(HandLeap hand)
        {
            if (hand.GrabStrength == 1 && !HlManager.IsStartPositionLock)
            {
                HlManager.SetRightStartPosition(hand);
            }
            if (hand.GrabStrength == 0 && HlManager.IsStartPositionLock)
            {
                HlManager.SetRightStartPosition(null);
                controlCalculator.ResetAll();
            }
        }

        private void DetectionSurMainGauche(HandLeap hand)
        {
            if (hand.GrabStrength == 1 && !HlManager.IsLeftAvailable)
            {
                HlManager.SetLeftStartPosition(hand);
            }
            if (hand.GrabStrength == 0 && HlManager.IsLeftAvailable)
            {
                HlManager.SetLeftStartPosition(null);
                controlCalculator.ResetSpeed();
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
            return controlCalculator.ListeCommande;
        }

        public int GetVitesse()
        {
            return controlCalculator.Speed;
        }
    }
}
