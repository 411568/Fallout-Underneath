using System.Text;

namespace FalloutUnderneath
{
    class Program
    {
        static int Main()
        {
            Console.SetWindowSize(82, 30);

            // Initialize the debug logger
            DebugLogger.SetDebugFile("DebugLog.txt");

            DebugLogger.Log("LET THE DEBUGGING BEGIN!");

            // Start the game engine
            GameEngine gameEngine = GameEngine.GetInstance();
            gameEngine.StartGame();


            return 0;
        }
    }
}