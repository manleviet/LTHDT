using System;

namespace QLTC_HDT
{
    class TaiKhoan
    {
        // Quan he ket hop, mot chieu tu DSGiaoDich qua TaiKhoan
        DSGiaoDich listGD;

        // BIEN THANH PHAN VA THUOC TINH
        string ten;
        public string Ten
        {
            get { return ten; }
            set
            {
                if (value == null || value.Length == 0)
                    ten = "Ten Tai Khoan";
                else
                    ten = value;
            }
        }

        // so tien ban dau khi tao tai khoan
        // dung thuoc tinh tu dong vi khong can kiem tra
        public double SoTienBanDau
        {
            get;
            set;
        }

        // [Quan he thanh phan] chi cho phep thay doi du lieu, khong gan doi tuong moi
        ThoiGian ngaymo;
        public string NgayMo
        {
            get
            {
                return ngaymo.NgayThangNam;
            }
            set
            {
                ngaymo.NgayThangNam = value;
            }
        }

        public enum KieuTaiKhoan { TienMat, Sec, TinDung, GhiNo, DauTu, Vay, TietKiem }
        public KieuTaiKhoan KieuTK { get; set; } // thuoc tinh tu dong

        // CAU TU
        // Cau tu mac dinh
        public TaiKhoan(DSGiaoDich listGD)
        {
            this.Ten = ""; // gan bang ten mac dinh
            this.SoTienBanDau = 0;
            // [Quan he thanh phan]
            this.ngaymo = new ThoiGian(); // tao doi tuong ben trong lop
            this.KieuTK = KieuTaiKhoan.TienMat;

            //[Quan he ket hop voi DSGiaoDich]
            // doi tuong duoc tao ra ben ngoai lop
            // khi gan doi tuong moi thi khong xoa doi tuong cu
            this.listGD = listGD;
        }

        public TaiKhoan(string ten, double sotienbandau, ThoiGian ngaymo, KieuTaiKhoan kieuTK, DSGiaoDich listGD)
        {
            this.Ten = ten;
            this.SoTienBanDau = sotienbandau;
            // [Quan he thanh phan]
            this.ngaymo = ngaymo; // tao doi tuong ben trong lop
            this.KieuTK = kieuTK;

            //[Quan he ket hop voi DSGiaoDich]
            // doi tuong duoc tao ra ben ngoai lop
            // khi gan doi tuong moi thi khong xoa doi tuong cu
            this.listGD = listGD;
        }
        
        // Huy tu
        ~TaiKhoan()
        {
            // [Quan he thanh phan] xoa doi tuong trong ham huy tu
            this.ngaymo = null;
            // [Quan he ket hop voi DSGiaoDich] khong xoa doi tuong
        }

        // Nap chong ham ToString cua lop Object
        public override string ToString()
        {
            string st = string.Format("\nTai khoan : {0}", this.Ten);
            st += string.Format("\nKieu tai khoan : {0}", LayTenKieuTaiKhoan(KieuTK));
            st += string.Format("\nNgay mo : {0}", this.NgayMo);
            st += string.Format("\nSo du : {0}", listGD.LaySoDuCuaTK(this));

            return st;
        }
        
        // Tao ra chuoi du lieu cua tai khoan de luu xuong file
        public string LayDuLieuGhiFile()
        {
            return string.Format("{0}-{1}-{2}-{3}", Ten, KieuTK, NgayMo, SoTienBanDau);
        }

        // CAC THANH PHAN STATIC
        // Ham tra ra ten kieu tai khoan
        public static string LayTenKieuTaiKhoan(KieuTaiKhoan kieuTK)
        {
            string st = "Tien mat";
            switch (kieuTK)
            {
                case KieuTaiKhoan.TienMat: st = "Tien mat"; break;
                case KieuTaiKhoan.Sec: st = "Sec"; break;
                case KieuTaiKhoan.TinDung: st = "Tin dung"; break;
                case KieuTaiKhoan.GhiNo: st = "Ghi no"; break;
                case KieuTaiKhoan.DauTu: st = "Dau tu"; break;
                case KieuTaiKhoan.Vay: st = "Vay"; break;
                case KieuTaiKhoan.TietKiem: st = "Tiet kiem"; break;
            }
            return st;
        }

        // Thuoc tinh tra ra mot chuoi chua ten cac kieu tai khoan
        public static string ChuoiKieuTaiKhoan
        {
            get
            {
                return "TienMat, Sec, TinDung, GhiNo, DauTu, Vay, TietKiem";
            }
        }
    }
}
