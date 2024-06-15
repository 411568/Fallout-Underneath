using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    public class ExitDoor : SpecialItems
    {
        public ExitDoor()
        {
            positionX = 79;
            positionY = 19;
            itemCharacter = '▓';
            itemColor = ConsoleColor.Blue;
            itemName = "ExitDoor";
        }
        public override void DrawOnViewport(Viewport currentViewport)
        {
            DebugLogger.Log("Drawing exit door on viewport");
            
            currentViewport.ChangeCharacterInPosition(positionX, positionY, itemCharacter, itemColor);
        }

        public override bool InteractWithItem(Player player, Viewport currentViewport, GameEngine engine)
        {
            DebugLogger.Log("Exit door interacting with player");

            var playerPosition = player.GetPlayerPosition();
            if(playerPosition.Item1 == positionX && playerPosition.Item2 == positionY)
            {
                player.IncreaseLevel();
                currentViewport.ChangeViewport(player.GetPlayerLevel());

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}