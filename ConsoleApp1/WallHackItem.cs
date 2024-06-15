using System.Runtime.CompilerServices;
using System.Security;
using System.Text;


namespace FalloutUnderneath
{
    public class WallHackItem : SpecialItems
    {
        public WallHackItem()
        {
            Random random = new Random();
    
            positionX = random.Next(10, 70);
            positionY = random.Next(2, 18);
            itemCharacter = '■';
            itemColor = ConsoleColor.Cyan;
            itemName = "WallHackItem";
        }
        public override void DrawOnViewport(Viewport currentViewport)
        {
            DebugLogger.Log("Drawing wall hack item door on viewport");
            
            Random random = new Random();

            positionX = random.Next(10, 70);
            positionY = random.Next(2, 18);

            currentViewport.ChangeCharacterInPosition(positionX, positionY, itemCharacter, itemColor);
        }

        public override bool InteractWithItem(Player player, Viewport currentViewport, GameEngine engine)
        {
            DebugLogger.Log("wall hack item interacting with player");

            var playerPosition = player.GetPlayerPosition();
            if(playerPosition.Item1 == positionX && playerPosition.Item2 == positionY)
            {
                player.TurnOnWallHack();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}