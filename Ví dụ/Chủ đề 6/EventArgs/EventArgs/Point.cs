using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventArgs
{
    class PointEventArgs : System.EventArgs
    {
        public readonly int X;
        public readonly int Y;

        public PointEventArgs(int x, int y)
        {
            this.X = x;
            this.Y = x;
        }
    }

    class Point
    {
        int x;
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                if (ChangedValueEvent != null)
                    ChangedValueEvent(this, new PointEventArgs(x, y));
            }
        }

        int y;
        public int Y
        {
            get { return x; }
            set
            {
                y = value;
                if (ChangedValueEvent != null)
                    ChangedValueEvent(this, new PointEventArgs(x, y));
            }
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Kiểu sự kiện
        //public delegate void ChangedValueHandler(int newX, int newY);
        public delegate void ChangedValueHandler(object sender, PointEventArgs e);

        // Danh sách chứa các hàm xử lý sự kiện
        //private ChangedValueHandler listOfHandlers;

        // Nơi đăng ký nhận sự kiện
        //public void RegisterWithChangedValue(ChangedValueHandler methodToCall)
        //{
        //    listOfHandlers += methodToCall;
        //}

        public event ChangedValueHandler ChangedValueEvent;
    }
}
