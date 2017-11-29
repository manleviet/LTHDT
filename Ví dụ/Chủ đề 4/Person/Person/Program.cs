using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person("Steve", "Jobs");

            Console.WriteLine(p1.ToString());
            Console.WriteLine(p1);
            Console.WriteLine(p1.GetHashCode());

            Person p2 = p1;
            object o = p2;

            if (o.Equals(p1) && p2.Equals(o))
                Console.WriteLine("Same instance!");

            Console.ReadKey();
        }
    }
}
