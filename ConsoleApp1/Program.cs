using System.Text;

namespace FalloutUnderneath
{
    class Program
    {
        static int Main()
        {
            Console.SetWindowSize(100, 30);

            GameMap testMap = new GameMap(40, 10);
            testMap.CreateNewMap();
            testMap.PrintMaze();


            ScreenTextInterface textInterface = ScreenTextInterface.GetInstance();
            textInterface.Setup(25);
            textInterface.WriteText("SADASDAASD");


            var name = Console.ReadLine(); 
            return 0;
        }
    }
}