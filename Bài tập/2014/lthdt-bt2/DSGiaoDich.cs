using System;
using System.Collections;
using System.Collections.Generic;

namespace QLTC_HDT
{
    class DSGiaoDich : IEnumerable
    {
        // [Quan hệ]Quan hệ thu nap với lớp GiaoDichTheoThang
        List<GiaoDichTheoThang> listThang;

        // THUOC TINH
        // kiem tra rong
        public bool LaRong
        {
            get
            {
                bool kq = true;
                foreach (GiaoDichTheoThang gdThang in listThang)
                    kq &= gdThang.LaRong;
                return kq;
            }
        }
        // lay so luong giao dich trong danh sach giao dich
        // khong phai la lay so luong GiaoDichTheoThang
        public int SoLuong
        {
            get
            {
                int soluong = 0;
                foreach (GiaoDichTheoThang gd in listThang)
                    soluong += gd.SoLuong;
                return soluong;
            }
        }

        // Thuộc tính chỉ mục
        // lay giao dich theo chi muc
        public GiaoDich this[int index]
        {
            get
            {
                if (index < 0 || index >= this.SoLuong)
                    return null;
                int vitriGDThang = LayViTriGDThangNam(ref index);
                return listThang[vitriGDThang][index];
            }
            set
            {
                if (index >= 0 && index < this.SoLuong)
                {
                    int vitriGDThang = LayViTriGDThangNam(ref index);
                    listThang[vitriGDThang][index] = value;
                }
            }
        }

        // Su dung cho viec tu sinh ma giao dich
        public int MaChoGiaoDichMoi
        {
            get
            {
                int max = -1;
                foreach (GiaoDichTheoThang gd in listThang)
                {
                    int maxMa = gd.MaxMaGD;
                    if (maxMa > max) max = maxMa;
                }
                return max + 1;
            }
        } 

        // CAU TU
        public DSGiaoDich()
        {
            listThang = new List<GiaoDichTheoThang>();
        }

        ~DSGiaoDich()
        {
            listThang = null;
        }

        // THANH PHAN PUBLIC
        // Them giao dich Mo Tai khoan
        // duoc dung de tao giao dich theo thang
        public void ThemTK(TaiKhoan tk)
        {
            // tao giao dich moi voi loaiGD la MoTK
            GiaoDich gd = new Thu(MaChoGiaoDichMoi, tk.NgayMo, tk.SoTienBanDau, tk, GiaoDich.LoaiGiaoDich.MoTK);

            int index;
            GiaoDichTheoThang gdThang;
            // xem thu giao dich do thuoc thang nam nao
            if ((index = LayViTriGDThangNam(tk.NgayMo)) != -1)
            {   // neu co thang nam do trong listThang roi thi
                listThang[index].ThemGD(gd); // va add giao dich do vao gdThang
            }
            else
            { // neu chua co
                gdThang = new GiaoDichTheoThang(gd); // tao gdThang moi
                listThang.Add(gdThang); // add gdThang voi listThang
                listThang.Sort(); // sap xep danh sach theo ngay
            }
        }

        // Sua thong tin giao dich Mo Tai khoan
        public void SuaTTTK(TaiKhoan tkCu, TaiKhoan tkMoi)
        {
            int index;
            if ((index = LayViTriGDThangNam(tkCu.NgayMo)) == -1)
            {
                Console.WriteLine("LOI trong qua trinh sua thong tin TaiKhoan khoan");
                Console.ReadLine();
            }
            else
            {
                GiaoDichTheoThang gdThang = listThang[index];
                index = gdThang.LayViTriGDMoTK(tkCu);
                GiaoDich gd = gdThang[index]; // lay ra giao dich Mo tai khoan

                gdThang.XoaGD(gd); // Xoa giao dich Mo tai khoan cu

                ThemTK(tkMoi); // Them cai moi
            }
        }

        // Xoa cac giao dich cua Tai khoan nao do
        public void XoaGiaoDichCua(TaiKhoan tk)
        {
            foreach (GiaoDichTheoThang gdThang in listThang)
            {
                gdThang.XoaGiaoDichCuaTK(tk);
            }
        }

        // Them giao dich moi
        public void ThemGD(GiaoDich gd)
        {
            int index;
            GiaoDichTheoThang gdThang;
            // xem thu giao dich do thuoc thang nam nao
            if ((index = LayViTriGDThangNam(gd.NgayGD)) != -1)
            {// neu co thang nam do trong listThang roi thi them giao dich vao
                listThang[index].ThemGD(gd);
            }
            else // neu chua thi tao GiaoDichTheoThang moi
            {
                gdThang = new GiaoDichTheoThang(gd);
                listThang.Add(gdThang);
                listThang.Sort();
            }
        }

        // Xoa giao dich
        public void XoaGD(GiaoDich gd)
        {
            foreach (GiaoDichTheoThang gdThang in listThang)
                if (gdThang.CoGD(gd)) gdThang.XoaGD(gd);
        }

        // Ham tra ra Giao dich voi ma nao do
        public GiaoDich LayGDVoiMa(int ma)
        {
            GiaoDich gd = null;
            foreach (GiaoDichTheoThang gdThang in listThang)
            {
                gd = gdThang.LayGDVoiMa(ma);
                if (gd != null)
                    break;
            }
            return gd;
        }

        // Ham tra ra danh sach giao dich cua tai khoan nao do 
        public List<GiaoDich> LayCacGDCuaTK(TaiKhoan tk)
        {
            List<GiaoDich> list = new List<GiaoDich>();

            foreach (GiaoDichTheoThang gdThang in listThang)
                foreach (GiaoDich gd in gdThang)
                    if (gd.ThuocTaiKhoan == tk ||
                        (gd is ChuyenKhoan && ((ChuyenKhoan)gd).TaiKhoanNhan == tk))
                        list.Add(gd);

            return list;
        }

        // Ham tinh so du cua tai khoan nao do
        public double LaySoDuCuaTK(TaiKhoan tk)
        {
            double sodu = 0;
            foreach (GiaoDichTheoThang gd in listThang)
                sodu += gd.LaySoDuCuaTK(tk);
            return sodu;
        }

        // Ham tinh tong thu cua tai khoan nao do
        public double LayTongThu(TaiKhoan tk)
        {
            double tongthu = 0;
            foreach (GiaoDichTheoThang gd in listThang)
                tongthu += gd.LayTongThu(tk);
            return tongthu;
        }

        // Ham tinh tong chi cua tai khoan nao do
        public double LayTongChi(TaiKhoan tk)
        {
            double tongchi = 0;
            foreach (GiaoDichTheoThang gd in listThang)
                tongchi += gd.LayTongChi(tk);
            return tongchi;
        }

        // Ham tinh thong ke
        // dung cho thong ke theo thoi gian
        public List<GiaoDich> ThongKeTheoThoiGian(ThoiGian ngayDauKy, ThoiGian ngayCuoiKy,
                                                  out double sodudauky,
                                                  out double tongthu,
                                                  out double tongchi,
                                                  out double soducuoiky)
        {
            List<GiaoDich> list = new List<GiaoDich>();

            List<GiaoDich> temp;
            // duyet qua tuan tu cac ngay
            for (ThoiGian demNgay = ngayDauKy; demNgay <= ngayCuoiKy; demNgay++)
            {
                temp = LayGDCuaNgay(demNgay); // lay danh sach cac giao dich trong ngay do
                if (temp != null)
                    list.AddRange(temp); // chen vao danh sach chung
            }

            // tinh so du dau ky
            sodudauky = LaySoDuTruocNgay(ngayDauKy);

            // tinh tong thu
            tongthu = TinhTongThu(list);

            // tinh tong chi
            tongchi = TinhTongChi(list);

            // so du cuoi ky
            soducuoiky = sodudauky + tongthu + tongchi;

            return list;
        }

        // Ham tinh thong ke
        // dung cho cac loai thong ke theo phan loai va thoi gian
        // mang tongthus va tongchis la hai mang tong thu va tong chi theo tung loai giao dich
        public void ThongKeTheoThoiGian(ThoiGian ngayDauKy, ThoiGian ngayCuoiKy,
                                        out bool coGD,
                                        out double tongthu,
                                        out double tongchi,
                                        out double[] tongthus,
                                        out double[] tongchis,
                                        out int size)
        {
            List<GiaoDich> list = new List<GiaoDich>();

            List<GiaoDich> temp;
            for (ThoiGian demNgay = ngayDauKy; demNgay <= ngayCuoiKy; demNgay++)
            {
                temp = LayGDCuaNgay(demNgay);
                if (temp != null)
                    list.AddRange(temp);
            }

            coGD = false;
            tongthu = 0;
            tongchi = 0;
            tongthus = null;
            tongchis = null;
            size = 0;
            if (list.Count != 0)
            {
                coGD = true;
                // tinh tong thu
                tongthu = TinhTongThu(list);

                // tinh tong chi
                tongchi = TinhTongChi(list);

                size = GiaoDich.SoLoaiGiaoDich;
                // tinh tong thu theo phan loai
                tongthus = new double[size];
                KhoiTaoMang(tongthus, size);
                TinhTongTheoPhanLoai(list, tongthus, GiaoDich.KieuGiaoDich.Thu);

                // tinh tong chi theo phan loai
                tongchis = new double[size];
                KhoiTaoMang(tongchis, size);
                TinhTongTheoPhanLoai(list, tongchis, GiaoDich.KieuGiaoDich.Chi);
            }
        }

        // Nap chong ham ToString
        public override string ToString()
        {
            string st = string.Format("\nCo so du lieu co tat ca {0} giao dich.", this.SoLuong);
            for (int i = 0; i < this.SoLuong; i++)
            {
                st += string.Format("\n{0}.", i + 1);
                st += this[i];
            }

            return st;
        }

        // Cai dat giao dien IEnumerable
        public IEnumerator GetEnumerator()
        {
            foreach (GiaoDichTheoThang gdThang in listThang)
                foreach (GiaoDich gd in gdThang)
                    yield return gd;
        }

        // THANH PHAN PRIVATE
        // Ham tra ra vi tri cua GiaoDichTheoThang co thang nam nao do
        private int LayViTriGDThangNam(ThoiGian ngay)
        {
            for (int i = 0; i < listThang.Count; i++)
                if (listThang[i].ThangNam.LaTrungThangNam(ngay))
                    return i;
            return -1;
        }

        // Ham tra ra vi tri cua GiaoDichTheoThang dua vao vi tri cua GiaoDich
        private int LayViTriGDThangNam(ref int index)
        {
            int i;
            for (i = 0; i < listThang.Count; i++)
                if (index - listThang[i].SoLuong >= 0)
                    index -= listThang[i].SoLuong;
                else
                    break;
            return i;
        }

        // Ham tra ra danh sach cac giao dich thuc hien trong ngay nao do
        private List<GiaoDich> LayGDCuaNgay(ThoiGian ngay)
        {
            List<GiaoDich> list = new List<GiaoDich>();

            int index;
            if ((index = LayViTriGDThangNam(ngay)) == -1) return null;
            else
            {
                GiaoDichTheoThang gdThang = listThang[index];
                return gdThang.LayGDCuaNgay(ngay);
            }
        }

        // Ham tinh so du dau ky (truoc ngay nao do)
        private double LaySoDuTruocNgay(ThoiGian ngay)
        {
            double sodu = 0;
            foreach (GiaoDich gd in this)
                // chuyen khoan khong duoc tinh la thu hay chi
                // nen khong tinh vao so du
                if (gd.NgayGD < ngay && !(gd is ChuyenKhoan))
                    sodu += gd.SoLuong;
            return sodu;
        }

        // Ham tinh tong thu cua cac giao dich nao do
        private double TinhTongThu(List<GiaoDich> list)
        {
            double tongthu = 0;
            foreach (GiaoDich gd in list)
                if (gd is Thu)
                    tongthu += gd.SoLuong;
            return tongthu;
        }

        // Ham tinh tong chi cua cac giao dich nao do
        private double TinhTongChi(List<GiaoDich> list)
        {
            double tongthu = 0;
            foreach (GiaoDich gd in list)
                if (gd is Chi)
                    tongthu += gd.SoLuong;
            return tongthu;
        }

        // Ham khoi tao gia tri cua mot mang
        private void KhoiTaoMang(double[] arr, int size)
        {
            for (int i = 0; i < size; i++)
                arr[i] = 0;
        }

        // Ham tinh tong (co the thu hoac chi) theo phan loai
        private void TinhTongTheoPhanLoai(List<GiaoDich> list, double[] tong, GiaoDich.KieuGiaoDich kieuGD)
        {
            foreach (GiaoDich gd in list)
            {
                switch (kieuGD)
                {
                    case GiaoDich.KieuGiaoDich.Thu:
                        if (gd is Thu)
                            tong[(int)gd.LoaiGD] += Math.Abs(gd.SoLuong);
                        break;
                    case GiaoDich.KieuGiaoDich.Chi:
                        if (gd is Chi)
                            tong[(int)gd.LoaiGD] += Math.Abs(gd.SoLuong);
                        break;
                }
            }
        }
    }
}
