using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casting
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle(10, 20);
            DrawSquare((Square)rect); // ép kiểu tường minh

            Square sq2 = (Square)90; // ép kiểu tường minh
            int size = (int)sq2; // ép kiểu tường minh

            Console.WriteLine(sq2);

            Square sq3 = new Square();
            sq3.Size = 83;
            Rectangle rect1 = sq3; // ép kiểu ngầm định

            Console.WriteLine(rect1);

            Console.ReadKey();
        }

        static void DrawSquare(Square sq)
        {
            Console.WriteLine(sq);
            sq.Draw();
        }
    }
}
