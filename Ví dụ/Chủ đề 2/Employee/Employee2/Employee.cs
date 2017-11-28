using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee2
{
    class Employee
    {
        // Thuoc tinh static
        private static string companyName;
        public static string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        private string empSSN;
        public string SocialSecurityNumber // thuoc tinh chi doc
        {
            get { return empSSN; }
        }

        private string empName;
        public string Name
        {
            get { return empName; }
            set
            {
                if (value.Length > 15)
                    empName = "";
                else
                    empName = value;
            }
        }

        private int empAge;
        public int Age
        {
            get { return empAge; }
            set
            {
                if (value < 18)
                    empAge = 18;
                else
                    empAge = value;
            }
        }

        // thuoc tinh tu dong
        public float Pay { get; set; }

        public Employee()
        {
            Name = "";
            Age = 18;
            empSSN = "";
            Pay = 0;
        }

        public Employee(string name, int age, string ssn, float pay)
        {
            Name = name;
            Age = age;
            empSSN = ssn;
            Pay = pay;
        }

        static Employee()
        {
            companyName = "HCE";
        }
    }
}
