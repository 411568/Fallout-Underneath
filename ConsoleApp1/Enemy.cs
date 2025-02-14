using System.Runtime.CompilerServices;
using System.Security;
using System.Text;


namespace FalloutUnderneath
{
    public abstract class Enemy : IDrawable
    {
        protected int hp;
        protected int dmg;

        protected int positionX;
        protected int positionY;
        protected bool explosionResistance;

        protected ConsoleColor enemyColor;
        protected char enemyCharacter = ' ';

        protected string enemyName = "";
        public string GetEnemyName()
        {
            return enemyName;
        }

        public (int, int) GetEnemyPosition()
        {
            return (positionX, positionY);
        }

        public bool GetExplosionResistance()
        {
            return explosionResistance;
        }

        public int GetEnemyHP()
        {
            return hp;
        }

        public void DrawOnViewport(Viewport currentViewport)
        {
            DebugLogger.Log($"Drawing {enemyName} on viewport");
            
            currentViewport.ChangeCharacterInPosition(positionX, positionY, enemyCharacter, enemyColor);
        }

        protected void WriteToTextInterface(ScreenTextInterface textInterface)
        {
            DebugLogger.Log($"Writing {enemyName} stats to text interface");

            textInterface.ClearText();
            textInterface.WriteTextAtLine($"You've been attacked by a {enemyName}.", 0);   
            textInterface.WriteTextAtLine($"He hit you for {dmg} hp.", 1);   

            Console.ReadKey();
        }

        public abstract bool AttackPlayer(Player player, ScreenTextInterface textInterface);
        public abstract void Move(Viewport currentViewport, Player player);
    }
}
