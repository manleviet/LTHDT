using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
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
                    ChangedValueEvent.Invoke(value, y);
                //if (listOfHandlers != null)
                //    listOfHandlers.Invoke(value, y); // Gọi hàm xử lý sự kiện
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
                    ChangedValueEvent.Invoke(x, value);
                //if (listOfHandlers != null)
                //    listOfHandlers.Invoke(x, value); // Gọi hàm xử lý sự kiện
            }
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Kiểu sự kiện
        public delegate void ChangedValueHandler(int newX, int newY);

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
