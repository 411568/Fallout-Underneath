using System.Runtime.CompilerServices;
using System.Security;
using System.Text;


namespace FalloutUnderneath
{
    public class FeralGhoul : Enemy
    {
        public FeralGhoul()
        {
            enemyName = "FeralGhoul";
            enemyColor = ConsoleColor.Magenta;
            enemyCharacter = '!';
            hp = 100;
            dmg = 50;

            explosionResistance = true;

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
            ;
        }
    }
}