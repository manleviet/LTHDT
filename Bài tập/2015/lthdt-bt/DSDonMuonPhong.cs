using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QLPH
{
    class DSDonMuonPhong : Collection<DonMuonPhong>, IFormattable
    {
        #region Khai bao bien va thuoc tinh
        private bool coThayDoi = true;
        public bool CoThayDoi
        {
            get
            {
                if (coThayDoi) return true;
                foreach (DonMuonPhong p in this)
                    if (p.CoThayDoi) return true;
                return false;
            }
            set
            {
                coThayDoi = value;
                if (value == false)
                    foreach (DonMuonPhong p in this)
                        p.CoThayDoi = false;
            }
        }

        // thuoc tinh chi muc
        // tra ra doi tuong DonMuonPhong theo ma
        public DonMuonPhong this[string ma]
        {
            get
            {
                foreach (DonMuonPhong don in this)
                    if (don.Ma == ma)
                        return don;
                return null;
            }
        }

        // thuoc tinh tra ra ma tu dong cho mot don muon phong moi
        public string MaChoDonMoi
        {
            get
            {
                int max = 0;
                // lay gia tri ma lon nhat
                foreach (DonMuonPhong don in this)
                {
                    int ma = int.Parse(don.Ma);
                    if (ma > max)
                        max = ma;
                }
                return (max + 1).ToString(); // tang len mot
            }
        }
        #endregion

        #region Nap chong Collection
        protected override void ClearItems()
        {
            if (Count > 0)
            {
                base.ClearItems();
                CoThayDoi = true;
            }
        }

        protected override void InsertItem(int index, DonMuonPhong item)
        {
            base.InsertItem(index, item);
            CoThayDoi = true;
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            CoThayDoi = true;
        }

        protected override void SetItem(int index, DonMuonPhong item)
        {
            base.SetItem(index, item);
            CoThayDoi = true;
        }
        #endregion

        #region Cac ham public
        // Ham them don muon phong moi vao vi tri
        // duoc sap xep theo ngay thang
        public void ThemThichHop(DonMuonPhong don)
        {
            // tim vi tri thich hop de them don moi vao
            // sao cho danh sach cac don duoc sap xep theo ngay thang
            int i = LayViTriThichHop(don);
            base.InsertItem(i, don);
        }

        // Ham tim vi tri chen thich hop cua mot don moi
        // sao cho cac don tuan theo thu tu ve thoi gian
        public int LayViTriThichHop(DonMuonPhong don)
        {
            int i;
            for (i = 0; i < base.Count; i++)
                if (don < base[i]) // nhung tiet muon nho hon hoac bang
                    break;
            return i;
        }
        
        // Ham lay thong tin cua 10 don muon phong voi don bat dau duoc truyen qua theo tham so vitribd
        public string LayThongTinMuoiDonMuonPhong(int vitribd)
        {
            string temp = "";
            int vitrikt = (vitribd + 10 > Count) ? Count : vitribd + 10;
            int dem = vitribd;
            for (int i = vitribd; i < vitrikt; i++)
            {
                dem++;
                temp += string.Format("{0}.\n", dem);
                temp += this[i];
            }
            return temp;
        }

        // Ham lay thong tin cac don muon phong thuoc khoang thoi gian
        public List<string> LayTTCacDonMPThuocThoiGian(NgayThang ngayBD, NgayThang ngayKT)
        {
            List<string> list = new List<string>();
            foreach (DonMuonPhong don in this)
                if (ngayBD <= don.NgayMuon && don.NgayMuon <= ngayKT)
                    list.Add(don.ToString());
            return list;
        }

        // Ham lay thong tin cac don muon phong cua mot nguoi vao thang nao do
        public List<string> LayTTCacDonMPTheoNguoiMuon(string nguoiMuon, NgayThang ngaythang)
        {
            List<string> list = new List<string>();
            foreach (DonMuonPhong don in this)
                if (nguoiMuon == don.TenNguoiMuon 
                    && don.NgayMuon.Thang == ngaythang.Thang 
                    && don.NgayMuon.Nam == ngaythang.Nam)
                    list.Add(don.ToString());
            return list;
        }

        // Ham lay danh sach ma cac phong duoc su dung trong thang
        public List<string> LayDSMaPhongDuocSDTrongThang(NgayThang thangnam)
        {
            List<string> list = new List<string>();
            foreach (DonMuonPhong don in this)
                if (don.NgayMuon.Thang == thangnam.Thang && don.NgayMuon.Nam == thangnam.Nam
                    && list.IndexOf(don.PhongMuon.MaPhong) < 0)
                    list.Add(don.PhongMuon.MaPhong);
            return list;
        }

        // Ham tinh so lan duoc su dung cua mot phong trong thang
        public int TinhSoLanSD(string maphong, NgayThang thangnam)
        {
            int dem = 0;
            foreach (DonMuonPhong don in this)
                if (don.PhongMuon.MaPhong == maphong &&
                    don.NgayMuon.Thang == thangnam.Thang &&
                    don.NgayMuon.Nam == thangnam.Nam)
                    dem += don.TietKTMuon - don.TietBDMuon + 1;
            return dem;
        }
        #endregion

        #region Nap chong Object
        public override string ToString()
        {
            string temp = "";
            int dem = 0;
            for (int i = 0; i < Count; i++)
            {
                dem++;
                temp += string.Format("{0}.\n", dem);
                temp += this[i];
            }
            return temp;
        }
        #endregion

        #region Nap chong IFormattable
        // Sau khi cai dat ham nay, co the su dung nhu dang ham ToString
        // nhung kem them ky tu de dinh dang cho phu hop
        // O day, "c" la dai dien cho Console - du lieu xuat ra de hien thi tren Console
        // "f" la dai dien cho File - du lieu xuat ra de luu file
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(format))
                format = "c";
            char first = format.ToLower()[0];
            if (format.Length == 1 && first == 'f')
            {// dinh dang de xuat du lieu ra file
                string temp = "";
                foreach (DonMuonPhong don in this)
                    temp += string.Format("{0:f}\n", don);
                temp = temp.Remove(temp.Length - 1);
                return temp;
            }
            else if (first == 'c') // dinh dang de xuat ra console 
                return ToString();
            throw new FormatException();
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return ToString(null, formatProvider);
        }
        #endregion
    }
}
