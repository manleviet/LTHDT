using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QLPH
{
    class DSPhong : Collection<Phong>, IFormattable
    {
        #region Khai bao bien va thuoc tinh
        private bool coThayDoi = true;
        public bool CoThayDoi
        {
            get
            {
                if (coThayDoi) return true;
                foreach (Phong p in this)
                    if (p.CoThayDoi) return true;
                return false;
            }
            set
            {
                coThayDoi = value;
                if (value == false)
                    foreach (Phong p in this)
                        p.CoThayDoi = false;
            }
        }

        // thuoc tinh chi muc
        // tra ra doi tuong Phong theo maphong
        public Phong this[string maphong]
        {
            get
            {
                foreach (Phong p in this)
                    if (p.MaPhong == maphong)
                        return p;
                return null;
            }
        }
        #endregion

        #region Nap chong ham cua Collection
        protected override void ClearItems()
        {
            if (Count > 0)
            {
                base.ClearItems();
                CoThayDoi = true;
            }
        }

        protected override void InsertItem(int index, Phong item)
        {
            base.InsertItem(index, item);
            CoThayDoi = true;
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            CoThayDoi = true;
        }

        protected override void SetItem(int index, Phong item)
        {
            base.SetItem(index, item);
            CoThayDoi = true;
        }
        #endregion

        // Ham them phong moi vao vi tri duoc sap xep theo ma phong
        public void ThemThichHop(Phong p)
        {
            // tim vi tri thich hop de them phong moi vao
            // sao cho danh sach cac phong duoc sap xep theo ma phong
            int i;
            for (i = 0; i < base.Count; i++)
                if (p < base[i])
                    break;
            base.InsertItem(i, p); // chen vao vi tri thich hop
        }

        // Ham lay thong tin cua 10 phong voi phong bat dau duoc truyen qua theo tham so vitribd
        public string LayThongTinMuoiPhong(int vitribd)
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

        #region Nap chong Object
        // Nap chong ham ToString de lay thong tin tat ca cac phong
        // theo dinh dang de xuat ra Console
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
        // "a" la dinh dang de lay ra day ten cac phong
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(format))
                format = "c";
            char first = format.ToLower()[0];
            if (format.Length == 1 && first == 'f')
            {// dinh dang de xuat du lieu ra file
                string temp = "";
                foreach (Phong p in this)
                    temp += string.Format("{0:f}\n", p);
                temp = temp.Remove(temp.Length - 1);
                return temp;
            }
            else if (format.Length == 1 && first == 'a')
            { // dinh dang de lay ra day ten cac phong
                string temp = "";
                foreach (Phong p in this)
                    temp += string.Format("{0},", p.MaPhong);
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
