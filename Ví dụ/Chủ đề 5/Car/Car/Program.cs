using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car
{
    class Program
    {
        static void Main(string[] args)
        {
            Car[] cars = new Car[3];
            cars[0] = new Car(1, "B", 4);
            cars[1] = new Car(3, "A", 3);
            cars[2] = new Car(2, "C", 2);

            // sắp xếp theo ID
            Console.WriteLine("Sap xep theo ID");
            Array.Sort(cars);
            PrintCars(cars);

            // sắp xếp theo Name
            Console.WriteLine("Sap xep theo Name");
            Array.Sort(cars, new Car.NameComparer()); // trực tiếp tạo đối tượng
            PrintCars(cars);

            // sắp xếp theo số bánh xe
            Console.WriteLine("Sap xep theo so banh xe");
            Array.Sort(cars, Car.SortByNumWheel); // chỉ lấy đối tượng từ thuộc tính của lớp
            PrintCars(cars);

            Console.ReadKey();
        }

        static void PrintCars(Car[] cars)
        {
            foreach (Car c in cars)
                Console.WriteLine(c);
        }
    }
}
