using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPointy
{
    class Rectangle : Shape, IPointy
    {
        public Rectangle(string name) : base(name) { }

        public byte Points
        {
            get { return 4; }
        }

        public override void Draw()
        {
            Console.WriteLine("Draw a rectangle with name {0}", Name);
        }
    }
}
