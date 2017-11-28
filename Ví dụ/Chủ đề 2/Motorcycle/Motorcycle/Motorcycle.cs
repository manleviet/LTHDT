using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle
{
    class Motorcycle
    {
        string name;
        int intensity;

        public Motorcycle() { }
        public Motorcycle(int intensity) : this(intensity, "") { }
        public Motorcycle(string name) : this(0, name) { }
        public Motorcycle(int intensity, string name)
        {
            if (intensity > 10)
                intensity = 10;
            this.intensity = intensity;
            this.name = name;
        }

        public void PrintStatus()
        {
            Console.WriteLine("{0}-{1}", this.name, this.intensity);
        }
    }
}
