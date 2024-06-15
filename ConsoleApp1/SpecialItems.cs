using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    public abstract class SpecialItems
    {
        protected char itemCharacter = ' ';
        protected int positionX = 1;
        protected int positionY = 1;
        public string itemName = "";
        protected ConsoleColor itemColor = ConsoleColor.White;

        public abstract void DrawOnViewport(Viewport currentViewport);

        public abstract bool InteractWithItem(Player player, Viewport currentViewport, GameEngine engine);
    }
}