using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace FalloutUnderneath
{
    public class ItemFactory
    {
        private Random random = new Random();

        public Item CreateItem()
        {
            int itemType = random.Next(2); // Generates a random number between 0 and 99

            if (itemType == 0)
            {
                return new Dynamite();
            }
            else
            {
                return new Pickaxe();
            }
        }
    }
}