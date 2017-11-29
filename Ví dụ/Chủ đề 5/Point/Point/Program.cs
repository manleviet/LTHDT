using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p = new Point(10, 10);
            Point p1 = (Point)p.Clone();

            Console.WriteLine(p);
            Console.WriteLine(p1);

            Circle c = new Circle(p, 20);
            Circle c1 = (Circle)c.Clone();

            Console.WriteLine(c);
            Console.WriteLine(c1);

            Console.ReadKey();
        }
    }
}
