using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventArgs
{
    class PointController
    {
        Point p;

        public PointController(Point p)
        {
            this.p = p;
            //this.p.RegisterWithChangedValue(ChangedValueEvent);
            p.ChangedValueEvent += ChangedValueEvent;
        }

        ~PointController()
        {
            p.ChangedValueEvent -= ChangedValueEvent;
        }

        // Hàm xử lý sự kiện
        public static void ChangedValueEvent(object sender, PointEventArgs e)
        {
            Console.WriteLine("{0}-{1}", e.X, e.Y);
        }
    }
}
