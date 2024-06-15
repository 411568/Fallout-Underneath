using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    public abstract class Item : IDrawable
    {
        protected int weight;
        protected int wearLevel;
        protected int positionX;
        protected int positionY;

        protected ConsoleColor itemColor;
        protected char itemCharacter = ' ';

        protected string itemName = "";

        public string GetItemName()
        {
            return itemName;
        }

        public (int, int) GetItemPosition()
        {
            return (positionX, positionY);
        }

        public abstract bool UseItem();

        public int GetWeight()
        {
            return weight;
        }

        public abstract string GetStats();

        public void DrawOnViewport(Viewport currentViewport)
        {
            DebugLogger.Log($"Drawing item {itemName} on viewport");
            
            currentViewport.ChangeCharacterInPosition(positionX, positionY, itemCharacter, itemColor);
        }
    }   
}