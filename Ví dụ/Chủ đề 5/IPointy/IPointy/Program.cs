using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPointy
{
    class Program
    {
        static void Main(string[] args)
        {
            Shape[] shapes = new Shape[4];
            shapes[0] = new Hexagon("hex");
            shapes[1] = new Circle("cir", 10);
            shapes[2] = new ThreeDCircle("3D", 20);
            shapes[3] = new Rectangle("rec");

            foreach (Shape s in shapes)
                if (s is IPointy)
                    Console.WriteLine(((IPointy)s).Points);

            IPointy p = FindFirstPointyShape(shapes);
            PointMe(p);

            Console.ReadKey();
        }

        static void PointMe(IPointy p)
        {
            Console.WriteLine(p.Points);
        }

        static IPointy FindFirstPointyShape(Shape[] shapes)
        {
            foreach (Shape s in shapes)
                if (s is IPointy) return s as IPointy;
            return null;
        }
    }
}
