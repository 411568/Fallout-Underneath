using System.Runtime.CompilerServices;
using System.Security;
using System.Text;


namespace FalloutUnderneath
{
    public class Ghoul : Enemy
    {
        public Ghoul()
        {
            enemyName = "Ghoul";
            enemyColor = ConsoleColor.Red;
            enemyCharacter = '!';
            hp = 100;
            dmg = 10;

            explosionResistance = false; 

            Random random = new Random();

            positionX = random.Next(5, 75);
            positionY = random.Next(3, 17);
        }

        public override bool AttackPlayer(Player player, ScreenTextInterface textInterface)
        {
            WriteToTextInterface(textInterface);

            hp -= player.FightWithEnemy(dmg);

            if(player.GetPlayerHP() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void Move(Viewport currentViewport, Player player)
        {
            DebugLogger.Log("Moving enemy to player");

            int playerX = player.GetPlayerPosition().Item1;
            int playerY = player.GetPlayerPosition().Item2;

            if(playerX > positionX)
            {
                MoveEnemy(1, 0, currentViewport);
            }    
            else if(playerX < positionX)
            {
                MoveEnemy(-1, 0, currentViewport);
            }
            else if(playerY > positionY)
            {
                MoveEnemy(0, 1, currentViewport);
            }
            else if(playerY < positionY)
            {
                MoveEnemy(0, -1, currentViewport);
            }
        }

        private void MoveEnemy(int x, int y, Viewport currentViewport)
        {
            if(currentViewport.GetCharacterFromPosition(positionX, positionY) != ' ') // check if the enemy died from some item
            {
                currentViewport.ChangeCharacterInPosition(positionX, positionY, ' ', ConsoleColor.Black); // clear the previous position

                if(currentViewport.GetCharacterFromPosition(positionX + x, positionY + y) == ' ') // move if not a wall
                {
                    positionX += x;
                    positionY += y;
                }

                currentViewport.ChangeCharacterInPosition(positionX, positionY, enemyCharacter, enemyColor); // draw in the current position
            }
        }
    }
}