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
            // ! TESTING
            GameMap testMap = new GameMap(40, 10);
            testMap.CreateNewMap();
            testMap.PrintMaze();

            // ! TESTING
            ScreenTextInterface textInterface = ScreenTextInterface.GetInstance();
            textInterface.Setup(25);
            textInterface.WriteText("TEXT INTERFACE INIT");


            // Start the game engine
            GameEngine gameEngine = GameEngine.GetInstance();
            gameEngine.StartGame();


            return 0;
        }
    }
}