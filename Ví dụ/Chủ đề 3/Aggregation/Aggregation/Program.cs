using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregation
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine e1 = new Engine("Ford 1.0L EcoBoost");

            Car c = new Car("Ford Ranger"); // tạo chiếc xe
            c.CarEngine = e1; // gắn động cơ vào
            Console.WriteLine(c.GetDescription());

            c.CarEngine = null; // tháo động cơ ra
            Console.WriteLine(c.GetDescription());
            Console.WriteLine(e1.Name); // động cơ e1 vẫn còn

            Engine e2 = new Engine("Chrysler 6.2L");
            c.CarEngine = e2; // gán động cơ mới
            Console.WriteLine(c.GetDescription());

            c = null; // hủy chiếc xe
            Console.WriteLine(e2.Name); // động cơ e2 vẫn còn

            Console.ReadKey();
        }
    }
}
