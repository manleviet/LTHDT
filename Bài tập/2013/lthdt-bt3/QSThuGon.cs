using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XuLySoLieu
{
    class QSThuGon : QuanSat
    {
        private int m;
        public int M
        {
            get
            {
                return m;
            }
            set
            {
                if (value <= 0)
                    m = 0;
                else
                    m = value;
            }
        }

        public override double TongQuanSat
        {
            get { return X * M; }
        }

        public override double TongQuanSat2
        {
            get { return X * X * M; }
        }

        public QSThuGon(double x, int m)
            : base(x)
        {
            M = m;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}", X, M);
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
