using System;
using System.IO;

namespace XuLySoLieu
{
    // Lop nay xu ly tat ca cac chuc nang lien quan den file
    static class XuLyFile
    {
        // Doc du lieu mau tu file
        public static MauNgauNhien DocFile(string path)
        {
            // mo file ra de doc
            StreamReader reader = new StreamReader(path);

            if (reader == null) return null; // neu khong mo duoc thi tra ra null

            // Đọc dòng đầu tiên để xử lý kiểu quan sát
            String input = reader.ReadLine();
            MauNgauNhien.KieuQuanSat kieuqs;
            if (!Enum.TryParse(input, out kieuqs)) // kiem tra xem du lieu o dong dau co phu hop voi 3 dang quan sat
                return null; // neu khong, tra ra null

            // tao mau ngau nhien moi
            MauNgauNhien mau = new MauNgauNhien(kieuqs);

            // Đọc dòng tiếp theo để biết số thành phần
            input = reader.ReadLine();
            int k;
            if (!int.TryParse(input, out k)) return null; // neu dong tiep theo khong dung khuon dang thi cung tra ra null

            // Đọc từng dòng, tùy vào kiểu quan sát mà tách các giá trị ra
            double x;
            double y;
            int m;
            for (int i = 0; i < k; i++)
            {
                input = reader.ReadLine();
                switch (kieuqs)
                {
                    case MauNgauNhien.KieuQuanSat.ThongThuong:
                        if (!TachGiaTri(input, out x)) return null; // neu khong tach dung thi tra ra null
                        mau.AddQuanSat(x); // tach duoc thi them quan sat do vao mau
                        break;
                    case MauNgauNhien.KieuQuanSat.ThuGon:
                        if (!TachGiaTri(input, out x, out m)) return null;
                        mau.AddQuanSat(x, m);
                        break;
                    case MauNgauNhien.KieuQuanSat.Khoang:
                        if (!TachGiaTri(input, out x, out y, out m)) return null;
                        mau.AddQuanSat(x, y, m);
                        break;
                }
            }
            reader.Close(); // dong file lai
             
            return mau; // tra mau doc duoc ra
        }

        private static bool TachGiaTri(string input, out double x)
        {
            // xử lý tham số out
            x = 0.0;
            
            string[] inputs = input.Split();
            // chuỗi phải chứa 1 số
            if (inputs.Length != 1) return false;

            // sử dụng hàm TryParse để kiểm tra xem có thể
            // chuyển chuỗi số về số kiểu double hay không
            // hàm này sẽ trả ra true nếu ép kiểu được
            // giá trị ép kiểu được sẽ trả ra cho tham số thứ hai
            if (!double.TryParse(input, out x))
                return false;
            return true;
        }

        private static bool TachGiaTri(string input, out double x, out int m)
        {
            // xử lý tham chiếu out
            x = 0.0;
            m = 0;

            // tách chuỗi
            string[] inputs = input.Split();

            // chuỗi phải có 2 số
            if (inputs.Length != 2) return false;

            if (!double.TryParse(inputs[0], out x))
                return false;

            // xử lý số thứ hai
            if (!int.TryParse(inputs[1], out m)) 
                return false;

            return true;
        }

        private static bool TachGiaTri(string input, out double x, out double y, out int m)
        {
            // xử lý cho tham số out
            x = 0.0;
            y = 0.0;
            m = 0;

            // tách chuỗi
            string[] inputs = input.Split();

            // chuỗi phải chứa 3 số 
            if (inputs.Length != 3) return false;

            // xử lý số đầu tiên
            if (!double.TryParse(inputs[0], out x))
                return false;

            // xử lý số thứ hai
            if (!double.TryParse(inputs[1], out y))
                return false;

            // xử lý số thứ ba
            if (!int.TryParse(inputs[2], out m))
                return false;

            return true;
        }

        public static bool LuuMauRaFile(MauNgauNhien mau, string pathout)
        {
            StreamWriter writer = new StreamWriter(pathout);

            if (writer == null) return false;

            // dong dau tien la 1 so the hien 3 kieu quan sat
            writer.WriteLine(mau.KieuQS);
            // dong thu hai la so thanh phan
            writer.WriteLine(mau.Count);
            // cac dong tiep theo 
            foreach (QuanSat qs in mau)
            {
                string tam = qs.ToString();

                string newtam = tam.Replace("\t", " ");

                writer.WriteLine(newtam);
            }

            writer.Close();

            return true;
        }

        public static bool LuuKQRaFile(MauNgauNhien mau, string pathout)
        {
            StreamWriter writer = new StreamWriter(pathout);

            if (writer == null) return false;

            // luu gia tri ky vong va phuong sai
            writer.WriteLine("{0:f4} {1:f4}", mau.KyVong, mau.PhuongSai);
            // neu co uoc luong thi luu ket qua uoc luong
            if (mau.KetQuaULMoiNhat != null)
                writer.WriteLine("{0}", mau.KetQuaULMoiNhat);
            // neu co kiem dinh thi luu ket qua kiem dinh
            if (mau.KetQuaKDMoiNhat != null)
                writer.WriteLine("{0}", mau.KetQuaKDMoiNhat);

            writer.Close();

            return true;
        }
    }
}
