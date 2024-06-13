using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    class ScreenTextInterface
    {
        private ScreenTextInterface() {}

        private static ScreenTextInterface? _instance;

        private int lineNumber = 22;

        public static ScreenTextInterface GetInstance()
        {
            if(_instance == null)
            {
                _instance = new ScreenTextInterface();
            }

            return _instance;
        }

        // * Set at which line we start drawing text
        public void Setup(int line)
        {
            lineNumber = line;
        }

        // Write text at a specified position
        private void WriteAt(string text, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(text);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                DebugLogger.LogError($"Argument out of range exception thrown: {e}");
            }
        }

        public void WriteText(string text)
        {
            DebugLogger.Log("Writing to text interface");
            
            // ? Clear text every time we write something? 
            ClearText();

            WriteAt(text, 7, lineNumber + 2);
        }


        public void ClearText()
        {
            DebugLogger.Log("Clearing text interface");

            WriteAt("    __________________________________________________________________________", 0, lineNumber);
            WriteAt("    |                                                                        |", 0, lineNumber + 1);
            WriteAt("    |                                                                        |", 0, lineNumber + 2);
            WriteAt("    |                                                                        |", 0, lineNumber + 3);
            WriteAt("    \\________________________________________________________________________/", 0, lineNumber + 3);
        }
    }
}