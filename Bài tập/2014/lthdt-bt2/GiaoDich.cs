using System;
using System.Collections;

namespace QLTC_HDT
{
    // La lop truu tuong, khong cho phep tao doi tuong tu lop nay
    abstract class GiaoDich : IComparable
    {
        // Enum nay duoc dung de nhap du lieu cho khong phai de phan biet kieu giao dich
        public enum KieuGiaoDich { Thu, Chi, ChuyenKhoan }

        // BIEN THANH PHAN VA THUOC TINH
        // ma giao dich
        int ma;
        public int Ma // thuoc tinh chi doc, khong cho gan gia tri moi vao
        {
            get
            {
                return ma;
            }
        }

        ThoiGian ngaygd; // [Quan he thanh phan]
        public string NgayGD
        {
            get
            {
                return this.ngaygd.NgayThangNam;
            }
            set
            {
                this.ngaygd.NgayThangNam = value;
            }
        }

        public string GhiChu { get; set; } // thuoc tinh tu dong

        protected double soluong;
        // ham truu tuong, bat buoc phai nap chong
        public abstract double SoLuong { get; set; }

        // [Quan he ket hop voi tai khoan]
        TaiKhoan thuocTaiKhoan;
        public TaiKhoan ThuocTaiKhoan
        {
            get
            {
                return thuocTaiKhoan;
            }
            set
            {
                if (value != null)
                    thuocTaiKhoan = value;
            }
        }

        public enum LoaiGiaoDich
        {
            Khac, Xang, DangKyXe, DichVuDiLai, Luong, Thuong,
            TienMat, TuThien, QuanAo, TinDung, AnUong, GiaoDuc,
            GiaiTri, Qua, TapPham, SucKhoeTheThao, SuaNha, BaoHiem,
            TreCon, Vay, YTe, ThanhToanNo, ThuNuoi, ThueNha, TietKiem,
            Thue, DuLich, TruyenHinh, DienGas, DienThoai, Nuoc, Internet,
            VeTinh, MoTK
        }
        public LoaiGiaoDich LoaiGD { get; set; }
        // thuoc tinh tra ra so luong loai giao dich duoc ho tro
        public static int SoLoaiGiaoDich
        {
            get
            {
                return (int)LoaiGiaoDich.MoTK + 1;
            }
        }

        // CAU TU
        public GiaoDich(int ma, ThoiGian ngaygd, double soluong, TaiKhoan thuocTaiKhoan, LoaiGiaoDich loaiGD, string ghichu = "")
        {
            this.ma = ma;
            this.ngaygd = ngaygd;
            this.soluong = soluong;
            this.ThuocTaiKhoan = thuocTaiKhoan;
            this.LoaiGD = loaiGD;
            this.GhiChu = ghichu;
        }

        ~GiaoDich()
        {
            // [Quan he thanh phan] phai xoa doi tuong
            ngaygd = null;
        }

        // ham truu tuong, bat buoc phai nap chong
        public abstract string LayDuLieuGhiFile();

        // Cai dat giao dien IComparable
        // de dung trong ham Sort cua cac Colection
        // vi du: khi them mot giao dich thi sap xep danh sach giao dich theo ngay giao dich
        public int CompareTo(object o)
        {
            GiaoDich temp = o as GiaoDich;
            if (temp != null)
            {
                // sap xep theo ngay giao dich
                return this.NgayGD.CompareTo(temp.NgayGD);
            }
            throw new ArgumentException("not a GiaoDich");
        }

        // CAC THANH PHAN STATIC
        // thuoc tinh tra ra chuoi cac kieu giao dich
        public static string ChuoiKieuGiaoDich
        {
            get
            {
                return "Thu, Chi, ChuyenKhoan";
            }
        }

        // thuoc tinh tra ra chuoi cac loai giao dich
        public static string ChuoiLoaiGiaoDich
        {
            get
            {
                return "Khac, Xang, DangKyXe, DichVuDiLai, Luong, Thuong, TienMat, TuThien, QuanAo, TinDung, AnUong, GiaoDuc, GiaiTri, Qua, TapPham, SucKhoeTheThao, SuaNha, BaoHiem, TreCon, Vay, YTe, ThanhToanNo, ThuNuoi, ThueNha, TietKiem, Thue, DuLich, TruyenHinh, DienGas, DienThoai, Nuoc, Internet, VeTinh";
            }
        }

        // ham trả ra ten cua loai giao dich
        public static string LayTenLoaiGiaoDich(LoaiGiaoDich loaiGD)
        {
            string st = "Khac";
            switch (loaiGD)
            {
                case LoaiGiaoDich.Xang: st = "Xang"; break;
                case LoaiGiaoDich.DangKyXe: st = "Dang ky xe"; break;
                case LoaiGiaoDich.DichVuDiLai: st = "Dich vu di lai"; break;
                case LoaiGiaoDich.Luong: st = "Luong"; break;
                case LoaiGiaoDich.Thuong: st = "Thuong"; break;
                case LoaiGiaoDich.TienMat: st = "Tien mat"; break;
                case LoaiGiaoDich.TuThien: st = "Tu thien"; break;
                case LoaiGiaoDich.QuanAo: st = "Quan ao"; break;
                case LoaiGiaoDich.TinDung: st = "Tin dung"; break;
                case LoaiGiaoDich.AnUong: st = "An uong"; break;
                case LoaiGiaoDich.GiaoDuc: st = "Giao duc"; break;
                case LoaiGiaoDich.GiaiTri: st = "Giai tri"; break;
                case LoaiGiaoDich.Qua: st = "Qua"; break;
                case LoaiGiaoDich.TapPham: st = "Tap pham"; break;
                case LoaiGiaoDich.SucKhoeTheThao: st = "Suc khoe & the thao"; break;
                case LoaiGiaoDich.SuaNha: st = "Sua chua nha cua"; break;
                case LoaiGiaoDich.BaoHiem: st = "Bao hiem"; break;
                case LoaiGiaoDich.TreCon: st = "Tre con"; break;
                case LoaiGiaoDich.Vay: st = "Vay"; break;
                case LoaiGiaoDich.YTe: st = "Y te"; break;
                case LoaiGiaoDich.ThanhToanNo: st = "Thanh toan no"; break;
                case LoaiGiaoDich.ThuNuoi: st = "Thu nuoi"; break;
                case LoaiGiaoDich.ThueNha: st = "Thue nha"; break;
                case LoaiGiaoDich.TietKiem: st = "Tiet kiem"; break;
                case LoaiGiaoDich.Thue: st = "Thue"; break;
                case LoaiGiaoDich.DuLich: st = "Du lich"; break;
                case LoaiGiaoDich.TruyenHinh: st = "Truyen hinh"; break;
                case LoaiGiaoDich.DienGas: st = "Dien & Gas"; break;
                case LoaiGiaoDich.DienThoai: st = "Dien thoai"; break;
                case LoaiGiaoDich.Nuoc: st = "Nuoc"; break;
                case LoaiGiaoDich.Internet: st = "Internet"; break;
                case LoaiGiaoDich.VeTinh: st = "Ve tinh"; break;
                case LoaiGiaoDich.MoTK: st = "Mo tai khoan"; break;
                default: st = "Khac"; break;
            }
            return st;
        }
    }
}
