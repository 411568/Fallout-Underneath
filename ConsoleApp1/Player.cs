using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace FalloutUnderneath
{
    class Player : IDrawable
    {
        private Player() {}

        private char playerCharacter = '@';
        private ConsoleColor playerColor = ConsoleColor.Green;
        private int playerX = 1;
        private int playerY = 1;
        private int previousX = 1;
        private int previousY = 1;

        private int playerSpeed = 1;
        private bool digThroughWall = false;
        
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
                            // TODO
                            // Check our pickaxe stats
                            previousX = playerX;
                            playerX++;

                            digThroughWall = false;
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
                            // TODO
                            // Check our pickaxe stats
                            previousX = playerX;
                            playerX--;

                            digThroughWall = false;
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
                            // TODO
                            // Check our pickaxe stats
                            previousY = playerY;
                            playerY++;

                            digThroughWall = false;
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
                            // TODO
                            // Check our pickaxe stats
                            previousY = playerY;
                            playerY++;

                            digThroughWall = false;
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