using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composition
{
    class Company
    {
        string name;
        List<Person> employees;
        TaxRegistration taxReg;

        public Company() : this("", "", 1, 1, 2017) { }
        public Company(string name, string id, int day, int month, int year)
        {
            this.name = name;
            employees = new List<Person>();
            // Tạo đối tượng mới bên trong cấu tử
            this.taxReg = new TaxRegistration(id, day, month, year);
        }

        // Xoá đối tượng trong hàm huỷ tử
        ~Company()
        {
            taxReg = null;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public void AddEmployee(Person e)
        {
            employees.Add(e);
        }

        public void RemoveEmployee(Person e)
        {
            employees.Remove(e);
        }

        // Chỉ cho phép thay đổi dữ liệu, không gán đối tượng mới
        public string TaxReg
        {
            get { return taxReg.ID; }
            set { taxReg.ID = value; }
        }

        // nhận vào thông tin để tạo đối tượng mới
        public void ChangeTaxReg(string id, int day, int month, int year)
        {
            taxReg = null; // xóa đối tượng cũ
            taxReg = new TaxRegistration(id, day, month, year);
        }

        public void PrintStatus()
        {
            Console.WriteLine("Comapany Name: {0}", name);
            Console.WriteLine("Tax Registration: {0} - {1}/{2}/{3}",
                              taxReg.ID, taxReg.Day, taxReg.Month, taxReg.Year);
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
