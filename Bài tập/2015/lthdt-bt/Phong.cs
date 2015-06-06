using System;

namespace QLPH
{
    abstract class Phong : IComparable, IFormattable
    {
        #region Khai bao bien va thuoc tinh
        private string maPhong;
        public string MaPhong
        {
            get
            {
                return maPhong; // chi cho set maPhong o ham cau tu
            }
        }

        private int sucChua = 0;
        public int SucChua
        {
            get { return this.sucChua; }
            set
            {
                if (sucChua != value && value > 0)
                {
                    sucChua = value;
                    CoThayDoi = true;
                }
            }
        }

        abstract public int DonGia { get; } // yeu cau cac lop con phai co them don gia

        // Thuoc tinh quan ly su thay doi du lieu cua doi tuong Phong
        // duoc dung de quyet dinh co luu du lieu xuong tap tin hay khong?
        public bool CoThayDoi
        {
            get;
            set;
        }
        #endregion

        // Cau tu
        public Phong(string maPhong, int sucChua, bool coThayDoi = true)
        {
            this.maPhong = maPhong;
            SucChua = sucChua;
            CoThayDoi = coThayDoi;
        }

        #region Nap chong Object
        abstract public override string ToString();

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return GetHashCode() == obj.GetHashCode();
        }
        #endregion

        #region Nap chong IComparable
        // cai dat cho IComparable
        public int CompareTo(object o)
        {
            Phong p = o as Phong;
            if (p != null)
            {
                return this.MaPhong.CompareTo(p.MaPhong);
            }
            throw new ArgumentException("Khong phai doi tuong kieu Phong");
        }
        #endregion

        #region Nap chong IFormattable
        // cai dat cho IFormattable
        abstract public string ToString(string format, IFormatProvider formatProvider);

        public virtual string ToString(string format)
        {
            return ToString(format, null);
        }

        public virtual string ToString(IFormatProvider formatProvider)
        {
            return ToString(null, formatProvider);
        }
        #endregion

        #region Nap chong toan tu
        // Nap chong toan tu ==
        public static bool operator ==(Phong n1, Phong n2)
        {
            return n1.Equals(n2);
        }

        // Nap chong toan tu !=
        public static bool operator !=(Phong n1, Phong n2)
        {
            return !n1.Equals(n2);
        }

        // Nap chong toan tu <
        public static bool operator <(Phong n1, Phong n2)
        {
            return (n1.CompareTo(n2) < 0);
        }

        // Nap chong toan tu >
        public static bool operator >(Phong n1, Phong n2)
        {
            return (n1.CompareTo(n2) > 0);
        }

        // Nap chong toan tu <=
        public static bool operator <=(Phong n1, Phong n2)
        {
            return (n1.CompareTo(n2) <= 0);
        }

        // Nap chong toan tu >=
        public static bool operator >=(Phong n1, Phong n2)
        {
            return (n1.CompareTo(n2) >= 0);
        }
        #endregion
    }
}
