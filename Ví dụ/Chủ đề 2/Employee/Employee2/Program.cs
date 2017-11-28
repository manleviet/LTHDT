using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee2
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee joe = new Employee();
            PrintEmployee(joe);

            joe.Name = "Joe";
            PrintEmployee(joe);

            joe.Age++;
            PrintEmployee(joe);

            Console.ReadKey();
        }

        static void PrintEmployee(Employee e)
        {
            Console.WriteLine("{0}-{1}-{2}-{3}",
                              e.SocialSecurityNumber,
                              e.Name,
                              e.Age,
                              e.Pay);
        }
    }
}
