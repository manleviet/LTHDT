using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLTC_HDT
{
    class Chi : GiaoDich // thua ke tu GiaoDich
    {
        // Nap chong thuoc tinh SoLuong
        public override double SoLuong
        {
            get
            {
                return (-1) * soluong; // tra ra gia tri am
            }
            set
            {
                soluong = Math.Abs(value);
            }
        }

        // CAU TU
        public Chi(int ma, ThoiGian ngaygd, double soluong, TaiKhoan thuocTaiKhoan, LoaiGiaoDich loaiGD, string ghichu = "")
            : base(ma, ngaygd, soluong, thuocTaiKhoan, loaiGD, ghichu)
        {
        }

        // Nap chong ham ToString
        public override string ToString()
        {
            string st = string.Format("\nMa giao dich : {0}", Ma);
            st += string.Format("\nNgay {0} chi ra {1} tu tai khoan {2}", NgayGD, SoLuong, ThuocTaiKhoan.Ten);
            st += string.Format("\nLoai giao dich : {0}", LayTenLoaiGiaoDich(LoaiGD));
            st += string.Format("\nGhi chu : {0}", GhiChu);

            return st;
        }

        // Nap chong ham LayDuLieuGhiFile
        public override string LayDuLieuGhiFile()
        {
            return string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}",
                                  (int)GiaoDich.KieuGiaoDich.Chi,
                                  Ma,
                                  NgayGD,
                                  soluong,
                                  LoaiGD,
                                  GhiChu,
                                  ThuocTaiKhoan.Ten,
                                  "");
        }
    }
}
