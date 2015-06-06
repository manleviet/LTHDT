using System;

namespace QLPH
{
    class ThucHanh : Phong
    {
        private int donGia;
        public override int DonGia // nap chong don gia
        {
            get { return donGia; }
        }

        // Cau tu
        public ThucHanh(string maPhong, int sucChua, bool coThayDoi = true)
            : base (maPhong, sucChua, coThayDoi)
        {
            if (sucChua > 45)
                donGia = 500000;
            else
                donGia = 400000;
        }

        #region Nap chong Object
        public override string ToString()
        {
            string temp = "";
            temp += string.Format("Ma phong: {0}\n", MaPhong);
            temp += string.Format("Loai phong: ThucHanh\n");
            temp += string.Format("Suc chua: {0}\n", SucChua);
            return temp;
        }
        #endregion

        #region Nap chong IFormattable
        // cai dat cho IFormattable
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(format))
                format = "c";
            char first = format.ToLower()[0];
            if (format.Length == 1 && first == 'f')
            {// dinh dang theo de xuat du lieu ra file
                return string.Format("{0},ThucHanh,{1}", MaPhong, SucChua);
            }
            else if (first == 'c') // dinh dang de xuat ra console 
                return ToString();
            throw new FormatException();
        }
        #endregion
    }
}
