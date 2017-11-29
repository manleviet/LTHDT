using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape
{
    class ThreeDCircle : Circle
    {
        public ThreeDCircle(string name, int radial) : base(name, radial) { }

        public override void Draw()
        {
            Console.WriteLine("Draw a 3D circle with name {0} and radial {1}",
                              Name,
                              radial);
        }
    }
}
