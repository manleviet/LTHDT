using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            PeopleCollection myPeople = new PeopleCollection();
            //thêm các đối tượng với cú pháp chỉ mục
            myPeople[0] = new Person("An", 40);
            myPeople[1] = new Person("Binh", 35);
            //lấy đối tượng sử dụng chỉ mục
            for (int i = 0; i < myPeople.Count; i++)
                Console.WriteLine(myPeople[i]);

            Console.ReadKey();
        }
    }
}
