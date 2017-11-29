using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Association
{
    class Company
    {
        string name;
        List<Person> employees; // the hien ban so 0-*

        public Company() : this("") { }
        public Company(string name)
        {
            this.name = name;
            employees = new List<Person>();
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public void AddEmployee(Person e) // hoặc gán đối tượng mới thông qua hàm thành phần
        {
            employees.Add(e);
        }

        public void RemoveEmployee(Person e)
        {
            employees.Remove(e); // 
        }

        public void PrintStatus()
        {
            Console.WriteLine("Comapany Name: {0}", name);
            if (employees.Count > 0)
            {
                Console.WriteLine("Employees: ");
                foreach (Person p in employees)
                    Console.WriteLine(p.Name);
            }
            else
                Console.WriteLine("No employee.");
        }
    }
}
