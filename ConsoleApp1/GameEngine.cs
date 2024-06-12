using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    class GameEngine
    {
        private GameEngine() {}

        private static GameEngine? _instance;

        // Game viewport and text interface
        private Viewport currentViewport = Viewport.GetInstance();
        private ScreenTextInterface textInterface = ScreenTextInterface.GetInstance();


        public static GameEngine GetInstance()
        {
            if(_instance == null)
            {
                _instance = new GameEngine();
            }

            return _instance;
        }

        public void StartGame()
        {
            DebugLogger.Log("Starting game");
            // TODO
        }

        public void GameLoop()
        {
            // TODO
        }

        private void Redraw()
        {
            // TODO
        }

        private void HandleUserInput()
        {
            // TODO
        }

        private void UpdateObjectState()
        {
            // TODO
        }    
    }
}