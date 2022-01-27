using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp
{
    class Program
    {
        class HandLeap : IEquatable<HandLeap>
        {
            public int Id { get; set; }

            public Leap.Vector PalmPosition { get; set; }

            public float GrabStrength { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                if (obj == this) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals(obj as HandLeap);
            }

            public bool Equals(HandLeap other)
            {
                return other.Id == Id;
            }

            public override int GetHashCode()
            {
                return Id;
            }
        }

        class HandLeapManager
        {
            public HashSet<HandLeap> Hands { get => hands; set => hands = value; }
            private HashSet<HandLeap> hands = new HashSet<HandLeap>();
        }

        static HandLeapManager mgr = new HandLeapManager();

        static void Main(string[] args)
        {
            Leap.Controller ctrl = new Leap.Controller();
            ctrl.FrameReady += Ctrl_FrameReady;
            ctrl.StartConnection();

            Timer t = new Timer(100);
            t.Elapsed += (source, elapsedEventArgs) =>
            {
                Console.Clear();
                IEnumerable<HandLeap> hands = new List<HandLeap>(mgr.Hands);
                if(hands.Count() == 0)
                {
                    Console.WriteLine("Come on and move your hands!");
                }
                foreach(var hand in hands)
                {
                    Console.WriteLine(hand.Id);
                    Console.WriteLine(hand.PalmPosition);
                    Console.WriteLine(hand.GrabStrength);
                    Console.WriteLine();
                }
            };
            t.Start();
            Console.ReadLine();
            t.Stop();
            ctrl.StopConnection();
        }

        private static void Ctrl_FrameReady(object sender, Leap.FrameEventArgs e)
        {
            lock(mgr)
            {
                mgr.Hands.Clear();
                var hands = e.frame.Hands.Select(h => new HandLeap { PalmPosition = h.PalmPosition, GrabStrength = h.GrabStrength, Id = h.Id });
                foreach (var h in hands)
                {
                    mgr.Hands.Add(h);
                }
            }
        }

    }
}
