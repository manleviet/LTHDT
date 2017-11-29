using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    class Program
    {
        static void Main(string[] args)
        {
            Garage g = new Garage();
            g.Car1 = new Car("Honda");
            g.Car2 = new Car("BMW");
            g.Car3 = new Car("Porsche");

            foreach (Car c in g)
                Console.WriteLine(c);

            Console.ReadKey();
        }
    }
}
