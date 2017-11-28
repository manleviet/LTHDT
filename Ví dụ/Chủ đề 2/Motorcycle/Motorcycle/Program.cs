using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle
{
    class Program
    {
        static void Main(string[] args)
        {
            Motorcycle m1 = new Motorcycle();
            m1.PrintStatus();

            Motorcycle m2 = new Motorcycle(9);
            m2.PrintStatus();

            Motorcycle m3 = new Motorcycle("Kawasaki");
            m3.PrintStatus();

            Motorcycle m4 = new Motorcycle(10, "Ducati");
            m4.PrintStatus();

            Console.ReadKey();
        }
    }
}
