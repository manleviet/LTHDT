using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XuLySoLieu
{
    class QSThongThuong : QuanSat
    {
        // M o day bang 1
        public override double TongQuanSat
        {
            get { return X; }
        }

        public override double TongQuanSat2
        {
            get { return X * X; }
        }

        public QSThongThuong(double x) : base(x) { }

        public override string ToString()
        {
            return string.Format("{0}", X);
        }

        public override bool Equals(object obj)
        {
            return this.ToString() == obj.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
