using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPointy
{
    class Hexagon : Shape, IPointy
    {
        public Hexagon(string name) : base(name) { }

        public byte Points
        {
            get { return 6; }
        }

        public override void Draw()
        {
            Console.WriteLine("Draw a hexagon with name {0}", Name);
        }
    }
}
