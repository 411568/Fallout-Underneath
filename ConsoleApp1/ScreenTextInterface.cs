using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    class ScreenTextInterface
    {
        private ScreenTextInterface() {}

        private static ScreenTextInterface? _instance;

        private int lineNumber = 20;

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
                Console.WriteLine(e.Message);
            }
        }

        public void WriteText(string text)
        {
            WriteAt(text, 0, lineNumber);
        }

        public void ClearText()
        {
            WriteAt("                                                                          ", 0, lineNumber);
        }
    }
}