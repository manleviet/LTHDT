using System;

namespace QLPH
{
    class NgayThang : IComparable
    {
        #region Khai bao bien va thuoc tinh
        private int ngay;
        public int Ngay
        {
            get { return ngay; } // khong co set de khong cho phep gan vao thong qua thuoc tinh
        }

        private int thang;
        public int Thang
        {
            get { return thang; } // khong co set de khong cho phep gan vao thong qua thuoc tinh
        }

        private int nam;
        public int Nam
        {
            get { return nam; } // khong co set de khong cho phep gan vao thong qua thuoc tinh
        }
        #endregion

        #region Cau tu
        // Ham cau tu mac dinh
        public NgayThang()
        {
            ngay = 1;
            thang = 1;
            nam = 1;
        }

        // Ham cau tu voi 3 tham so ngay, thang, nam
        public NgayThang(int ngay, int thang, int nam)
        {
            // Neu tham so truyen vao khong phai la ngay hop le thi nem loi ra
            if (!LaNgayHopLe(ngay, thang, nam)) throw new ArgumentException("Tham so truyen vao khong phai la mot ngay.");
            this.ngay = ngay;
            this.thang = thang;
            this.nam = nam;
        }
        #endregion

        #region Cac ham public
        // Ham chuyen doi mot chuoi thanh kieu NgayThang
        // Neu tach duoc thi tra ra doi tuong kieu NgayThang
        // nguoc lai thi tra ra null
        public static NgayThang Parse(string chuoiNgay)
        {
            int ngay, thang, nam;
            if (TachGiaTriNgayThangNam(chuoiNgay, out ngay, out thang, out nam))
                return new NgayThang(ngay, thang, nam);
            return null;
        }

        // Ham chuyen doi mot chuoi thanh kieu NgayThang
        // Neu tach duoc thi tra ra true, va doi tuong kieu NgayThang o tham so ngayTachDuoc
        // nguoc lai thi tra ra false
        public static bool TryParse(string chuoiNgay, out NgayThang ngayTachDuoc)
        {
            int ngay, thang, nam;
            ngayTachDuoc = new NgayThang();
            if (TachGiaTriNgayThangNam(chuoiNgay, out ngay, out thang, out nam))
            {
                ngayTachDuoc.ngay = ngay;
                ngayTachDuoc.thang = thang;
                ngayTachDuoc.nam = nam;
                return true;
            }
            ngayTachDuoc = null;
            return false;
        }

        // Ham tra ra ngay cuoi cung cua thang
        public static int NgayCuoiCungCuaThang(NgayThang thangnam)
        {
            return NgayCuoiCungCuaThang(thangnam.thang, thangnam.nam);
        }

        // Ham tra ra so ngay chu nhat cua thang
        public static int SoNgayCN(NgayThang thangnam)
        {
            NgayThang ngayDauThang = new NgayThang(1, thangnam.Thang, thangnam.Nam);
            // tinh ngay tuyet doi cua ngay 01/thang/nam
            int ngayTDDauThang = NgayTuyetDoi(ngayDauThang);
            // xac dinh ngay cuoi cung cua thang la ngay nao (28, 29, 30 hay 31 ?)
            // tinh ngay tuyet doi cua ngay cuoi cung cua thang do
            NgayThang ngayCuoiThang = new NgayThang(NgayCuoiCungCuaThang(thangnam), thangnam.Thang, thangnam.Nam);
            int ngayTDCuoiThang = NgayTuyetDoi(ngayCuoiThang);

            // cong thuc ky dieu
            int soNgayCN = (ngayTDCuoiThang - ngayTDDauThang - (ngayTDCuoiThang % 7 + 1) + 8) / 7;
            return soNgayCN;
        }
        #endregion

        #region Nap chong ham lop Object
        // NAP CHONG CAC HAM CUA LOP OBJECT
        public override string ToString()
        {
            return string.Format("{0:d2}/{1:d2}/{2:d4}", ngay, thang, nam);
        }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            return o.ToString() == this.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        #endregion

        #region Cac ham tien ich
        // Ham tach ngay, thang, nam tu chuoi ngay thang nam
        private static bool TachGiaTriNgayThangNam(string chuoiNgay, out int ngay, out int thang, out int nam)
        {
            string[] items = chuoiNgay.Split('/'); // tach chuoi ra 3 thanh phan
            ngay = thang = nam = 1;
            // kiem tra :
            // 1. du 3 thanh phan : ngay, thang, nam
            // 2. ngay, thang, nam la cac so
            // 3. ngay thang nam do hop le
            if (items.Length != 3 ||
                !int.TryParse(items[0], out ngay) ||
                !int.TryParse(items[1], out thang) ||
                !int.TryParse(items[2], out nam) ||
                !LaNgayHopLe(ngay, thang, nam))
            { // neu ngay sai hay khong tach dung thi
                return false;
            }
            return true;
        }

        // Ham kiem tra xem ngay thang nam co hop le khong
        // tra ra true neu hop le
        private static bool LaNgayHopLe(int ngay, int thang, int nam)
        {
            if (nam > 0 && thang > 0 && thang < 13 &&
                ngay > 0 && ngay <= NgayCuoiCungCuaThang(thang, nam))
                return true;
            return false;
        }

        // Tra ve so ngay cua mot thang cho truoc
        private static int NgayCuoiCungCuaThang(int thang, int nam)
        {
            switch (thang)
            {
                case 2:
                    if (LaNamNhuan(nam))
                        return 29;
                    else
                        return 28;
                case 4:
                case 6:
                case 9:
                case 11: return 30;
                default: return 31;
            }
        }

        // Tra ve true neu nam la nam nhuan
        private static bool LaNamNhuan(int nam)
        {
            return (((nam % 4) == 0) && ((nam % 100) != 0))
                    || ((nam % 400) == 0);
        }

        // Tinh so ngay trong nam cho den
        // thang/ngay/nam nhap vao
        private static int SoNgayTuDauNam(NgayThang ngay)
        {
            // gia su cac thang deu 31 ngay
            int songay = (ngay.Thang - 1) * 31 + ngay.Ngay;

            // hieu chinh cac thang sau thang hai
            if (ngay.Thang > 2)
            {
                songay = songay - ((4 * ngay.Thang + 23) / 10);
                if (LaNamNhuan(ngay.Nam))
                    songay = songay + 1;
            }

            return songay;
        }

        // Ham tinh ngay tuyet doi
        private static int NgayTuyetDoi(NgayThang ngay)
        {
            return SoNgayTuDauNam(ngay) // so ngay trong nam
                    + 365 * (ngay.Nam - 1)          // so ngay cua cac nam truoc do
                    + (ngay.Nam - 1) / 4   // nam nhuan Julian
                    - (ngay.Nam - 1) / 100 // nam nhuan chan tram
                    + (ngay.Nam - 1) / 400; // nam nhuan Gregorian
        }
        #endregion

        #region IComparable
        // CAI DAT GIAO DIEN ICOMPARABLE
        // de dung voi ham Sort cua cac lop Collection
        // va dung trong viec nap chong toan tu so sanh
        public int CompareTo(object o)
        {
            if (o is NgayThang)
            {
                NgayThang n = (NgayThang)o;
                if (this == n) return 0;
                if (this.Nam > n.Nam ||
                    (this.Nam == n.Nam && this.Thang > n.Thang) ||
                    (this.Nam == n.Nam && this.Thang == n.Thang && this.Ngay > n.Ngay))
                    return 1;
                return -1;
            }
            else throw new ArgumentException();
        }
        #endregion

        #region Nap chong toan tu
        // Nap chong toan tu ==
        public static bool operator ==(NgayThang n1, NgayThang n2)
        {
            return n1.Equals(n2);
        }

        // Nap chong toan tu !=
        public static bool operator !=(NgayThang n1, NgayThang n2)
        {
            return !n1.Equals(n2);
        }

        // Nap chong toan tu <
        public static bool operator <(NgayThang n1, NgayThang n2)
        {
            return (n1.CompareTo(n2) < 0);
        }

        // Nap chong toan tu >
        public static bool operator >(NgayThang n1, NgayThang n2)
        {
            return (n1.CompareTo(n2) > 0);
        }

        // Nap chong toan tu <=
        public static bool operator <=(NgayThang n1, NgayThang n2)
        {
            return (n1.CompareTo(n2) <= 0);
        }

        // Nap chong toan tu >=
        public static bool operator >=(NgayThang n1, NgayThang n2)
        {
            return (n1.CompareTo(n2) >= 0);
        }
        #endregion
    }
}
