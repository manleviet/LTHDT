using System;
using System.Collections.Generic;

namespace QLPH
{
    class QuanLyPhong
    {
        #region Quan ly duong dan mac dinh den data.txt
        static private string duongDanMacDinh;
        static public string DuongDanMacDinh // duong dan mac dinh den file data.txt
        {
            get { return duongDanMacDinh; }
        }

        static QuanLyPhong()
        {
            // data.txt dat cung thu muc voi tap tin exe cua chuong trinh
            duongDanMacDinh = AppDomain.CurrentDomain.BaseDirectory + @"data.txt";
        }
        #endregion

        private bool coThayDoi = true;
        public bool CoThayDoi
        {
            get
            {
                if (coThayDoi) return true;
                foreach (Phong p in listPhong)
                    if (p.CoThayDoi) return true;
                foreach (DonMuonPhong don in listDon)
                    if (don.CoThayDoi) return true;
                return false;
            }
            set
            {
                coThayDoi = value;
                if (value == false)
                {
                    foreach (Phong p in listPhong)
                        p.CoThayDoi = false;
                    foreach (DonMuonPhong don in listDon)
                        don.CoThayDoi = false;
                }
            }
        }

        public enum LoaiPhong { PhongHoc, ThucHanh }
        private DSPhong listPhong;
        private DSDonMuonPhong listDon;

        // Cau tu
        public QuanLyPhong()
        {
            listPhong = new DSPhong();
            listDon = new DSDonMuonPhong();
        }

        #region Cac thuoc tinh va ham xu ly tren Don muon phong
        // Thuoc tinh kiem tra danh sach Don muon phong co don nao khong?
        public bool LaKhongCoDon
        {
            get
            {
                return SoDonMuonPhong == 0;
            }
        }

        // Thuoc tinh tra ra so luong don trong danh sach Don muon phong
        public int SoDonMuonPhong
        {
            get
            {
                return listDon.Count;
            }
        }

        // Ham kiem tra co ma don trong danh sach Don muon phong
        public bool LaCoMaDon(string ma)
        {
            foreach (DonMuonPhong don in listDon)
                if (don.Ma == ma)
                    return true;
            return false;
        }

        // Ham them don muon phong moi vao cuoi danh sach Don muon phong
        public void ThemDon(string ma, string nguoiMuon, string dvCongTac, string maphong,
                            NgayThang ngayMuon, int tietBDMuon, int tietKTMuon)
        {
            if (LaCoMaDon(ma)) throw new ArgumentException("Trung ma don muon phong.");

            DonMuonPhong don = new DonMuonPhong(ma, nguoiMuon, dvCongTac,
                                                listPhong[maphong], ngayMuon, tietBDMuon, tietKTMuon);

            if (don != null)
                listDon.Add(don);
        }

        // Ham them don muon phong moi vao vi tri thich hop
        // phu hop voi sap xep theo thoi gian
        public void ThemDonVoiViTriThichHop(string nguoiMuon, string dvCongTac, string maphong,
                                            NgayThang ngayMuon, int tietBDMuon, int tietKTMuon)
        {
            DonMuonPhong don = new DonMuonPhong(listDon.MaChoDonMoi, nguoiMuon, dvCongTac, 
                                                listPhong[maphong], ngayMuon, tietBDMuon, tietKTMuon);

            if (don != null)
                listDon.ThemThichHop(don);
        }

        // Ham sua thong tin mot don muon phong
        public void SuaTTMotDonMuonPhong(string madon, string tenNgMuon, string dvCongtac, string maphong,
                                         NgayThang ngayMuon, int tietBDMuon, int tietKTMuon)
        {
            DonMuonPhong donCu = listDon[madon];
            DonMuonPhong donMoi = new DonMuonPhong(madon, tenNgMuon, dvCongtac, listPhong[maphong], ngayMuon, tietBDMuon, tietKTMuon);

            listDon.Remove(donCu);
            listDon.Insert(listDon.LayViTriThichHop(donMoi), donMoi);
        }

        // Ham xoa mot don muon phong
        public void XoaDonMuonPhong(string madon)
        {
            listDon.Remove(listDon[madon]);
        }

        // Ham lay thong tin cua 10 don muon phong theo thu tu
        public string LayThongTinMuoiDonMuonPhong(int thu)
        {
            string temp = "";
            int vitribd = thu * 10;
            temp += listDon.LayThongTinMuoiDonMuonPhong(vitribd);
            return temp;
        }
        
        // Ham lay thong tin tat ca cac don muon phong de luu file
        public string LayDSDonMPDeLuuFile()
        {
            return string.Format("{0:f}", listDon);
        }

        // Ham lay thong tin mot don muon phong
        public string LayTTMotDonMuonPhong(string madon)
        {
            return string.Format("{0}", listDon[madon]);
        }

        // Ham lay thong tin cac don muon phong thuoc thoi gian
        public List<string> LayTTCacDonMPThuocThoiGian(NgayThang ngayBD, NgayThang ngayKT)
        {
            return listDon.LayTTCacDonMPThuocThoiGian(ngayBD, ngayKT);
        }

        // Ham lay thong tin cac don muon phong cua mot nguoi trong thang nao do
        public List<string> LayTTCacDonMPTheoNguoiMuon(string nguoiMuon, NgayThang ngaythang)
        {
            return listDon.LayTTCacDonMPTheoNguoiMuon(nguoiMuon, ngaythang);
        }

        // Ham lay ma cac phong duoc su dung trong thang
        public List<string> LayDSMaPhongDuocSDTrongThang(NgayThang thangnam)
        {
            return listDon.LayDSMaPhongDuocSDTrongThang(thangnam);
        }

        // Ham lay danh sach cac tiet trong cua mot phong trong ngay nao do
        public List<int> LayDSTietTrong(string maphong, NgayThang ngay)
        {
            // mang tuong trung cho 10 tiet
            // gia tri true co nghia la tiet co phong trong
            bool[] temp = new bool[] { true, true, true, true, true, true, true, true, true, true };
            foreach (DonMuonPhong don in listDon)
                if (don.PhongMuon.MaPhong == maphong && don.NgayMuon == ngay)
                {
                    for (int i = don.TietBDMuon; i <= don.TietKTMuon; i++) // duyet qua tat ca cac tiet
                        temp[i - 1] = false; // gan thanh khong trong (false)
                }
            List<int> tiettrong = new List<int>();
            for (int i = 0; i < temp.Length; i++)
                if (temp[i]) tiettrong.Add(i + 1); // tao ra mang cac tiet trong
            return tiettrong;
        }

        // Tinh so lan su dung cua cac phong trong thang
        public List<int> TinhSoLanSD(List<string> dsMaPhong, NgayThang thangnam)
        {
            List<int> dsSLSD = new List<int>();
            foreach (string maphong in dsMaPhong)
                dsSLSD.Add(listDon.TinhSoLanSD(maphong, thangnam));
            return dsSLSD;
        }
        #endregion

        #region Cac thuoc tinh va ham xu ly tren Phong
        // Thuoc tinh tra ra mot chuoi chua ten cac loai phong
        public static string ChuoiLoaiPhong
        {
            get
            {
                return "PhongHoc, ThucHanh";
            }
        }

        // Thuoc tinh kiem tra danh sach phong co du lieu khong
        // neu khong tra ra true va nguoc lai
        public bool LaKhongCoPhong
        {
            get
            {
                return SoPhong == 0;
            }
        }

        // Thuoc tinh tra ra sos luong phong hien co
        public int SoPhong
        {
            get { return listPhong.Count; }
        }

        // Ham kiem tra co ton tai ma phong trong danh sach phong
        // tra ra true neu co, false neu nguoc lai
        public bool LaCoMaPhong(string maphong)
        {
            foreach (Phong p in listPhong)
                if (p.MaPhong == maphong)
                    return true;
            return false;
        }

        // Them phong moi vao cuoi danh sach
        public void ThemPhong(string ma, LoaiPhong loaiphong, int succhua)
        {
            Phong phong = TaoPhong(ma, loaiphong, succhua);

            if (phong != null)
                listPhong.Add(phong);
        }

        // Them phong moi vao vi tri thich hop, theo thu tu maphong
        public void ThemPhongVoiViTriThichHop(string ma, LoaiPhong loaiphong, int succhua)
        {
            Phong phong = TaoPhong(ma, loaiphong, succhua);

            if (phong != null)
                listPhong.ThemThichHop(phong);
        }

        // Ham thuc hien sua thong tin mot phong
        public void SuaTTMotPhong(string maphong, LoaiPhong loaiphong, int succhua)
        {
            Phong pMoi = null;
            Phong pCu = listPhong[maphong]; // lay doi tuong phong cu
            int index = listPhong.IndexOf(pCu); // lay vi tri cua phong cu trong danh sach

            // tao phong moi
            if (loaiphong == LoaiPhong.PhongHoc && pCu is ThucHanh)
                pMoi = new PhongHoc(maphong, succhua);
            else if (loaiphong == LoaiPhong.ThucHanh && pCu is PhongHoc)
                pMoi = new ThucHanh(maphong, succhua);
            else
            { // truong hop chi sua succhua
                pCu.SucChua = succhua; // khong tao phong moi
                return; // thoat khoi ham
            }
            listPhong.RemoveAt(index); // xoa phong cu
            listPhong.Insert(index, pMoi); // chen phong moi
        }

        // Ham thuc hien xoa phong theo ma phong
        public void XoaPhong(string maphong)
        {
            listPhong.Remove(listPhong[maphong]);
        }

        // Ham lay thong tin cua 10 phong theo thu tu
        public string LayThongTinMuoiPhong(int thu)
        {
            string temp = "";
            int vitribd = thu * 10;
            temp += listPhong.LayThongTinMuoiPhong(vitribd);
            return temp;
        }

        // Ham lay thong tin cac phong de luu file
        public string LayDSPhongDeLuuFile()
        {
            return string.Format("{0:f}",listPhong);
        }

        // Ham lay danh sach tat cac cac phong
        public string LayDSMaPhong()
        {
            return string.Format("{0:a}", listPhong);
        }

        // Ham lay thong tin mot phong theo ma phong
        public string LayTTMotPhong(string maphong)
        {
            return string.Format("{0}", listPhong[maphong]);
        }

        // Ham lay danh sach phong trong trong ngay nao do va phu hop voi dieu kien
        // ve loai phong va suc chua
        public List<string> TimPhongTrong(NgayThang ngay, LoaiPhong loaiphong, int succhua)
        {
            List<string> list = new List<string>();
            // lay danh sach cac phong duoc su dung trong thang
            List<string> DSPhongDuocSD = LayDSMaPhongDuocSDTrongThang(ngay);
            // tu do, lay danh sach cac phong khong duoc su dung trong thang
            // va phu hop voi suc chua
            foreach (Phong p in listPhong)
                if (DSPhongDuocSD.IndexOf(p.MaPhong) < 0 && p.SucChua >= succhua &&
                    ((loaiphong == LoaiPhong.PhongHoc && p is PhongHoc) ||
                    (loaiphong == LoaiPhong.ThucHanh && p is ThucHanh)))
                    list.Add(p.MaPhong);
            return list;
        }

        // Tao mot phong moi tu tham so dau vao
        // tuy theo loai phong ma tao doi tuong cho thich hop
        private Phong TaoPhong(string ma, LoaiPhong loaiphong, int succhua)
        {
            Phong phong = null;
            switch (loaiphong)
            {
                case LoaiPhong.PhongHoc:
                    phong = new PhongHoc(ma, succhua);
                    break;
                case LoaiPhong.ThucHanh:
                    phong = new ThucHanh(ma, succhua);
                    break;
            }
            return phong;
        }
        #endregion

        #region Cac ham tien ich khac
        // Ham loc bo nhung tiet khong the chon sau khi da chon duoc tiet bat dau muon
        public static List<int> LayDSTietTrong(List<int> tiettrong, int tietBDMuon)
        {
            // bo nhung tiet nho hon va bang tietbdmuon
            while (tiettrong.Count > 0 && tiettrong[0] <= tietBDMuon)
            {
                tiettrong.RemoveAt(0);
            }
            // tim gia tri bi trong
            int i = 0;
            tietBDMuon++;
            while (i < tiettrong.Count && tiettrong[i] == tietBDMuon + i)
            {
                i++;
            }
            tiettrong.RemoveRange(i, tiettrong.Count - i);
            return tiettrong;
        }

        // Ham tinh tong so tiet su dung phong trong thang
        public int TinhTongSoTietSDTrongThang(NgayThang thangnam)
        {
            int ngayCuoiCungCuaThang = NgayThang.NgayCuoiCungCuaThang(thangnam);

            // so tiet bang so ngay trong thang - so ngay chu nhat * 10 (moi ngay 10 tiet)
            int soTiet = (ngayCuoiCungCuaThang - NgayThang.SoNgayCN(thangnam)) * 10;

            return soTiet;
        }

        // Ham tinh tien muon phong theo don muon phong
        public double TinhTienMuonPhong(string madon)
        {
            return listDon[madon].TienMuonPhong;
        }

        // Ham xoa trong hai danh sach Phong va Don muon phong
        public void XoaDuLieu()
        {
            listPhong.Clear();
            listDon.Clear();
        }
        #endregion
    }
}
