using System;
using System.Collections.Generic;
using System.Text;

namespace Robot_Controller
{
    class HandLeapManager
    {
        public HashSet<HandLeap> Hands { get => hands; set => hands = value; }
        private HashSet<HandLeap> hands = new HashSet<HandLeap>();



        public HandLeap StartPosition { get; set; }
        public bool IsStartPositionLock { get; set; }

        public HandLeapManager()
        {
            IsStartPositionLock = false;
        }



        public void SetAllStartPosition(HandLeap hand)
        {
            StartPosition = hand;
            IsStartPositionLock = hand == null ? false : true;
        }
    }
}
