using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCE
{
    class Program
    {
        static void Main(string[] args)
        {
            HCEPerson binh = new HCEPerson(901289, "Hoang Van Binh", "1 Le Loi");
            Student an = new Student(971232, "Nguyen Van An", "100 Phung Hung", 43, 2);

            Class c = new Class("HTTT4253");
            an.AddClassTaken(c);

            Console.WriteLine(binh.DisplayProfile());
            Console.WriteLine(an.DisplayProfile());

            binh = an;

            // Hàm DisplayProfile được gọi dựa trên Kiểu khai báo của biến binh
            Console.WriteLine(binh.DisplayProfile());

            // Phải thực hiện ép kiểu
            Student tam = (Student)binh;

            Console.ReadLine();
        }
    }
}
