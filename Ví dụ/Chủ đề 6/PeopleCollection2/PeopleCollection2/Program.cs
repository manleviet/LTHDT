using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCollection2
{
    class Program
    {
        static void Main(string[] args)
        {
            PeopleCollection myPeople = new PeopleCollection();
            //thêm các đối tượng với cú pháp chỉ mục
            myPeople["An"] = new Person("An", 40);
            myPeople["Binh"] = new Person("Binh", 35);
            //lấy đối tượng sử dụng chỉ mục
            Console.WriteLine(myPeople["An"]);
            Console.WriteLine(myPeople[1]);

            Console.ReadKey();
        }
    }
}
