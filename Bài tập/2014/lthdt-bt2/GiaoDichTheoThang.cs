using System;
using System.Collections;
using System.Collections.Generic;

namespace QLTC_HDT
{
    // Day la lop quan ly cac giao dich cung mot thang
    class GiaoDichTheoThang : IComparable, IEnumerable
    {
        // Hai bien thanh phan quan trong
        // 1. thang nam cua cac giao dich
        ThoiGian thangnam;
        public ThoiGian ThangNam
        {
            get
            {
                return this.thangnam;
            }
            set
            {
                if (value != null)
                    this.thangnam = value;
            }
        }
        // 2. danh sach cac giao dich
        List<GiaoDich> listGD;

        // CAC THUOC TINH
        // thuoc tinh kiem tra rong
        public bool LaRong
        {
            get
            {
                return listGD.Count == 0;
            }
        }
        // thuoc tinh tra ra so luong giao dich trong danh sach
        public int SoLuong
        {
            get
            {
                return listGD.Count;
            }
        }
        // thuoc tinh chi muc
        public GiaoDich this[int index]
        {
            get
            {
                if (index < 0 || index >= listGD.Count)
                    return null;
                return listGD[index];
            }
            set
            {
                if (index >= 0 && index < listGD.Count)
                    listGD[index] = value;
            }
        }
        // thuoc tinh lay ma giao dich lon nhat
        // dung khi tao giao dich moi voi ma tu dong tang len 1
        public int MaxMaGD
        {
            get
            {
                int max = -1;
                foreach (GiaoDich gd in listGD)
                    if (max < gd.Ma) max = gd.Ma;
                return max;
            }
        }

        // CAU TU
        public GiaoDichTheoThang(GiaoDich gd)
        {
            // Mot doi tuong moi cua Giao dich theo thang duoc tao ra
            // khi co mot giao dich moi ma giao dich do co thang khong trung voi
            // cac giao dich theo thang da co
            ThoiGian ngay = gd.NgayGD;
            this.thangnam = new ThoiGian(1, ngay.Thang, ngay.Nam);
            listGD = new List<GiaoDich>();
            listGD.Add(gd); // add giao dich moi do vao
        }

        ~GiaoDichTheoThang()
        {
            listGD = null;
        }

        // Them giao dich vao danh sach
        public void ThemGD(GiaoDich gd)
        {
            listGD.Add(gd); // them vao cuoi danh sach
            listGD.Sort(); // sap xep lai danh sach theo ngay giao dich
        }

        // Ham xoa giao dich khoi danh sach
        public void XoaGD(GiaoDich gd)
        {
            listGD.Remove(gd);
        }

        // Ham xoa cac giao dich cua tai khoan
        public void XoaGiaoDichCuaTK(TaiKhoan tk)
        {
            // dung vong lap foreach xoa tat ca cac giao dich phat sinh cua tk nay
            foreach (GiaoDich gd in listGD)
            {
                if (gd.ThuocTaiKhoan == tk ||
                    (gd is ChuyenKhoan && ((ChuyenKhoan)gd).TaiKhoanNhan == tk))
                    listGD.Remove(gd);
            }
        }

        // Ham kiem tra co giao dich nao do trong danh sach khong
        public bool CoGD(GiaoDich gd)
        {
            return listGD.Contains(gd);
        }

        // Ham tra ra vi tri cua Giao dich Mo tai khoan cua Tai khoan dau vao
        public int LayViTriGDMoTK(TaiKhoan tk)
        {
            GiaoDich gd;
            for (int i = 0; i < listGD.Count; i++)
            {
                gd = listGD[i];
                if (gd.LoaiGD == GiaoDich.LoaiGiaoDich.MoTK &&
                    gd.ThuocTaiKhoan == tk)
                    return i;
            }
            return -1;
        }

        // Ham tra ra giao dich co ma nao do 
        public GiaoDich LayGDVoiMa(int ma)
        {
            foreach (GiaoDich gd in listGD)
                if (gd.Ma == ma)
                    return gd;
            return null;
        }

        // Ham tra ra danh sach cac giao dich duoc thuc hien trong ngay nao do
        public List<GiaoDich> LayGDCuaNgay(ThoiGian ngay)
        {
            List<GiaoDich> list = new List<GiaoDich>();

            foreach (GiaoDich gd in listGD)
                if (gd.NgayGD == ngay)
                    list.Add(gd);

            if (list.Count == 0) return null;
            return list;
        }

        // Ham tinh so du cua tai khoan
        public double LaySoDuCuaTK(TaiKhoan tk)
        {
            double sodu = 0;
            foreach (GiaoDich gd in listGD) // duyet qua danh sach
            {
                if (gd.ThuocTaiKhoan == tk)
                    sodu += gd.SoLuong;
                if (gd is ChuyenKhoan && ((ChuyenKhoan)gd).TaiKhoanNhan == tk)
                    sodu += ((ChuyenKhoan)gd).SoLuongNhan;
            }
            return sodu;
        }

        // Ham tinh tong thu cua tai khoan
        public double LayTongThu(TaiKhoan tk)
        {
            double tongthu = 0;
            foreach (GiaoDich gd in listGD)
            {
                if (gd is Thu && gd.ThuocTaiKhoan == tk && gd.LoaiGD != GiaoDich.LoaiGiaoDich.MoTK)
                    tongthu += gd.SoLuong;
                if (gd is ChuyenKhoan && ((ChuyenKhoan)gd).TaiKhoanNhan == tk)
                    tongthu += ((ChuyenKhoan)gd).SoLuongNhan;
            }
            return tongthu;
        }

        // Ham tinh tong chi cua tai khoan
        public double LayTongChi(TaiKhoan tk)
        {
            double tongchi = 0;
            foreach (GiaoDich gd in listGD)
            {
                if (gd.ThuocTaiKhoan == tk && (gd is Chi || gd is ChuyenKhoan))
                    tongchi += gd.SoLuong;
            }
            return Math.Abs(tongchi);
        }

        // Cai dat giao dien IEnumerable
        public IEnumerator GetEnumerator()
        {
            return listGD.GetEnumerator();
        }

        // Cai dat giao dien IComparable
        public int CompareTo(object o)
        {
            GiaoDichTheoThang temp = o as GiaoDichTheoThang;
            if (temp != null)
            {
                // sap xep theo thoi gian giao dich
                return this.ThangNam.CompareTo(temp.ThangNam);
            }
            throw new ArgumentException("not a GiaoDichTheoThang");
        }
    }
}
