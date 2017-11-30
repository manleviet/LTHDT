using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchellingModel
{
    class GridUtility
    {
        public static void DrawGrid(int[,] grid)
        {
            if (grid.GetLength(0) > 50 || grid.GetLength(1) > 50)
                return;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                    if (grid[i, j] == 0)
                        Console.Write("O ");
                    else if (grid[i, j] == 1)
                        Console.Write("X ");
                    else
                        Console.Write("  ");
                Console.WriteLine();
            }
        }
    }
}
