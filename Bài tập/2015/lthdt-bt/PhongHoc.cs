using System;

namespace QLPH
{
    class PhongHoc : Phong
    {
        private int donGia;
        public override int DonGia // nap chong don gia
        {
            get { return this.donGia; }
        }

        public PhongHoc(string maPhong, int sucChua, bool coThayDoi = true)
            : base (maPhong, sucChua, coThayDoi)
        {
            if (sucChua > 40)
                donGia = 300000;
            else
                donGia = 200000;
        }

        #region Nap chong Object
        public override string ToString()
        {
            string temp = "";
            temp += string.Format("Ma phong: {0}\n", MaPhong);
            temp += string.Format("Loai phong: PhongHoc\n");
            temp += string.Format("Suc chua: {0}\n", SucChua);
            return temp;
        }
        #endregion

        #region Nap chong IFormattable
        // Sau khi cai dat ham nay, co the su dung nhu dang ham ToString
        // nhung kem them ky tu de dinh dang cho phu hop
        // O day, "c" la dai dien cho Console - du lieu xuat ra de hien thi tren Console
        // "f" la dai dien cho File - du lieu xuat ra de luu file
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(format))
                format = "c";
            char first = format.ToLower()[0];
            if (format.Length == 1 && first == 'f')
            {// dinh dang theo de xuat du lieu ra file
                return string.Format("{0},PhongHoc,{1}", MaPhong, SucChua);
            }
            else if (first == 'c') // dinh dang de xuat ra console 
                return ToString();
            throw new FormatException();
        }
        #endregion
    }
}
