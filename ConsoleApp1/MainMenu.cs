using System.Runtime.InteropServices;
using System.Text;

namespace FalloutUnderneath
{
    class MainMenu
    {
        private MainMenu(){}
        
        private static MainMenu? _instance;

        public static MainMenu GetInstance()
        {
            if(_instance == null)
            {
                _instance = new MainMenu();
            }

            return _instance;
        }

        public void ShowMainMenu()
        {
            DebugLogger.Log("Showing main menu");

            Console.Clear();

            Console.SetCursorPosition(10, 10);
            Console.Write("Fallout Underneath");

            Console.SetCursorPosition(10, 12);
            Console.Write("Main menu");

            Console.SetCursorPosition(10, 13);
            Console.Write("1. Start game");
            Console.SetCursorPosition(10, 14);
            Console.Write("2. Instructions");
            Console.SetCursorPosition(10, 15);
            Console.Write("3. Exit game");

            ConsoleKeyInfo key = Console.ReadKey();

            switch(key.KeyChar)
            {
                case '1':
                    break;
                case '2': 
                    ShowInstructions();
                    ShowMainMenu();
                    break;
                case '3': 
                    DebugLogger.Log("Exiting game...");
                    Environment.Exit(0); // * Exit game
                    break;
                default:
                    ShowMainMenu();
                    break;
            }
        }

        private void ShowInstructions()
        {
            DebugLogger.Log("Showing main menu instructions");

            Console.Clear();

            Console.SetCursorPosition(10, 10);
            Console.Write("Fallout Underneath");

            Console.SetCursorPosition(10, 12);
            Console.Write("User input:");

            Console.SetCursorPosition(10, 13);
            Console.Write("Move: W/A/S/D");
            Console.SetCursorPosition(10, 14);
            Console.Write("Open inventory: I");
            Console.SetCursorPosition(10, 15);
            Console.Write("Exit game: Esc");


            Console.SetCursorPosition(10, 17);
            Console.Write("Press any key to exit...");

            Console.ReadKey();
        }
    }
}
