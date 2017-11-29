using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    class EmployeeCollection : IEnumerable
    {
        List<Employee> empList;

        public EmployeeCollection()
        {
            empList = new List<Employee>();
        }

        public void AddEmployee(Employee e)
        {
            empList.Add(e);
        }

        public IEnumerator GetEnumerator()
        {
            return empList.GetEnumerator();
        }

        public void RemoveEmployee(Employee e)
        {
            empList.Remove(e);
        }
    }
}
