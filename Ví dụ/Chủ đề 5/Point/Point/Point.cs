using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point
{
    class Point : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public object Clone()
        {
            return new Point(this.X, this.Y);
            //return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("X:{0}; Y:{1}", this.X, this.Y);
        }
    }
}
