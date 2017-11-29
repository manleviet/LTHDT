using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point
{
    class Circle : ICloneable
    {
        public Point Center { get; set; }
        public int Radial { get; set; }

        public Circle(Point center, int radial)
        {
            Center = center;
            Radial = radial;
        }

        public override string ToString()
        {
            return string.Format("Center: {0}; Radial: {1}",
                                 Center,
                                 Radial);
        }

        public object Clone()
        {
            Circle newCircle = (Circle)this.MemberwiseClone();
            Point newPoint = new Point(Center.X, Center.Y);
            newCircle.Center = newPoint;
            return newCircle;
        }
    }
}
