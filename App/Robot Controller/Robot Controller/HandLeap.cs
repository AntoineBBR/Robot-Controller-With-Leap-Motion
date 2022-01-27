using System;
using System.Collections.Generic;
using System.Text;

namespace Robot_Controller
{
    class HandLeap : IEquatable<HandLeap>
    {
        public int Id { get; set; }

        public Leap.Vector PalmPosition { get; set; }

        public float GrabStrength { get; set; }

        public string X { get; set; }



        public HandLeap(int id, Leap.Vector palmPosition, float grabStrength, string x)
        {
            Id = id;
            PalmPosition = palmPosition;
            GrabStrength = grabStrength;
            X = x;
        }


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
}
