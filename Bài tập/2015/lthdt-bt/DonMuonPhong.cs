using System;

namespace QLPH
{
    class DonMuonPhong : IComparable, IFormattable
    {
        #region Khai bao bien va thuoc tinh
        private string ma;
        public string Ma
        {
            get { return ma; } // khong cho set ma
        }

        private string tenNguoiMuon = null;
        public string TenNguoiMuon
        {
            get { return this.tenNguoiMuon; }
            set
            {
                if (!string.IsNullOrEmpty(value) && tenNguoiMuon != value)
                {
                    tenNguoiMuon = value;
                    CoThayDoi = true;
                }
            }
        }

        private string dvCongTac = null;
        public string DVCongTac
        {
            get { return this.dvCongTac; }
            set
            {
                if (!string.IsNullOrEmpty(value) && dvCongTac != value)
                {
                    dvCongTac = value;
                    CoThayDoi = true;
                }
            }
        }

        // cai dat quan he ?
        private Phong phongMuon = null;
        public Phong PhongMuon
        {
            get { return this.phongMuon; }
            set
            {
                if (value != null && value != phongMuon)
                {
                    phongMuon = value;
                    CoThayDoi = true;
                }
            }
        }

        private NgayThang ngayMuon = null;
        public NgayThang NgayMuon
        {
            get { return ngayMuon; }
            set
            {
                if (value != null && value != ngayMuon)
                {
                    ngayMuon = value;
                    CoThayDoi = true;
                }
            }
        }
        
        private int tietBDMuon = -1;
        public int TietBDMuon
        {
            get { return tietBDMuon; }
            set
            {
                if (tietBDMuon != value)
                {
                    tietBDMuon = value;
                    CoThayDoi = true;
                }
            }
        }

        private int tietKTMuon = -1;
        public int TietKTMuon
        {
            get { return tietKTMuon; }
            set
            {
                if (tietKTMuon != value)
                {
                    tietKTMuon = value;
                    CoThayDoi = true;
                }
            }
        }

        // Thuoc tinh quan ly su thay doi du lieu cua doi tuong DonMuonPhong
        // duoc dung de quyet dinh co luu du lieu xuong tap tin hay khong?
        public bool CoThayDoi
        {
            get;
            set;
        }

        // thuoc tinh tra ra so tien can thanh toan cho don muon phong nay
        public double TienMuonPhong
        {
            get
            {
                return (TietKTMuon - TietBDMuon + 1) * PhongMuon.DonGia;
            }
        }
        #endregion

        // Ham cau tu
        public DonMuonPhong(string ma, string tenNguoiMuon, string dvCongTac, Phong phongMuon,
                            NgayThang ngayMuon, int tietBDMuon, int tietKTMuon, bool cothaydoi = true)
        {
            this.ma = ma;
            this.TenNguoiMuon = tenNguoiMuon;
            this.DVCongTac = dvCongTac;
            this.PhongMuon = phongMuon;
            this.NgayMuon = ngayMuon;
            this.TietBDMuon = tietBDMuon;
            this.TietKTMuon = tietKTMuon;
            this.CoThayDoi = cothaydoi;
        }

        #region Nap chong Object
        public override string ToString()
        {
            string temp = "";
            temp += string.Format("Ma: {0}\n", this.Ma);
            temp += string.Format("Nguoi muon: {0}\n", this.TenNguoiMuon);
            temp += string.Format("Dv cong tac: {0}\n", this.DVCongTac);
            temp += string.Format("Phong muon: {0}\n", this.PhongMuon.MaPhong);
            temp += string.Format("Ngay muon: {0}\n", this.NgayMuon);
            temp += string.Format("Tiet muon: {0}-{1}\n", this.TietBDMuon, this.TietKTMuon);
            return temp;
        }

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

        #region Nap chong IFormattable
        // Sau khi cai dat ham nay, co the su dung nhu dang ham ToString
        // nhung kem them ky tu de dinh dang cho phu hop
        // O day, "c" la dai dien cho Console - du lieu xuat ra de hien thi tren Console
        // "f" la dai dien cho File - du lieu xuat ra de luu file
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(format))
                format = "c";
            char first = format.ToLower()[0];
            if (format.Length == 1 && first == 'f')
            {// dinh dang theo de xuat du lieu ra file
                return string.Format("{0},{1},{2},{3},{4},{5},{6}",
                                     Ma, TenNguoiMuon, DVCongTac, PhongMuon.MaPhong, NgayMuon, TietBDMuon, TietKTMuon);
            }
            else if (first == 'c') // dinh dang de xuat ra console
                return ToString();
            throw new FormatException();
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return ToString(null, formatProvider);
        }
        #endregion

        #region Nap chong IComparable
        public int CompareTo(object o)
        {
            if (o is DonMuonPhong)
            {
                DonMuonPhong don = (DonMuonPhong)o;
                if (this == don) return 0;
                if (NgayMuon < don.NgayMuon // neu ngay thang nho hon
                    || (NgayMuon == don.NgayMuon // hoac neu ngay thang bang nhau
                    && TietBDMuon <= don.TietBDMuon)) // nhung tiet bat dau nho hon
                    return -1; // la nho hon
                return 1;
            }
            else throw new ArgumentException();
        }
        #endregion

        #region Nap chong toan tu
        // Nap chong toan tu ==
        public static bool operator ==(DonMuonPhong n1, DonMuonPhong n2)
        {
            return n1.Equals(n2);
        }

        // Nap chong toan tu !=
        public static bool operator !=(DonMuonPhong n1, DonMuonPhong n2)
        {
            return !n1.Equals(n2);
        }

        // Nap chong toan tu <
        public static bool operator <(DonMuonPhong n1, DonMuonPhong n2)
        {
            return (n1.CompareTo(n2) < 0);
        }

        // Nap chong toan tu >
        public static bool operator >(DonMuonPhong n1, DonMuonPhong n2)
        {
            return (n1.CompareTo(n2) > 0);
        }

        // Nap chong toan tu <=
        public static bool operator <=(DonMuonPhong n1, DonMuonPhong n2)
        {
            return (n1.CompareTo(n2) <= 0);
        }

        // Nap chong toan tu >=
        public static bool operator >=(DonMuonPhong n1, DonMuonPhong n2)
        {
            return (n1.CompareTo(n2) >= 0);
        }
        #endregion
    }
}
