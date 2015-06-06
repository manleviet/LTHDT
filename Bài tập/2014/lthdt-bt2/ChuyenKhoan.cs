using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLTC_HDT
{
    class ChuyenKhoan : GiaoDich
    {
        // Nap chong thuoc tinh SoLuong
        public override double SoLuong
        {
            get
            {
                return (-1) * soluong; // am voi tai khoan chuyen
            }
            set
            {
                soluong = Math.Abs(value);
            }
        }

        public double SoLuongNhan
        {
            get
            {
                return soluong; // duong voi tai khoan nhan
            }
        }

        // Giao dich dang chuyen khoan thi co them Tai khoan Nhan
        TaiKhoan taiKhoanNhan;
        public TaiKhoan TaiKhoanNhan
        {
            get
            {
                return taiKhoanNhan;
            }
            set
            {
                if (value != null)
                    taiKhoanNhan = value;
            }
        }

        // CAU TU
        public ChuyenKhoan(int ma, ThoiGian ngaygd, double soluong, TaiKhoan thuocTaiKhoan, TaiKhoan taikhoanNhan, LoaiGiaoDich loaiGD, string ghichu = "")
            : base(ma, ngaygd, soluong, thuocTaiKhoan, loaiGD, ghichu)
        {
            TaiKhoanNhan = taikhoanNhan;
        }

        // Nap chong ham ToString
        public override string ToString()
        {
            string st = string.Format("\nMa giao dich : {0}", Ma);
            st += string.Format("\nNgay {0} ", NgayGD);
            st += string.Format("chuyen khoan tu {0} den {1}", ThuocTaiKhoan.Ten, TaiKhoanNhan.Ten);
            st += string.Format("\nSo luong : {0}", SoLuong);
            st += string.Format("\nLoai giao dich : {0}", LayTenLoaiGiaoDich(LoaiGD));
            st += string.Format("\nGhi chu : {0}", GhiChu);

            return st;
        }

        // Nap chong ham LayDuLieuGhiFile
        public override string LayDuLieuGhiFile()
        {
            return string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}",
                                  (int)GiaoDich.KieuGiaoDich.ChuyenKhoan,
                                  Ma,
                                  NgayGD,
                                  soluong,
                                  LoaiGD,
                                  GhiChu,
                                  ThuocTaiKhoan.Ten,
                                  TaiKhoanNhan.Ten);
        }
    }
}
