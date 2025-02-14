using System.Numerics;
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
        private MainMenu mainMenu = MainMenu.GetInstance()
        private ExitDoor exitDoor = new ExitDoor();
        private List<SpecialItems> specialItemsList = new List<SpecialItems>();
        private List<Item> itemList = new List<Item>();
        private WallHackItem wallHackItem = new WallHackItem();

        private List<Enemy> enemyList = new List<Enemy>();

        private bool _gameOver = false;

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

            foreach(Item item in itemList)
            {
                item.DrawOnViewport(currentViewport);
            }

            List<int> enemyVector = new List<int>();
            foreach(Enemy enemy in enemyList)
            {
                if(enemy.GetExplosionResistance() == true)
                {
                    enemy.DrawOnViewport(currentViewport);
                }
                else
                {
                    if(currentViewport.GetCharacterFromPosition(enemy.GetEnemyPosition().Item1, enemy.GetEnemyPosition().Item2) == ' ')
                    {
                        enemyVector.Add(enemyList.IndexOf(enemy));
                    }
                }
            }

            foreach(int elem in enemyVector)
            {
                enemyList.RemoveAt(elem);
            }


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
            // Checking interactions with special items
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

            // Checking interactions with normal items (picking them up and adding to inventory) 
            (int, int) playerPosition = player.GetPlayerPosition();
            int itemNum = -1;

            foreach(Item item in itemList)
            {
                string name = item.GetItemName();
                DebugLogger.Log($"Checking item: {name}");

                (int, int) itemPosition = item.GetItemPosition();

                if(itemPosition == playerPosition)
                {
                    bool result = player.AddItemToInventory(item, textInterface);
                    if(item != null && itemList != null)
                    {
                        if(result == true)
                        {
                            itemNum = itemList.IndexOf(item);
                        }
                    }
                    else
                    {
                        DebugLogger.LogError("Item could not be removed");
                    }
                }
            }

            if(itemNum != -1 && itemList != null)
            {
                DebugLogger.Log("Item removed from list");
                itemList.RemoveAt(itemNum);
            }


            // Checking interactions with enemies
            if(enemyList != null)
            {
                foreach(Enemy enemy in enemyList)
                {
                    enemy.Move(currentViewport, player);
                }
            }

            int enemyNum = -1;
            if(enemyList != null)
            {
                foreach(Enemy enemy in enemyList)
                {
                    string name = enemy.GetEnemyName();
                    DebugLogger.Log($"Checking enemy: {name}");

                    if(playerPosition == enemy.GetEnemyPosition())
                    {
                        bool result = enemy.AttackPlayer(player, textInterface);

                        if(result == false)
                        {
                            mainMenu.DeathScreen();
                        }


                        if(enemy != null && enemyList != null)
                        {
                            if(result == true)
                            {
                                enemyNum = enemyList.IndexOf(enemy);
                            }
                        }
                        else
                        {
                            DebugLogger.LogError("Enemy could not be removed");
                        }
                    }
                }
            }

            if(enemyNum != -1 && enemyList != null)
            {
                DebugLogger.Log("Enemy removed from list");
                enemyList.RemoveAt(enemyNum);
            }
        }    

        public void NextGameLevel()
        {
            int level = player.GetPlayerLevel();
            DebugLogger.Log($"We got to the next level, current level: {level}");

            // Draw new level map
            currentViewport.RedrawCurrentViewport();

            // * Spawning special items
            specialItemsList.Clear();
            specialItemsList.Add(exitDoor);

            Random random = new Random();
            if(random.Next(10) > 7)
            {
                specialItemsList.Add(wallHackItem);
                wallHackItem.DrawOnViewport(currentViewport);
            }

            // Draw the special items on viewport
            exitDoor.DrawOnViewport(currentViewport);

            // * Spawning normal items for the user to pick up
            itemList.Clear();
            int itemCount = random.Next(3);
            ItemFactory itemFactory = new ItemFactory();
            for(int i = 0; i < itemCount; i++)
            {
                itemList.Add(itemFactory.CreateItem());
            }

            foreach(Item item in itemList)
            {
                item.DrawOnViewport(currentViewport);
            }
            
            // * Spawning enemies on the map
            enemyList.Clear();
            int enemyCount = random.Next(10);
            EnemyFactory enemyFactory = new EnemyFactory();
            for(int i = 0; i < enemyCount; i++)
            {
                enemyList.Add(enemyFactory.CreateEnemy(level));
            } 

            foreach(Enemy enemy in enemyList)
            {
                enemy.DrawOnViewport(currentViewport);
            }

            // Change player position
            player.GoToStartPosition();

            // Clear text interface
            textInterface.ClearText();

            // Restart game loop
            GameLoop();
        }
    }
}