using System;

namespace RobotControllerLib
{
    /// <summary>
    /// class representing a hand
    /// </summary>
    public class HandLeap : IEquatable<HandLeap>
    {
        /// <summary>
        /// the number of the hand that is detected (incremented each time a hand is lost/detected)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// current position of the hand
        /// </summary>
        public Leap.Vector PalmPosition { get; set; }

        /// <summary>
        /// indicates whether the hand is closed or open
        /// </summary>
        public float GrabStrength { get; set; }

        /// <summary>
        /// indicates if it's the left or right hand
        /// </summary>
        public string Side { get; set; }

        /// <summary>
        /// current twist of the wrist
        /// </summary>
        public Leap.LeapQuaternion Rotation { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">id of the hand that is detected</param>
        /// <param name="palmPosition">current position of the hand</param>
        /// <param name="grabStrength">indicates whether the hand is closed or open</param>
        /// <param name="isLeft">true if it's the left hand, flase for a right hand</param>
        /// <param name="rotation">current twist of the wrist</param>
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
