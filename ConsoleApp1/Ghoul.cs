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

            Random random = new Random();

            positionX = random.Next(5, 75);
            positionY = random.Next(3, 17);
        }

        public override bool AttackPlayer(Player player, ScreenTextInterface textInterface)
        {
            WriteToTextInterface(textInterface);

            hp -= player.FightWithEnemy(dmg);

            throw new NotImplementedException();
        }
    }
}