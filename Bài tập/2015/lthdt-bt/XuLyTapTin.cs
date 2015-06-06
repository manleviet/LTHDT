using System;
using System.Collections.Generic;
using System.IO;

namespace QLPH
{
    // Lop ngoai le cho cac loi ve xu ly tap tin
    class XuLyTapTinException : Exception
    {
        public XuLyTapTinException() : base() { }
        public XuLyTapTinException(string msg) : base(msg) { }
        public XuLyTapTinException(string msg, Exception inner)
            : base(msg, inner) { }
    }

    // Lop nay xu ly tat ca cac chuc nang lien quan den file
    // lop nay la lop tien ich, nen cac ham deu dat la static
    // va lop nay la lop static
    static class XuLyTapTin
    {
        // Ham xu ly doc du lieu tu file
        // dau vao : duongdan - duong dan den file du lieu
        // dau ra : du lieu doc duoc duoc them vao doi tuong quanlyphong
        public static void DocDuLieu(string duongdan, QuanLyPhong quanlyphong)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(duongdan);
                // Doc du lieu danh sach Phong
                DocDSPhong(sr, quanlyphong);
                // Doc du lieu danh sach Don muon phong
                DocDSDonMuonPhong(sr, quanlyphong);
            }
            catch (FileNotFoundException fnx)
            {
                quanlyphong.XoaDuLieu();
                throw fnx;
            }
            catch (XuLyTapTinException ex)
            {
                quanlyphong.XoaDuLieu();
                throw ex;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        // Ham doc du lieu phong tu file
        private static void DocDSPhong(StreamReader sr, QuanLyPhong quanlyphong)
        {
            int sophong;
            // doc so phong, neu khong doc duoc thi nem ra loi
            if (!int.TryParse(sr.ReadLine(), out sophong)) throw new XuLyTapTinException("Noi dung tap tin du lieu bi hong.");

            for (int i = 0; i < sophong; i++) // duyet qua cac dong de doc thong tin cac phong
            {
                string input = sr.ReadLine(); // doc mot dong
                // neu khong doc duoc thi nem loi
                if (string.IsNullOrEmpty(input)) throw new XuLyTapTinException("Noi dung tap tin du lieu bi hong.");

                // tach gia tri
                string[] inputs = input.Split(',');

                // chuỗi phải chứa 3 thanh phan
                if (inputs.Length != 3) throw new XuLyTapTinException("Noi dung tap tin du lieu bi hong.");

                string maphong = inputs[0]; // thanh phan dau tien la ma phong

                // thanh phan thu hai la loai phong
                QuanLyPhong.LoaiPhong loaiPhong;
                if (!Enum.TryParse(inputs[1], out loaiPhong))
                    throw new XuLyTapTinException("Noi dung tap tin du lieu bi hong.");

                // thanh phan thu ba la suc chua
                int succhua;
                if (!int.TryParse(inputs[2], out succhua))
                    throw new XuLyTapTinException("Noi dung tap tin du lieu bi hong.");

                quanlyphong.ThemPhong(maphong, loaiPhong, succhua); // them phong moi vao cuoi danh sach
            }
        }

        // Ham doc du lieu don muon phong tu file
        private static void DocDSDonMuonPhong(StreamReader sr, QuanLyPhong quanlyphong)
        {
            int sodon;
            // doc so luong don muon phong, neu khong dung thi nem loi ra
            if (!int.TryParse(sr.ReadLine(), out sodon)) throw new XuLyTapTinException("Noi dung tap tin du lieu bi hong.");

            for (int i = 0; i < sodon; i++) // duyet qua tung dong de doc du lieu cac don muon phong
            {
                string input = sr.ReadLine(); // doc mot dong
                // neu khong doc duoc thi nem loi ra
                if (string.IsNullOrEmpty(input)) throw new XuLyTapTinException("Noi dung tap tin du lieu bi hong.");

                // tach gia tri
                string[] inputs = input.Split(',');

                // chuỗi phải chứa 7 thanh phan
                if (inputs.Length != 7) throw new XuLyTapTinException("Noi dung tap tin du lieu bi hong.");

                string madon = inputs[0]; // thanh phan dau tien la ma don

                // thanh phan thu hai la ten nguoi muon
                string tenNgMuon = inputs[1];
                // thanh phan thu ba la don vi cong tac
                string dvCongtac = inputs[2];
                // thanh phan thu tu la ma phong
                string maphong = inputs[3];
                // kiem tra xem phong co co trong danh sach phong hay khong?
                if (!quanlyphong.LaCoMaPhong(maphong)) throw new XuLyTapTinException("Noi dung tap tin du lieu bi loi.");
                // thanh phan thu nam la ngay muon
                NgayThang ngayMuon;
                if (!NgayThang.TryParse(inputs[4], out ngayMuon))
                    throw new XuLyTapTinException("Noi dung tap tin du lieu bi hong");
                // thanh phan thu sau la tiet bat dau muon
                int tietBDMuon;
                if (!int.TryParse(inputs[5], out tietBDMuon))
                    throw new XuLyTapTinException("Noi dung tap tin du lieu bi hong");
                // thanh phan thu bay la tiet ket thuc muon
                int tietKTMuon;
                if (!int.TryParse(inputs[6], out tietKTMuon))
                    throw new XuLyTapTinException("Noi dung tap tin du lieu bi hong");

                try
                {
                    quanlyphong.ThemDon(madon, tenNgMuon, dvCongtac,
                                        maphong, ngayMuon, tietBDMuon, tietKTMuon); // them don moi vao danh sach
                }
                catch (ArgumentException ex)
                {
                    string mess = string.Format("{0}. Noi dung tap tin du lieu bi hong", ex.Message);
                    throw new XuLyTapTinException(mess);
                }
            }
        }

        // Ham xu ly ghi du lieu vao file
        public static void LuuDuLieu(string duongdan, QuanLyPhong quanlyphong)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(duongdan, false);

                // Luu du lieu phong
                sw.WriteLine(quanlyphong.SoPhong); // luu so luong phong
                sw.WriteLine(quanlyphong.LayDSPhongDeLuuFile());

                // Luu du lieu don muon phong
                sw.WriteLine(quanlyphong.SoDonMuonPhong); // luu so luong don muon phong
                sw.WriteLine(quanlyphong.LayDSDonMPDeLuuFile());
            }
            catch (XuLyTapTinException ex)
            {
                throw ex;
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
    }
}
