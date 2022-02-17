namespace LeapMotionDetector
{
    class Program
    {
        private static ManagerController manager;

        static void Main(string[] args)
        {
            manager = new ManagerController(true);
            manager.Detection();
        }
    }
}
