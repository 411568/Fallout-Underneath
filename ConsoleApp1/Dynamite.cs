using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    public class Dynamite : Item
    {
        public Dynamite()
        {
            weight = 10;
            itemName = "Dynamite";
            wearLevel = 1;
            itemColor = ConsoleColor.DarkYellow;
            itemCharacter = 'Í';

            Random random = new Random();

            positionX = random.Next(5, 75);
            positionY = random.Next(3, 17);
        }

        public override bool UseItem()
        {
            if(wearLevel > 0)
            {
                DebugLogger.Log("Using dynamite");

                Player player = Player.GetInstance();
                Viewport currentViewport = Viewport.GetInstance();

                wearLevel = 0;
                weight = 0;

                int startX = player.GetPlayerPosition().Item1 - 10;
                int startY = player.GetPlayerPosition().Item2 - 5;
                
                if(startX <= 0)
                {
                    startX = 1;
                }

                if(startY <= 0)
                {
                    startY = 1;
                }

                int endX = player.GetPlayerPosition().Item1 + 10;
                int endY = player.GetPlayerPosition().Item2 + 5;

                if(endX >= 80)
                {
                    endX = 1;
                }

                if(endY >= 40)
                {
                    endY = 1;
                }

                for(int x = startX; x < endX; x++)
                {
                    for(int y = startY; y < endY; y++)
                    {
                        currentViewport.ChangeCharacterInPosition(x, y, ' ', ConsoleColor.White);
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public override string GetStats()
        {
            DebugLogger.Log("Getting dynamite stats");

            string stats = itemName + ", uses left: " + wearLevel.ToString();

            return stats;
        }
    }
}