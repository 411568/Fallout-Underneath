using System.Runtime.CompilerServices;
using System.Security;
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

        private bool _gameOver = false;

        // TODO
        //private Player player = Player.GetInstance(); 

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
            // Intro screen + menu

            // Start the game loop
            GameLoop();
        }

        public void GameLoop()
        {
            while(_gameOver == false)
            {
                Redraw();
                HandleUserInput();
                UpdateObjectState();
            }
        }

        private void Redraw()
        {
            // TODO
        }

        private void HandleUserInput()
        {
            var key = Console.ReadKey(true).Key;
            
            string pressedKey = key.ToString();
            DebugLogger.Log($"Pressed key: {pressedKey}");

            switch(key)
            {
                case ConsoleKey.A:
                    //player.Move(-1, 0);
                    break;
                case ConsoleKey.W:
                    //player.Move(0, 1);
                    break;
                case ConsoleKey.S:
                    //player.Move(0, -1);
                    break;
                case ConsoleKey.D:
                    //player.Move(1, 0);
                    break;
                case ConsoleKey.I:
                    //player.OpenInventory();
                    break;
                case ConsoleKey.Escape: // * Exit game
                    DebugLogger.Log("Exiting game...");
                    _gameOver = true;
                    break;
            }
        }

        private void UpdateObjectState()
        {
            // TODO
        }    
    }
}