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
        private bool threadOn = true;


        public ManagerController()
        {
            ctrl = new Leap.Controller();
            ctrl.FrameReady += Ctrl_FrameReady;
            ctrl.StartConnection();

            HlManager = new HandLeapManager();
            controlCalculator = new ControlCalculator();

            timer = new Timer(100);
            timer.Elapsed += (source, ElapsedEventArgs) => Boucle();
        }

        /// <summary>
        /// start the main thread (Boucle function)
        /// </summary>
        public void Detection()
        {
            //démarage du thread
            timer.Start();
            while (threadOn) { }
            timer.Stop();
            ctrl.StopConnection();
        }

        /// <summary>
        /// looping function for detecting and updating the direction list
        /// </summary>
        private void Boucle()
        {
            IEnumerable<HandLeap> hands = new List<HandLeap>(HlManager.Hands);
            
            if(hands.Count() != 0) //if one hand at least is detected
            {
                foreach (var hand in hands)
                {
                    if (hand != null && hands.Count() != 0 && hand.Side == "R") //if right hand -> detection on right hand -> update of the list
                    {
                        RightHandDetection(hand);
                        if (HlManager.IsStartPositionLock)
                        {
                            controlCalculator.FourAxisMouvCalculation(HlManager.StartPositionRight, hand, !HlManager.IsLeftAvailable);
                            controlCalculator.CalculRotation(hand);
                        }
                        else
                        {
                            controlCalculator.ResetAll();
                        }
                    }
                    if (hand != null && hands.Count() > 1 && hand.Side == "L") //if left hand -> detection on left hand -> update of the speed
                    {
                        LeftHandDetection(hand);
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
            if (hands.Count() == 0) //if no hands detected -> reset list and speed
            {
                controlCalculator.ResetAll();
            }

        }

        /// <summary>
        /// detect if the right hand is close
        /// </summary>
        /// <param name="hand">right hand object</param>
        private void RightHandDetection(HandLeap hand)
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

        /// <summary>
        /// detect if the left hand is close
        /// </summary>
        /// <param name="hand">left hand object</param>
        private void LeftHandDetection(HandLeap hand)
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

        /// <summary>
        /// update the list of hands
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctrl_FrameReady(object sender, Leap.FrameEventArgs e)
        {
            HlManager.Hands.Clear();
            var hands = e.frame.Hands.Select(h => new HandLeap(h.Id, h.PalmPosition, h.GrabStrength, h.IsLeft, h.Rotation));
            foreach (var h in hands)
            {
                HlManager.Hands.Add(h);
            }
        }

        /// <summary>
        /// getter commands list
        /// </summary>
        /// <returns>commands list</returns>
        public List<Commande> GetListeCommande()
        {
            return controlCalculator.ListeCommande;
        }

        /// <summary>
        /// getter speed
        /// </summary>
        /// <returns>speed</returns>
        public int GetVitesse()
        {
            return controlCalculator.Speed;
        }
    }
}
