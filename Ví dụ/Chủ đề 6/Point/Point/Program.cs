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
            Point p1 = new Point(10, 10);
            Point p2 = new Point(20, 20);

            Point p3 = p1 + p2;
            Console.WriteLine(p3);

            Point p4 = p1 - 5;
            Console.WriteLine(p4);

            p1++;
            Console.WriteLine(p1);

            if (p1 != p2) Console.WriteLine("Not equal");

            if (p1 < p2) Console.WriteLine("Less than");

            Console.ReadKey();
        }
    }
}
