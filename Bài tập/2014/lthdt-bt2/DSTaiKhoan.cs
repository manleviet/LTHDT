using System;
using System.Collections;
using System.Collections.Generic;

namespace QLTC_HDT
{
    class DSTaiKhoan : IEnumerable
    {
        // [Quan hệ]Quan hệ thu nap với lớp TaiKhoan
        List<TaiKhoan> listTK;

        // Thuoc tinh kiem tra rong
        public bool LaRong
        {
            get
            {
                return listTK.Count == 0;
            }
        }

        // Thuoc tinh de lay so luong tai khoan hien co
        public int SoLuong
        {
            get
            {
                return listTK.Count;
            }
        }

        // Thuộc tính chỉ mục
        public TaiKhoan this[int index]
        {
            get
            {
                if (index < 0 || index >= listTK.Count)
                    return null;
                return listTK[index];
            }
            set
            {
                if (index >= 0 && index < listTK.Count)
                    listTK[index] = value;
            }
        }

        // CAU TU
        public DSTaiKhoan()
        {
            listTK = new List<TaiKhoan>(); // khoi tao danh sach
        }

        ~DSTaiKhoan()
        {
            // [Quan he thu nap] xoa cac doi tuong
            listTK = null;
        }

        // Add
        public void ThemTK(TaiKhoan tk)
        {
            //[Quan he thu nap]
            listTK.Add(tk);
        }

        // IndexOf
        public int LayViTriCuaTKCoTen(string ten)
        {
            for (int i = 0; i < listTK.Count; i++)
                if (listTK[i].Ten == ten)
                    return i;
            return -1;
        }

        // RemoveAt
        public void XoaTKCoViTri(int index)
        {
            listTK.RemoveAt(index);
        }

        // Ham tra ra danh sach ten cac tai khoan hien co trong danh sach
        public string LayDanhSachTenCacTaiKhoan()
        {
            string st = "";
            foreach (TaiKhoan tk in listTK)
                st += string.Format("{0}, ", tk.Ten);
            return st.Remove(st.Length - 2, 2);
        }

        // Nap chong ham ToString cua lop Object
        // de in ra thong tin tat ca cac Tai khoan
        public override string ToString()
        {
            string st = string.Format("\nCo so du lieu co tat ca {0} tai khoan.", listTK.Count);
            for (int i = 0; i < listTK.Count; i++)
            {
                st += string.Format("\n{0}.", i + 1);
                st += listTK[i];
            }

            return st;
        }

        // Ham lay du lieu tat ca cac tai khoan de ghi ra file
        public string LayDuLieuGhiFile()
        {
            string st = "";
            foreach (TaiKhoan tk in listTK)
                st += string.Format("{0}/n", tk.LayDuLieuGhiFile());
            return st;
        }

        // Cai dat giao dien IEnumerable
        // de cho phep lop nay dung voi cau lenh foreach
        public IEnumerator GetEnumerator()
        {
            return listTK.GetEnumerator();
        }
    }
}
