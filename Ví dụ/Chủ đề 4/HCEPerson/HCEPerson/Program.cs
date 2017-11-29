using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCEPerson
{
    class Program
    {
        static void Main(string[] args)
        {
            List<HCEPerson> people = new List<HCEPerson>();

            Teacher binh = new Teacher(901289, "Hoang Van Binh", "1 Le Loi", Teacher.Position.Adjunct);
            Student an = new Student(971232, "Nguyen Van An", "100 Phung Hung", 43, 2);

            Class c = new Class("HTTT4253");
            binh.AddClass(c);
            an.AddClassTaken(c);

            people.Add(binh);
            people.Add(an);

            foreach (HCEPerson p in people)
                Console.WriteLine(p.DisplayProfile()); // Thể hiện ứng dụng thứ nhất của Đa hình (Slide 15)

            // Thể hiện ứng dụng thứ hai của Đa hình (Slide 15)
            PrintStatus(binh);
            PrintStatus(an);

            Console.ReadKey();
        }

        // Thể hiện ứng dụng thứ hai của Đa hình (Slide 15)
        static void PrintStatus(HCEPerson p)
        {
            Console.WriteLine(p.DisplayProfile());
        }
    }
}
