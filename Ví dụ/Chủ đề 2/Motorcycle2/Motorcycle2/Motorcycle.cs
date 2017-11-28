using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle2
{
    class Motorcycle
    {
        string driverName;
        int driverIntensity;

        // Ham cau tu dung tham so mac dinh
        public Motorcycle(int intensity = 0, string name ="")
        {
            if (intensity > 10)
                intensity = 10;
            driverIntensity = intensity;
            driverName = name;
        }

        public void PrintStatus()
        {
            Console.WriteLine("{0}-{1}", driverName, driverIntensity);
        }
    }
}
