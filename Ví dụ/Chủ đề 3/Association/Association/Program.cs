using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Association
{
    class Program
    {
        static void Main(string[] args)
        {
            Company c = new Company("Microsoft");
            Person p1 = new Person("Bill Gates");
            Person p2 = new Person("Steve Jobs");

            c.AddEmployee(p1);
            c.AddEmployee(p2);
            p1.WorkAt = c;
            p2.WorkAt = c;

            c.PrintStatus();
            p1.PrintStatus();
            p2.PrintStatus();

            p2.WorkAt = null; // Steve Jobs chet
            c.RemoveEmployee(p2);

            p2.PrintStatus();
            c.PrintStatus();
            Console.ReadKey();
        }
    }
}
