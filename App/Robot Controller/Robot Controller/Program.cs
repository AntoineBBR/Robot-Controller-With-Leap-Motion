using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
            Timer timer = new Timer(100);
            timer.Elapsed += (source, ElapsedEventArgs) =>
            {
                Console.Clear();
                IEnumerable<HandLeap> hands = new List<HandLeap>(manager.Hands);

                if(hands.Count() == 0)
                {
                    Console.WriteLine("Pas de main");
                }

                foreach(var hand in hands)
                {
                    Console.Write("|");
                    Console.Write(hand.Id);
                    Console.Write("|  ");
                    Console.Write(hand.PalmPosition);
                    Console.Write("  :  ");
                    Console.Write(hand.GrabStrength);
                    Console.WriteLine(" / ");
                    Console.WriteLine(hand.X);
                }
            };

            //démarage du thread
            timer.Start();

            //attente d'une entrée dans la console pour terminer le programme
            Console.ReadLine();
            timer.Stop();
            ctrl.StopConnection();
            Console.WriteLine("Capteur déconnecté");
        }




        private static void Ctrl_FrameReady(object sender, Leap.FrameEventArgs e)
        {
            manager.Hands.Clear();
            var hands = e.frame.Hands.Select(h => new HandLeap(h.Id, h.PalmPosition, h.GrabStrength, h.ToString()));
            foreach(var h in hands)
            {
                manager.Hands.Add(h);
            }
        }

        
    }
}
