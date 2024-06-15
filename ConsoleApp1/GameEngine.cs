using System.Runtime.CompilerServices;
using System.Security;
using System.Text;


namespace FalloutUnderneath
{
    public class GameEngine
    {
        private GameEngine() {}

        private static GameEngine? _instance;

        // Game viewport and text interface
        private Viewport currentViewport = Viewport.GetInstance();
        private ScreenTextInterface textInterface = ScreenTextInterface.GetInstance();
        private MainMenu mainMenu = MainMenu.GetInstance();
        private ExitDoor exitDoor = new ExitDoor();
        private List<SpecialItems> specialItemsList = new List<SpecialItems>();
        private WallHackItem wallHackItem = new WallHackItem();

        private bool _gameOver = false;

        // TODO
        private Player player = Player.GetInstance(); 

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
            
            // Open main menu
            mainMenu.ShowMainMenu();

            // Draw current viewport
            currentViewport.StartNewGame();
            currentViewport.RedrawCurrentViewport();

            // Set the text interface line
            textInterface.Setup(22);

            // Draw text interface 
            textInterface.ClearText();

            // Draw the level exit door on viewport
            exitDoor.DrawOnViewport(currentViewport);

            // Add special items to list
            specialItemsList.Add(exitDoor);

            DebugLogger.Log("Starting main game loop");

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
            textInterface.ClearText();

            player.DrawOnViewport(currentViewport);
            player.WriteOnTextInterface(textInterface);
        }

        private void HandleUserInput()
        {
            var key = Console.ReadKey(true).Key;
            
            string pressedKey = key.ToString();
            DebugLogger.Log($"Pressed key: {pressedKey}");

            switch(key)
            {
                case ConsoleKey.A:
                    player.Move(-1, 0, currentViewport);
                    break;
                case ConsoleKey.W:
                    player.Move(0, -1, currentViewport);
                    break;
                case ConsoleKey.S:
                    player.Move(0, 1, currentViewport);
                    break;
                case ConsoleKey.D:
                    player.Move(1, 0, currentViewport);
                    break;
                case ConsoleKey.I:
                    player.OpenInventory(textInterface);
                    Console.ReadKey();
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
            foreach(SpecialItems specialItem in specialItemsList)
            {
                DebugLogger.Log($"Item: {specialItem.itemName}");

                if(specialItem.InteractWithItem(player, currentViewport, this) == true)
                {
                    DebugLogger.Log("interaction with player returned true");

                    if(specialItem.itemName == "ExitDoor")
                    {
                        NextGameLevel();
                    }
                }
            }
        }    

        public void NextGameLevel()
        {
            int level = player.GetPlayerLevel();
            DebugLogger.Log($"We got to the next level, current level: {level}");

            // Draw new level map
            currentViewport.RedrawCurrentViewport();

            specialItemsList.Clear();
            specialItemsList.Add(exitDoor);

            Random random = new Random();
            if(random.Next(0, 10) > 9)
            {
                specialItemsList.Add(wallHackItem);
                wallHackItem.DrawOnViewport(currentViewport);
            }

            // Draw the special itemson viewport
            exitDoor.DrawOnViewport(currentViewport);

            // Change player position
            player.GoToStartPosition();

            // Clear text interface
            textInterface.ClearText();

            // Restart game loop
            GameLoop();
        }
    }
}