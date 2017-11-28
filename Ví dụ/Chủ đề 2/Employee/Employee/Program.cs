using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee e = new Employee();
            e.SetName("Joe");
            Console.WriteLine(e.GetName());
            Console.ReadKey();
        }
    }
}
