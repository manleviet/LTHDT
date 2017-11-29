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
            EmployeeCollection col = new EmployeeCollection();
            col.AddEmployee(new Employee("Bill Gates"));
            col.AddEmployee(new Employee("Steve Jobs"));

            foreach (Employee e in col)
                Console.WriteLine(e);

            Console.ReadKey();
        }
    }
}
