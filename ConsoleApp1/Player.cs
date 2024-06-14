using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace FalloutUnderneath
{
    class Player : IDrawable, ITextInterface
    {
        private char playerCharacter = '@';
        private ConsoleColor playerColor = ConsoleColor.Green;
        private int playerX;
        private int playerY;
        private int previousX;
        private int previousY;

        private int playerSpeed;
        private bool digThroughWall;

        private int playerLevel;

        private int playerHP;

        private Inventory inventory;

        private Player()
        {
            DebugLogger.Log("Initializing player");

            playerLevel = 1;
            inventory = new Inventory(playerLevel);
            playerX = 1;
            playerY = 1;
            previousX = 1;
            previousY = 1;
            playerSpeed = 1;
            digThroughWall = false;
            playerHP = 100;

            inventory.AddItemToInventory(new Pickaxe());
        }
        
        private static Player? _instance;
        public static Player GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Player();
            }

            return _instance;
        }

        public void DrawOnViewport(Viewport currentViewport)
        {
            DebugLogger.Log("Drawing player on viewport");
            
            currentViewport.ChangeCharacterInPosition(previousX, previousY, ' ', ConsoleColor.Black);
            currentViewport.ChangeCharacterInPosition(playerX, playerY, playerCharacter, playerColor);

            // Update player position
            previousX = playerX;
            previousY = playerY;
        }

        public void WriteOnTextInterface(ScreenTextInterface textInterface)
        {
            DebugLogger.Log("Writing player stats to text interface");

            textInterface.WriteTextAtLine("Player stats: level " + playerLevel.ToString() + ", hp " + playerHP.ToString(), 0);
        }

        public void OpenInventory(ScreenTextInterface textInterface)
        {
            inventory.ShowInventory(textInterface);
        }


        private bool CheckPickaxeState()
        {
            DebugLogger.Log("Checking pickaxe state");

            Pickaxe? myPickaxe = (Pickaxe?)inventory.GetItemFromInventory("Pickaxe");

            if(myPickaxe != null)
            {
                if(myPickaxe.GetWearLevel() > 0)
                {
                    myPickaxe.UseItem();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public void Move(int xInput, int yInput, Viewport currentViewport)
        {
            if(xInput != 0 && yInput != 0)
            {
                DebugLogger.LogError("Invalid player move input");
            }
            else
            {
                DebugLogger.Log($"Moving player in direction X: {xInput}, Y: {yInput}");

                if(xInput == 1)
                {
                    if(currentViewport.GetCharacterFromPosition(playerX+playerSpeed, playerY) == '#')
                    {
                        // Double check if the player wants to try and go through wall
                        if(digThroughWall == true)
                        {
                            // Check our pickaxe stats
                            if(CheckPickaxeState())
                            {
                                previousX = playerX;
                                playerX++;

                                digThroughWall = false;
                            }
                        }
                        else
                        {
                            digThroughWall = true;
                        }
                    }
                    else
                    {
                        previousX = playerX;
                        playerX++;

                        digThroughWall = false;
                    }
                }
                if(xInput == -1)
                {
                    if(currentViewport.GetCharacterFromPosition(playerX-playerSpeed, playerY) == '#')
                    {
                        // Double check if the player wants to try and go through wall
                        if(digThroughWall == true)
                        {
                            // Check our pickaxe stats
                            if(CheckPickaxeState())
                            {
                                previousX = playerX;
                                playerX--;

                                digThroughWall = false;
                            }
                        }
                        else
                        {
                            digThroughWall = true;
                        }
                    }
                    else
                    {
                        previousX = playerX;
                        playerX--;

                        digThroughWall = false;
                    }
                }

                if(yInput == 1)
                {
                    if(currentViewport.GetCharacterFromPosition(playerX, playerY+playerSpeed) == '#')
                    {
                        // Double check if the player wants to try and go through wall
                        if(digThroughWall == true)
                        {
                            // Check our pickaxe stats
                            if(CheckPickaxeState())
                            {
                                previousY = playerY;
                                playerY++;

                                digThroughWall = false;
                            }
                        }
                        else
                        {
                            digThroughWall = true;
                        }
                    }
                    else
                    {
                        previousY = playerY;
                        playerY++;

                        digThroughWall = false;
                    }
                }
                if(yInput == -1)
                {
                    if(currentViewport.GetCharacterFromPosition(playerX, playerY-playerSpeed) == '#')
                    {
                        // Double check if the player wants to try and go through wall
                        if(digThroughWall == true)
                        {
                            // Check our pickaxe stats
                            if(CheckPickaxeState())
                            {
                                previousY = playerY;
                                playerY--;

                                digThroughWall = false;
                            }
                        }
                        else
                        {
                            digThroughWall = true;
                        }
                    }
                    else
                    {
                        previousY = playerY;
                        playerY--;

                        digThroughWall = false;
                    }
                }
            }
        }
    }
}