using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape
{
    class Circle : Shape
    {
        protected int radial;
        public int Radial
        {
            get { return radial; }
        }

        public Circle(string name, int radial) : base(name)
        {
            this.radial = radial;
        }

        public override void Draw()
        {
            Console.WriteLine("Draw a circle with name {0} and radial {1}", Name, radial);
        }
    }
}
