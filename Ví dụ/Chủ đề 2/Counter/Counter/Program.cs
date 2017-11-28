using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counter
{
    class Program
    {
        static void Main(string[] args)
        {
            // cau lenh nay sai vi ham cau tu la private
            //Counter c1 = new Counter();
            // dieu nay ngan khong tao doi tuong tu lop Counter nay
            Counter.currentCount = 0;
            Console.WriteLine(Counter.IncrementCount());
            Console.ReadKey();
        }
    }
}
