using System;
using System.Collections.Generic;
using System.Text;

namespace Robot_Controller
{
    class HandLeapManager
    {
        public HashSet<HandLeap> Hands { get => hands; set => hands = value; }
        private HashSet<HandLeap> hands = new HashSet<HandLeap>();
    }
}
