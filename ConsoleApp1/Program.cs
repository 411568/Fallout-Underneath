using System.Text;

namespace FalloutUnderneath
{
    class Program
    {
        static int Main()
        {
            Console.SetWindowSize(82, 30);
            Console.OutputEncoding = Encoding.UTF8;

            // Initialize the debug logger
            DebugLogger.SetDebugFile("DebugLog.txt");

            DebugLogger.Log("Program starting");

            // Start the game engine
            GameEngine gameEngine = GameEngine.GetInstance();
            gameEngine.StartGame();


            return 0;
        }
    }
}