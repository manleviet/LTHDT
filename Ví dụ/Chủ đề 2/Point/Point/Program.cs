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
            Point firstPoint = new Point();
            firstPoint.X = 10;
            firstPoint.Y = 10;

            PrintPoint(firstPoint);

            Point anotherPoint = new Point(20, 20);
            PrintPoint(anotherPoint);

            // goi khong tuong minh cau tu mac dinh
            Point finalPoint = new Point { X = 30, Y = 30 };
            PrintPoint(finalPoint);

            Console.ReadKey();
        }

        static void PrintPoint(Point p)
        {
            Console.WriteLine("X:{0}-Y:{1}", p.X, p.Y);
        }
    }
}
