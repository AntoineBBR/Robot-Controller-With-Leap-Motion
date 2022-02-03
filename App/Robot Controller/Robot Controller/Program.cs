using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Timers;

namespace Robot_Controller
{
    class Program
    {
        private static HandLeapManager manager = new HandLeapManager();
        private static CalculateurDeplacement calculateurDeplacement = new CalculateurDeplacement();



        static void Main(string[] args)
        {
            //Initialisation de la connexion
            Leap.Controller ctrl = new Leap.Controller();
            ctrl.FrameReady += Ctrl_FrameReady;
            ctrl.StartConnection();



            //Définir la fonction qui tourne en boucle dans un second thread
            System.Timers.Timer timer = new System.Timers.Timer(100);
            timer.Elapsed += (source, ElapsedEventArgs) => Boucle();

            //démarage du thread
            timer.Start();

            //attente d'une entrée dans la console pour terminer le programme
            Console.ReadLine();
            timer.Stop();
            ctrl.StopConnection();
            Console.WriteLine("Capteur déconnecté");
        }





        private static void Boucle()
        {
            IEnumerable<HandLeap> hands = new List<HandLeap>(manager.Hands);
            foreach (var hand in hands)
            {
                if (hand.Side == "R")
                {
                    DetectionSurMainDroite(hand);
                    if (manager.IsStartPositionLock)
                    {
                        calculateurDeplacement.CalculDeplacementQuatreAxe(manager.StartPosition, hand);
                    }
                }
            }

            


            AffichageMain(hands);
            Thread.Sleep(1000 / 60);
        }




        private static void DetectionSurMainDroite(HandLeap hand)
        {
            if (hand.GrabStrength == 1 && !manager.IsStartPositionLock)
            {
                manager.SetAllStartPosition(hand);
                Console.WriteLine("positionLock");
            }
            if(hand.GrabStrength == 0 && manager.IsStartPositionLock)
            {
                manager.SetAllStartPosition(null);
                Console.WriteLine("positionUnLock");
            }
        }


        private static void AffichageMain(IEnumerable<HandLeap> hands)
        {
            Console.Clear();

            if (hands.Count() == 0)
            {
                Console.WriteLine("Pas de main");
            }
            else
            {

                Console.Write("Start Position : ");
                if (manager.IsStartPositionLock) Console.WriteLine(manager.StartPosition.PalmPosition);
                else Console.WriteLine("null");

                foreach (var hand in hands)
                {
                    Console.WriteLine("|" + hand.Id + hand.Side + "|  " + hand.PalmPosition + "  :  " + hand.GrabStrength);
                    Console.WriteLine(hand.test);
                }
            }
        }



        private static void Ctrl_FrameReady(object sender, Leap.FrameEventArgs e)
        {
            manager.Hands.Clear();
            var hands = e.frame.Hands.Select(h => new HandLeap(h.Id, h.PalmPosition, h.GrabStrength, h.IsLeft, h.Rotation));
            foreach(var h in hands)
            {
                manager.Hands.Add(h);
            }
        }

        
    }
}
