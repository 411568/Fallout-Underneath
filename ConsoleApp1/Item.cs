using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    public abstract class Item
    {
        protected int weight;
        protected int wearLevel;

        protected string itemName = "";

        public string GetItemName()
        {
            return itemName;
        }

        public abstract bool UseItem();

        public abstract int GetWeight();

        public abstract string GetStats();
    }   
}