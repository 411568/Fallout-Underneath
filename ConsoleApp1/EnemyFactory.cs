using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace FalloutUnderneath
{
    public class EnemyFactory
    {
        private Random random = new Random();

        public Enemy CreateEnemy()
        {
            int itemType = random.Next(10); // Generates a random number between 0 and 99

            if (itemType > 5)
            {
                return new Ghoul();
            }
            else
            {
                return new Ghoul();
            }
        }
    }
}