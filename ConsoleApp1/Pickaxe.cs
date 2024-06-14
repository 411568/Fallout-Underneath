using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    public class Pickaxe : Item
    {
        public Pickaxe()
        {
            weight = 5;
            itemName = "Pickaxe";
            wearLevel = 10;
        }

        public int GetWearLevel()
        {
            return wearLevel;
        }

        public override int GetWeight()
        {
            return weight;
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