using System;
using System.Collections;

namespace QLTC_HDT
{
    class ThoiGian : IComparable
    {
        const string NGAY_MAC_DINH = "01/01/0001"; // khi ngay nhap vao sai thi se nhan gia tri nay

        // BIEN THANH PHAN VA THUOC TINH
        int ngay;
        public int Ngay
        {
            get
            {
                return ngay;
            }
        }
        int thang;
        public int Thang
        {
            get
            {
                return thang;
            }
        }
        int nam;
        public int Nam
        {
            get
            {
                return nam;
            }
        }

        // thuoc tinh nay de lay ra chuoi theo dinh dang dd/mm/yyyy
        public string NgayThangNam
        {
            get { return this.ToString(); }
            set
            {
                string temp = value;
                // kiem tra nhung truong hop value sai dinh dang
                // thi ngay thang se duoc gan bang ngay mac dinh
                if (value == null)
                    temp = NGAY_MAC_DINH;

                string[] items = temp.Split('/'); // tach chuoi ra 3 thanh phan
                int ngay, thang, nam;
                // kiem tra :
                // 1. du 3 thanh phan : ngay, thang, nam
                // 2. ngay, thang, nam la cac so
                // 3. ngay thang nam do hop le
                while (items.Length != 3 ||
                       !int.TryParse(items[0], out ngay) ||
                       !int.TryParse(items[1], out thang) ||
                       !int.TryParse(items[2], out nam) ||
                       !LaNgayHopLe(ngay, thang, nam))
                { // neu ngay sai thi gan bang ngay mac dinh
                    temp = NGAY_MAC_DINH;
                    items = temp.Split('/');
                }

                this.ngay = ngay;
                this.thang = thang;
                this.nam = nam;
            }
        }

        // thuoc tinh de kiem tra xem gia tri co nhan gia tri ngay mac dinh khong
        public bool LaNgayMacDinh
        {
            get
            {
                if (NgayThangNam == NGAY_MAC_DINH)
                    return true;
                return false;
            }
        }

        // CAU TU
        // nap chong cau tu mac dinh
        public ThoiGian()
        {
            this.NgayThangNam = NGAY_MAC_DINH;
        }

        public ThoiGian(int ngay, int thang, int nam)
        {
            if (!LaNgayHopLe(ngay, thang, nam)) // kiem tra ngay co hop le khong
                this.NgayThangNam = NGAY_MAC_DINH; // neu khong thi nhan ngay mac dinh
            // Gan truc tiep vao cac bien thanh phan (khong gan thong qua thuoc tinh)
            // vi cac gia tri ngay, thang, nam o day da dung roi
            this.ngay = ngay;
            this.thang = thang;
            this.nam = nam;
        }

        public ThoiGian(string ngay)
        {
            this.NgayThangNam = ngay; // gan thong qua thuoc tinh
            // trong thuoc tinh se dam bao du lieu gan vao hop le
        }

        // Ham so sanh ngaythang dau vao co trung thang nam hay khong
        public bool LaTrungThangNam(ThoiGian ngaythang)
        {
            if (this.thang == ngaythang.thang
                && this.nam == ngaythang.nam)
                return true;
            return false;
        }

        // NAP CHONG CAC HAM CUA LOP OBJECT
        public override string ToString()
        {
            return string.Format("{0:d2}/{1:d2}/{2:d4}", ngay, thang, nam);
        }

        public override bool Equals(object o)
        {
            return o.ToString() == this.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        // CAI DAT GIAO DIEN ICOMPARABLE
        // de dung voi ham Sort cua cac lop Collection
        // va dung trong viec nap chong toan tu so sanh
        public int CompareTo(object o)
        {
            if (o is ThoiGian)
            {
                ThoiGian n = (ThoiGian)o;
                if (this == n) return 0;
                if (this.Nam > n.Nam ||
                    (this.Nam == n.Nam && this.Thang > n.Thang) ||
                    (this.Nam == n.Nam && this.Thang == n.Thang && this.Ngay > n.Ngay))
                    return 1;
                return -1;
            }
            else throw new ArgumentException();
        }

        // NAP CHONG CAC TOAN TU
        // Nap chong phep toan cong
        public static ThoiGian operator +(ThoiGian n1, int t)
        {
            int ngay = n1.Ngay + t;
            int thang = n1.Thang;
            int nam = n1.Nam;
            int ngaycuoicung;
            while (ngay > (ngaycuoicung = NgayCuoiCungCuaThang(thang, nam)))
            {
                thang++;
                ngay -= ngaycuoicung;
                if (thang > 12)
                {
                    thang = 1;
                    nam++;
                }
            }
            ThoiGian ngayMoi = new ThoiGian(ngay, thang, nam);
            return ngayMoi;
        }

        // Nap chong phep toan tru
        public static ThoiGian operator -(ThoiGian n1, int t)
        {
            int ngay = n1.Ngay - t;
            int thang = n1.Thang;
            int nam = n1.Nam;
            while (ngay <= 0)
            {
                ngay += NgayCuoiCungCuaThang(thang - 1, nam);
                thang--;
                if (thang <= 0)
                {
                    thang = 12;
                    nam--;
                }
            }
            ThoiGian ngayMoi = new ThoiGian(ngay, thang, nam);
            return ngayMoi;
        }

        // Nap chong toan tu ++
        public static ThoiGian operator ++(ThoiGian n)
        {
            return n + 1;
        }

        // Nap chong toan tu --
        public static ThoiGian operator --(ThoiGian n)
        {
            return n - 1;
        }

        // Nap chong toan tu ==
        public static bool operator ==(ThoiGian n1, ThoiGian n2)
        {
            return n1.Equals(n2);
        }

        // Nap chong toan tu !=
        public static bool operator !=(ThoiGian n1, ThoiGian n2)
        {
            return !n1.Equals(n2);
        }

        // Nap chong toan tu <
        public static bool operator <(ThoiGian n1, ThoiGian n2)
        {
            return (n1.CompareTo(n2) < 0);
        }

        // Nap chong toan tu >
        public static bool operator >(ThoiGian n1, ThoiGian n2)
        {
            return (n1.CompareTo(n2) > 0);
        }

        // Nap chong toan tu <=
        public static bool operator <=(ThoiGian n1, ThoiGian n2)
        {
            return (n1.CompareTo(n2) <= 0);
        }

        // Nap chong toan tu >=
        public static bool operator >=(ThoiGian n1, ThoiGian n2)
        {
            return (n1.CompareTo(n2) >= 0);
        }

        // Nap chong toan tu chuyen doi kieu
        // Khi nap chong implicit thi khong can nap chong explicit nua
        public static implicit operator ThoiGian(string ngay)
        {
            ThoiGian ngayMoi = new ThoiGian(ngay);
            return ngayMoi;
        }

        // CAC HAM STATIC 
        // de cho cac lop khac va chuong trinh chinh cung su dung
        // ma khong can goi thong qua doi tuong
        public static int SoNgayTrongNam(ThoiGian t)
        {
            if (LaNamNhuan(t.nam))
                return 366;
            return 365;
        }

        public static int NgayCuoiCungCuaThang(ThoiGian t)
        {
            return NgayCuoiCungCuaThang(t.thang, t.nam);
        }

        // Ham kiem tra xem ngay thang nam co hop le khong
        // tra ra true neu hop le
        private bool LaNgayHopLe(int ngay, int thang, int nam)
        {
            if (nam > 0 && thang > 0 && thang < 13 &&
                ngay > 0 && ngay <= NgayCuoiCungCuaThang(thang, nam))
                return true;
            return false;
        }

        // Tra ve true neu nam la nam nhuan
        private static bool LaNamNhuan(int nam)
        {
            return (((nam % 4) == 0) && ((nam % 100) != 0))
                    || ((nam % 400) == 0);
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
    }
}
