using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System;
using System.Collections.Generic;


namespace FalloutUnderneath
{
    class GameMap
    {
        // * Whole game map is stored in here
        static private int mapWidth = 40;
        static private int mapHeight = 10;
        private char[,] wholeMap;

        // For the maze generating functions
        private Random rand = new Random();
        private static readonly int[] dx = { 0, 1, 0, -1 };
        private static readonly int[] dy = { -1, 0, 1, 0 };


        public GameMap(int width, int height)
        {
            mapWidth = width;
            mapHeight = height;

            wholeMap = new char[mapWidth*2+1, mapHeight*2+1];
        }

        public void CreateNewMap()
        {
            // Fill map with walls
            for (int y = 0; y < wholeMap.GetLength(1); y++)
            {
                for (int x = 0; x < wholeMap.GetLength(0); x++)
                {
                    wholeMap[x, y] = '#'; // Wall
                }
            }

            // Create the labyrinth
            CarvePassagesFrom(1, 1);

            // Add random walls so that it is harder to go through
            AddRandomWalls(20);
        }

        public bool GetMapFromFile(string fileName)
        {
            // TODO
            return false;
        }

        private void CarvePassagesFrom(int cx, int cy)
        {
            List<int> directions = new List<int> { 0, 1, 2, 3 };
            Shuffle(directions);

            foreach (int direction in directions)
            {
                int nx = cx + dx[direction] * 2;
                int ny = cy + dy[direction] * 2;

                if (IsInBounds(nx, ny) && wholeMap[nx, ny] == '#')
                {
                    wholeMap[cx + dx[direction], cy + dy[direction]] = ' '; // Passage
                    wholeMap[nx, ny] = ' '; // Passage
                    CarvePassagesFrom(nx, ny);
                }
            }
        }

        private void Shuffle(List<int> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                int temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        private bool IsInBounds(int x, int y)
        {
            return x > 0 && x < mapWidth * 2 && y > 0 && y < mapHeight * 2;
        }

        public void AddRandomWalls(int numberOfWalls)
        {
            int addedWalls = 0;

            while (addedWalls < numberOfWalls)
            {
                int x = rand.Next(1, mapWidth * 2);
                int y = rand.Next(1, mapHeight * 2);

                // Ensure we do not block the start or exit points
                if (wholeMap[x, y] == ' ' && !(x == 1 && y == 1) && !(x == mapWidth * 2 - 1 && y == mapHeight * 2 - 1))
                {
                    wholeMap[x, y] = '#';
                    addedWalls++;
                }
            }
        }


        // ! For testing purposes only
        public void PrintMaze()
        {
            for (int y = 0; y < wholeMap.GetLength(1); y++)
            {
                for (int x = 0; x < wholeMap.GetLength(0); x++)
                {
                    Console.Write(wholeMap[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}