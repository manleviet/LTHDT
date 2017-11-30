using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchellingModel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            int[,] grid = { {  1, 1,  0,  1,  0 },
                            { -1, 0,  0,  0,  0 },
                            {  1, 1, -1, -1, -1 },
                            {  1, 0,  1,  1,  1 },
                            {  1, 0,  0, -1,  0 } };

            GridUtility.DrawGrid(grid);
            Console.ReadKey();

            Console.Clear();
            int[,] grid1 = { { 1, -1, 0, -1,  0 },
                             { 0,  0, 0,  0,  0 },
                             { 1,  1, 1, -1,  1 },
                             { 1, -1, 1,  1,  1 },
                             { 1,  0, 0,  0, -1 } };

            GridUtility.DrawGrid(grid1);
            Console.ReadKey();

            Console.Clear();
            int[,] grid2 = new int[50, 50];

            for (int i = 0; i < 50; i++)
                for (int j = 0; j < 50; j++)
                    grid2[i, j] = 1;

            GridUtility.DrawGrid(grid2);

            Console.ReadKey();
        }
    }
}
