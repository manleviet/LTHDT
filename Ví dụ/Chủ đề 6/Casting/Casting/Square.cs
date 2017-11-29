using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casting
{
    class Square
    {
        public int Size { get; set; }

        public Square(int s)
        {
            Size = s;
        }
        public Square() { }

        public void Draw()
        {
            Console.WriteLine("Draw a Square");
        }

        public override string ToString()
        {
            return string.Format("[Size = {0}]", Size);
        }

        // ép kiểu tường minh từ Rectangle về Square
        public static explicit operator Square(Rectangle r)
        {
            Square s = new Square();
            s.Size = r.Height;
            return s;
        }

        // ép kiểu tường minh từ int thành Square
        public static explicit operator Square(int size)
        {
            Square s = new Square();
            s.Size = size;
            return s;
        }

        // ép kiểu tường minh từ Square thành int
        public static explicit operator int(Square s)
        {
            return s.Size;
        }
    }
}
