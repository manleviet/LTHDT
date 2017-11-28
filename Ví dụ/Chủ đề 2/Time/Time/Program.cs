using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time
{
    class Program
    {
        static void Main(string[] args)
        {
            Time t1 = new Time(1, 20);
            Time t2 = new Time(1, 40);

            t2.Add(t1);

            t2.PrintTime();
            Console.ReadKey();
        }
    }
}
