using System.Collections.Generic;

namespace RobotControllerLib
{
    public class HandLeapManager
    {
        public HashSet<HandLeap> Hands { get => hands; set => hands = value; }
        private HashSet<HandLeap> hands = new HashSet<HandLeap>();

        public HandLeap StartPositionRight { get; set; }
        public HandLeap StartPositionLeft { get; set; }
        public bool IsStartPositionLock { get; set; }
        public bool IsLeftAvailable { get; set; }

        public HandLeapManager()
        {
            IsStartPositionLock = false;
        }

        public void SetRightStartPosition(HandLeap hand)
        {
            StartPositionRight = hand;
            IsStartPositionLock = hand != null;
        }

        public void SetLeftStartPosition(HandLeap hand)
        {
            StartPositionLeft = hand;
            IsLeftAvailable = hand != null;
        }
    }
}
