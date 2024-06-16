using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    public class Pickaxe : Item
    {
        public Pickaxe()
        {
            weight = 10;
            itemName = "Pickaxe";
            wearLevel = 100;
            itemColor = ConsoleColor.DarkMagenta;
            itemCharacter = '┌';

            Random random = new Random();

            positionX = random.Next(5, 75);
            positionY = random.Next(3, 17);
        }

        public int GetWearLevel()
        {
            return wearLevel;
        }

        public override bool UseItem()
        {
            DebugLogger.Log("Using pickaxe");

            if(wearLevel > 0)
            {
                wearLevel--;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string GetStats()
        {
            DebugLogger.Log("Getting pickaxe stats");

            string stats = itemName + ", wear: " + wearLevel.ToString();

            return stats;
        }
    }
}