using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    class PointController
    {
        Point p;

        public PointController(Point p)
        {
            this.p = p;
            this.p.RegisterWithChangedValue(ChangedValueEvent);
        }

        // Hàm xử lý sự kiện
        public static void ChangedValueEvent(int newX, int newY)
        {
            Console.WriteLine("{0}-{1}", newX, newY);
        }
    }
}
