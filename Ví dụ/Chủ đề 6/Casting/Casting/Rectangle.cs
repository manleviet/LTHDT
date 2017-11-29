using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casting
{
    class Rectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(int w, int h)
        {
            Width = w;
            Height = h;
        }
        public Rectangle() { }

        public void Draw()
        {
            Console.WriteLine("Draw a Rectangle");
        }

        public override string ToString()
        {
            return string.Format("[Width = {0}; Height = {1}]", 
                                 Width, Height);
        }

        // ép kiểu ngầm định từ Square thành Rectangle
        public static implicit operator Rectangle(Square s)
        {
            Rectangle r = new Rectangle();
            r.Width = s.Size;
            r.Height = s.Size;
            return r;
        }
    }
}
