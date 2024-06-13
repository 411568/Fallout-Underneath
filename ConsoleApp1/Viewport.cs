using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    public class Viewport
    {
        private Viewport() {}

        private static Viewport? _instance;

        GameMap gameMapGenerator = new GameMap(40, 10);

        private char[,] currentMap = new char[40, 10];

        public static Viewport GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Viewport();
            }

            return _instance;
        }

        public void StartNewGame()
        {
            DebugLogger.Log("Creating viewport...");

            currentMap = gameMapGenerator.GetNewMap();
        }

        public bool ChangeViewport(int currentLevel)
        {
            DebugLogger.Log("Changed viewport");

            currentMap = gameMapGenerator.GetNewMap();
        
            return true;
        }

        public void RedrawCurrentViewport()
        {
            DebugLogger.Log("Redrawing viewport");

            Console.Clear();

            for (int y = 0; y < currentMap.GetLength(1); y++)
            {
                for (int x = 0; x < currentMap.GetLength(0); x++)
                {
                    Console.Write(currentMap[x, y]);
                }
                Console.WriteLine();
            }
        }

        public char GetCharacterFromPosition(int x, int y)
        {
            return currentMap[x, y];
        }

        public void ChangeCharacterInPosition(int x, int y, char inputCharacter, ConsoleColor inputColor)
        {
            DebugLogger.Log("Drawing something on the viewport");

            currentMap[x, y] = inputCharacter;

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = inputColor;
            Console.Write(inputCharacter);

            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}