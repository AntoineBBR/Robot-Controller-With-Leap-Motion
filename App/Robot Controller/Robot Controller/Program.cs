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
        



        static void Main(string[] args)
        {
            //Initialisation de la connexion
            Leap.Controller ctrl = new Leap.Controller();
            ctrl.FrameReady += Ctrl_FrameReady;
            ctrl.StartConnection();



            //Définir la fonction qui tourne en boucle dans un second thread
            System.Timers.Timer timer = new System.Timers.Timer(100);
            timer.Elapsed += (source, ElapsedEventArgs) =>
            {
                
                IEnumerable<HandLeap> hands = new List<HandLeap>(manager.Hands);

                







                AffichageMain(hands);
                Thread.Sleep(1000 / 30);
            };

            //démarage du thread
            timer.Start();

            //attente d'une entrée dans la console pour terminer le programme
            Console.ReadLine();
            timer.Stop();
            ctrl.StopConnection();
            Console.WriteLine("Capteur déconnecté");
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
