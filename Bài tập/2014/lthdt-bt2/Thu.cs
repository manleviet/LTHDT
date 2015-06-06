using System;

namespace QLTC_HDT
{
    class Thu : GiaoDich // thua ke lop GiaoDich
    {
        // Nap chong thuoc tinh SoLuong
        public override double SoLuong
        {
            get
            {
                return base.soluong; // gia tri duong
            }
            set
            {
                base.soluong = Math.Abs(value);
            }
        }

        // CAU TU
        public Thu(int ma, ThoiGian ngaygd, double soluong, TaiKhoan thuocTaiKhoan, LoaiGiaoDich loaiGD, string ghichu = "")
            : base(ma, ngaygd, soluong, thuocTaiKhoan, loaiGD, ghichu)
        {
        }

        // Nap chong ham ToString
        public override string ToString()
        {
            string st = string.Format("\nMa giao dich : {0}", Ma);
            st += string.Format("\nNgay {0} thu vao {1} cho tai khoan {2}", NgayGD, SoLuong, ThuocTaiKhoan.Ten);
            st += string.Format("\nLoai giao dich : {0}", LayTenLoaiGiaoDich(LoaiGD));
            st += string.Format("\nGhi chu : {0}", GhiChu);

            return st;
        }

        // Nap chong ham LayDuLieuGhiFile
        public override string LayDuLieuGhiFile()
        {
            return string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}",
                                  (int)GiaoDich.KieuGiaoDich.Thu,
                                  Ma,
                                  NgayGD,
                                  SoLuong,
                                  LoaiGD,
                                  GhiChu,
                                  ThuocTaiKhoan.Ten,
                                  "");
        }
    }
}
