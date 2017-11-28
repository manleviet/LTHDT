using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    class Employee
    {
        private string empName;

        public string GetName()
        {
            return empName;
        }

        public void SetName(string name)
        {
            if (name.Length <= 15)
                empName = name;
        }
    }
}
