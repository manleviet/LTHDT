using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point
{
    class Point : IComparable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int xPos, int yPos)
        {
            X = xPos;
            Y = yPos;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", X, Y);
        }

        public static Point operator + (Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.X + p2.Y);
        }

        public static Point operator - (Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator + (Point p1, int change)
        {
            return new Point(p1.X + change, p1.Y + change);
        }

        public static Point operator - (Point p1, int change)
        {
            return new Point(p1.X - change, p1.Y - change);
        }

        public static Point operator ++(Point p1)
        {
            return new Point(p1.X + 1, p1.Y + 1);
        }

        public static Point operator --(Point p1)
        {
            return new Point(p1.X - 1, p1.Y - 1);
        }

        //nạp chồng hai hàm Equals và GetHashCode
        public override bool Equals(object o)
        {
            return o.ToString() == this.ToString();
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        //nạp chồng toán tử bằng sử dụng hàm Equals
        public static bool operator == (Point p1, Point p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator != (Point p1, Point p2)
        {
            return !p1.Equals(p2);
        }

        //cài đặt giao diện IComparable
        public int CompareTo(object o)
        {
            if (o is Point)
            {
                Point p = (Point)o;
                if (this.X > p.X && this.Y > p.Y) return 1;
                if (this.X < p.X && this.Y < p.Y) return -1;
                else return 0;
            }
            else throw new ArgumentException();
        }
        //nạp chồng toán tử so sánh sử dụng hàm CompareTo
        public static bool operator < (Point p1, Point p2)
        {
            return (p1.CompareTo(p2) < 0);
        }

        public static bool operator > (Point p1, Point p2)
        {
            return (p1.CompareTo(p2) > 0);
        }
    }
}
