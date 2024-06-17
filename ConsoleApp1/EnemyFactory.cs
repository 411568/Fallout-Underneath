using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace FalloutUnderneath
{
    public class EnemyFactory
    {
        private Random random = new Random();

        public Enemy CreateEnemy(int level)
        {
            int enemyType = random.Next(10); // Generates a random number between 0 and 99

            if(level > 5) // TODO if level > 5 create stronger enemies
            {
                if (enemyType > 2)
                {
                    return new FeralGhoul();
                }
                else
                {
                    return new Ghoul();
                }
            }
            else
            {
                if (enemyType > 8)
                {
                    return new FeralGhoul();
                }
                else
                {
                    return new Ghoul();
                }
            }
        }
    }
}