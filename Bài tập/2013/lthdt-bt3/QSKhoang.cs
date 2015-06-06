using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XuLySoLieu
{
    class QSKhoang : QSThuGon
    {
        public double Y { get; set; }

        // Gia tri dai dien
        public double DaiDien
        {
            get
            {
                return (X + Y) / 2;
            }
        }

        public override double TongQuanSat
        {
            get
            {
                return DaiDien * M;
            }
        }

        public override double TongQuanSat2
        {
            get
            {
                return DaiDien * DaiDien * M;
            }
        }

        public QSKhoang (double x, double y, int m)
            : base(x, m)
        {
            Y = y;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}", X, Y, M);
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
