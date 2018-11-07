using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p = new Point(0, 0);
            PointController pc = new PointController(p);

            p.X = 1;
            p.Y = 1;

            Console.ReadKey();
        }
    }
}
