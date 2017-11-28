using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    class Program
    {
        static void Main(string[] args)
        {
            Garage g = new Garage();

            // cau lenh nay loi
            // vi MyAuto la null
            Console.WriteLine(g.MyAuto.Name);

            Console.ReadKey();
        }
    }
}
