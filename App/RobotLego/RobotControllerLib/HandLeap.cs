using System;

namespace RobotController
{
    public class HandLeap : IEquatable<HandLeap>
    {
        public int Id { get; set; }

        public Leap.Vector PalmPosition { get; set; }

        public float GrabStrength { get; set; }

        public string Side { get; set; }

        public Leap.LeapQuaternion Rotation { get; set; }



        public HandLeap(int id, Leap.Vector palmPosition, float grabStrength, bool isLeft, Leap.LeapQuaternion rotation)
        {
            Id = id;
            PalmPosition = palmPosition;
            GrabStrength = grabStrength;
            Side = isLeft ? "L" : "R";
            Rotation = rotation;
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
