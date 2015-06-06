using System;
using System.IO;

namespace QLTC_HDT
{
    // Lop nay xu ly tat ca cac chuc nang lien quan den file
    // lop nay la lop tien ich, nen cac ham deu dat la static
    // va lop nay la lop static
    static class XuLyFile
    {
        // Ham xu ly doc du lieu tu file
        // dau vao : pathIn - duong dan den file du lieu
        // dau ra : listTK - danh sach cac tai khoan
        //          listGD - danh sach cac giao dich
        public static void LayDuLieuTuFile(string pathIn, DSTaiKhoan listTK, DSGiaoDich listGD)
        {
            using (StreamReader reader = new StreamReader(pathIn))
            {
                string input = reader.ReadLine(); // doc so tai khoan

                int soTK;
                if (!int.TryParse(input, out soTK)) return; // neu khong phai so tai khoan thi thoat

                TaiKhoan tk;
                for (int i = 0; i < soTK; i++) // lap qua so dong theo so tai khoan
                {
                    input = reader.ReadLine(); // doc mot dong

                    // tach du lieu trong moi dong va tao ra doi tuong Tai khoan moi
                    if (!TachGiaTri(input, out tk, listGD)) return; 

                    listTK.ThemTK(tk); // them Tai khoan moi vao danh sach
                }

                input = reader.ReadLine(); // doc dong so giao dich
                int soGD;
                if (!int.TryParse(input, out soGD)) return;

                GiaoDich gd;
                for (int i = 0; i < soGD; i++) // duyet qua cac giao dich
                {
                    input = reader.ReadLine(); // doc mot dong thong tin giao dich

                    // tach thong tin giao dich va tao Giao dich moi
                    if (!TachGiaTri(input, out gd, listTK)) return;

                    listGD.ThemGD(gd); // them Giao dich moi vao danh sach
                }
            }
        }

        // Ham xu ly tach dong thong tin tai khoan
        // va tra ra mot Tai khoan moi
        private static bool TachGiaTri(string input, out TaiKhoan tk, DSGiaoDich listGD)
        {
            // xu ly tham so out, phong truong hop doc du lieu bi loi
            tk = null;

            string[] inputs = input.Split('-');

            // chuỗi phải chứa 4 thanh phan
            if (inputs.Length != 4) return false;

            string ten = inputs[0]; // thanh phan dau tien la ten tai khoan

            // thanh phan thu hai la kieu tai khoan
            TaiKhoan.KieuTaiKhoan kieuTK;
            if (!Enum.TryParse(inputs[1], out kieuTK))
                return false;

            ThoiGian ngaymo = inputs[2]; // thanh phan thu ba la ngay

            // thanh phan thu tu la so tien
            double sotienbandau;
            // sử dụng hàm TryParse để kiểm tra xem có thể
            // chuyển chuỗi số về số kiểu double hay không
            // hàm này sẽ trả ra true nếu ép kiểu được
            // giá trị ép kiểu được sẽ trả ra cho tham số thứ hai
            if (!double.TryParse(inputs[3], out sotienbandau)) return false;

            tk = new TaiKhoan(ten, sotienbandau, ngaymo, kieuTK, listGD);
            return true;
        }

        // Ham xu ly tach thong tin giao dich
        // va tra ra giao dich moi
        private static bool TachGiaTri(string input, out GiaoDich gd, DSTaiKhoan listTK)
        {
            // xu ly tham so out, phong truong hop doc du lieu bi loi
            gd = null;

            string[] inputs = input.Split('-');

            // chuỗi phải chứa 8 thanh phan
            if (inputs.Length != 8) return false;

            GiaoDich.KieuGiaoDich kGD;
            if (!Enum.TryParse(inputs[0], out kGD)) return false;

            int ma;
            if (!int.TryParse(inputs[1], out ma)) return false;

            ThoiGian ngaygd = inputs[2];

            double soluong;
            if (!double.TryParse(inputs[3], out soluong)) return false;

            GiaoDich.LoaiGiaoDich lGD;
            if (!Enum.TryParse(inputs[4], out lGD)) return false;

            string ghichu = inputs[5];

            int index;
            if ((index = listTK.LayViTriCuaTKCoTen(inputs[6])) == -1) return false;
            TaiKhoan tk = listTK[index];

            TaiKhoan tkNhan = null;
            if ((inputs[7] != "") && ((index = listTK.LayViTriCuaTKCoTen(inputs[7])) == -1))
                return false;
            if (inputs[7] != "")
                tkNhan = listTK[index];

            switch (kGD)
            {
                case GiaoDich.KieuGiaoDich.Thu:
                    gd = new Thu(ma, ngaygd, soluong, tk, lGD, ghichu);
                    break;
                case GiaoDich.KieuGiaoDich.Chi:
                    gd = new Chi(ma, ngaygd, soluong, tk, lGD, ghichu);
                    break;
                case GiaoDich.KieuGiaoDich.ChuyenKhoan:
                    gd = new ChuyenKhoan(ma, ngaygd, soluong, tk, tkNhan, lGD, ghichu);
                    break;
            }

            return true;
        }

        // Ham xu ly luu danh sach Tai khoan va danh sach giao dich ra file
        public static bool LuuDuLieuRaFile(DSTaiKhoan listTK, DSGiaoDich listGD, string pathout)
        {
            using (StreamWriter writer = new StreamWriter(pathout))
            {
                if (writer == null) return false;

                // dong dau tien la so tai khoan
                writer.WriteLine(listTK.SoLuong);
                // cac dong tiep theo luu thong tin tai khoan
                foreach (TaiKhoan tk in listTK)
                    writer.WriteLine(tk.LayDuLieuGhiFile());

                // dong cho so luong giao dich
                writer.WriteLine(listGD.SoLuong);
                // cac dong tiep theo luu thong tin giao dich
                foreach (GiaoDich gd in listGD)
                    writer.WriteLine(gd.LayDuLieuGhiFile());
            }
            return true;
        }
    }
}
