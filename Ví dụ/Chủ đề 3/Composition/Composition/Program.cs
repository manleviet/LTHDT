using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composition
{
    class Program
    {
        static void Main(string[] args)
        {
            Company c = new Company("Microsoft", "007", 10, 11, 2017);

            Person p1 = new Person("Bill Gates");
            Person p2 = new Person("Steve Jobs");

            c.AddEmployee(p1);
            c.AddEmployee(p2);
            p1.WorkAt = c;
            p2.WorkAt = c;

            c.PrintStatus();

            c = null; // xóa công ty thì mã số thuế cũng mất theo
            
            Console.ReadKey();
        }
    }
}
