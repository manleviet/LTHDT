using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape
{
    class Program
    {
        static void Main(string[] args)
        {
            Shape[] shapes = new Shape[4];
            shapes[0] = new Hexagon("hex");
            shapes[1] = new Circle("cir", 10);
            shapes[2] = new ThreeDCircle("3D", 20);
            shapes[3] = new Circle("cir1", 30);

            foreach (Shape s in shapes)
                s.Draw();

            foreach (Shape s in shapes)
                if (s is Circle)
                {
                    Circle c = s as Circle;
                    Console.WriteLine("Radial: {0}", c.Radial);
                }

            Console.ReadKey();
        }
    }
}
