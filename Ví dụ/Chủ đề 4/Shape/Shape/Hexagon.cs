using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape
{
    class Hexagon : Shape
    {
        public Hexagon(string name) : base(name) { }

        public override void Draw()
        {
            Console.WriteLine("Draw a hexagon with name {0}", Name);
        }
    }
}
