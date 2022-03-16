using System.Collections.Generic;

namespace RobotControllerLib
{
    /// <summary>
    /// class managing a collection of hands
    /// </summary>
    public class HandLeapManager
    {
        /// <summary>
        /// handleap collection
        /// </summary>
        public HashSet<HandLeap> Hands { get => hands; set => hands = value; }
        private HashSet<HandLeap> hands = new HashSet<HandLeap>();

        /// <summary>
        /// save the start position when user close is right hand
        /// </summary>
        public HandLeap StartPositionRight { get; set; }
        /// <summary>
        /// save the start position when user close is left hand
        /// </summary>
        public HandLeap StartPositionLeft { get; set; }
        /// <summary>
        /// is the right hand is close ?
        /// </summary>
        public bool IsStartPositionLock { get; set; }
        /// <summary>
        /// is the left hand is close ?
        /// </summary>
        public bool IsLeftAvailable { get; set; }

        public HandLeapManager()
        {
            IsStartPositionLock = false;
        }

        /// <summary>
        /// set the right hand start position
        /// </summary>
        /// <param name="hand">any HandLeap object if the right hand is close / null if the right hand is open or lose</param>
        public void SetRightStartPosition(HandLeap hand)
        {
            StartPositionRight = hand;
            IsStartPositionLock = hand != null;
        }

        /// <summary>
        /// set the left hand start position
        /// </summary>
        /// <param name="hand">any HandLeap object if the left hand is close / null if the right hand is open or lose</param>
        public void SetLeftStartPosition(HandLeap hand)
        {
            StartPositionLeft = hand;
            IsLeftAvailable = hand != null;
        }
    }
}
