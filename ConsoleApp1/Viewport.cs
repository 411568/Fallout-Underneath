using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    class Viewport
    {
        private Viewport() {}

        private static Viewport? _instance;

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
            // TODO
        }

        public bool ChangeViewport(int currentLevel)
        {
            // TODO
            return false;
        }
    }
}