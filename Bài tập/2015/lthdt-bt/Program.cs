using System;
using System.IO;
using System.Collections.Generic;

namespace QLPH
{
    // Lop ngoai le nay duoc dung de thoat khoi quy trinh nhap
    // Khi dang nhap du lieu, neu ban muon thoat khoi viec nhap tiep thi bam Ctrl+Z
    class CtrlZException : Exception
    {
        public CtrlZException() : base() { }
        public CtrlZException(string msg) : base(msg) { }
        public CtrlZException(string msg, Exception inner)
            : base(msg, inner) { }
    }

    class Program
    {
        #region Vung khai bao bien
        // Kieu cu phap the hien cu phap dong lenh khi goi chuong trinh
        // Dang 1 : QuanLyPhong.exe
        // Dang 2 : QuanLyPhong.exe -h
        enum KieuCuPhap { Dang1, Dang2 };
        // bien kieuCP de nhan dang cu phap dong lenh
        static KieuCuPhap kieuCP;

        static QuanLyPhong quanlyPhong;
        #endregion

        #region Main
        static void Main(string[] args)
        {
            // Kiem tra doi so dong lenh
            // de xac dinh dang cu phap dong lenh
            if (args.Length == 0)
                kieuCP = KieuCuPhap.Dang1; // khi khong co doi so nao thi thuc hien theo dang 1
            else if (args.Length == 1 && args[0] == "-h")
                kieuCP = KieuCuPhap.Dang2; // thuoc dang 2
            else
            {
                // In thong bao loi va hien thi huong dan su dung
                Console.WriteLine("Lenh goi chuong trinh cua ban bi sai");
                kieuCP = KieuCuPhap.Dang2;
            }

            // xu ly theo dang cu phap dong lenh
            switch (kieuCP)
            {
                case KieuCuPhap.Dang1:
                    // khoi tao doi tuong quan ly phong
                    try
                    {
                        // Tao doi tuong va load du lieu tu file data.txt
                        quanlyPhong = new QuanLyPhong();
                        XuLyTapTin.DocDuLieu(QuanLyPhong.DuongDanMacDinh, quanlyPhong);
                    }
                    catch (FileNotFoundException fx)
                    {
                        Console.WriteLine("LOI: Khong tim thay tap tin du lieu");
                        Console.ReadLine();
                    }
                    catch (XuLyTapTinException ex)
                    {
                        Console.WriteLine("LOI: {0}", ex.Message);
                        Console.ReadLine();
                    }
                    finally
                    {
                        quanlyPhong.CoThayDoi = false;
                    }

                    // Hien thi Menu
                    XuLyMenu();
                    break;
                case KieuCuPhap.Dang2:
                    InHuongDanSuDung(); // In ra huong dan su dung
                    Console.ReadKey(); // Cho nguoi sd doc huong dan va bam enter
                    Environment.Exit(0); // roi thoat khoi chuong trinh
                    break;
            }
        }

        // Ham thuc hien in ra huong dan su dung cho chuong trinh
        static void InHuongDanSuDung()
        {
            Console.WriteLine();
            Console.WriteLine("CHUONG TRINH QUAN LY PHONG HOC");
            Console.WriteLine("Cu phap cau lenh theo 1 trong 2 dang sau :");
            Console.WriteLine("QuanLyPhong.exe"); // Dang 1
            Console.WriteLine("QuanLyPhong.exe -h"); // Dang 2
            Console.WriteLine("Chuong trinh luu du lieu tu dong vao file data.txt.");
            Console.WriteLine();
        }
        #endregion

        #region Xu ly menu
        // In menu cua chuong trinh
        static void InMenu()
        {
            Console.Clear();
            Console.Write("CHUONG TRINH QUAN LY PHONG HOC");
            Console.WriteLine();
            Console.WriteLine("1 - Them phong hoc");
            Console.WriteLine("2 - Xoa phong");
            Console.WriteLine("3 - Sua thong tin phong");
            Console.WriteLine("4 - Danh sach phong");
            Console.WriteLine("5 - Tim phong trong");
            Console.WriteLine();
            Console.WriteLine("6 - Them don muon phong");
            Console.WriteLine("7 - Xoa don muon phong");
            Console.WriteLine("8 - Sua thong tin don muon phong");
            Console.WriteLine("9 - Danh sach don muon phong");
            Console.WriteLine();
            Console.WriteLine("10 - Thong ke su dung phong theo thoi gian");
            Console.WriteLine("11 - Thong ke su dung phong theo nguoi muon");
            Console.WriteLine("12 - Thong ke su dung phong theo thang");
            Console.WriteLine("13 - Tinh tien muon phong");
            Console.WriteLine();
            Console.WriteLine("14 - Thoat");
            Console.WriteLine("Hay chon so tu 1 den 14 phu hop voi chuc nang tuong ung.");
            Console.Write("Ban chon so : ");
        }

        // ham xu ly menu
        static void XuLyMenu()
        {
            int menu;
            bool kt = true;
            while (kt)
            {
                InMenu(); // in menu ra man hinh
                menu = XulyChonMenu(1, 14); // cho nguoi su dung chon menu

                switch (menu)
                {
                    case 1: // Them phong hoc
                        ThemPhongMoi();
                        break;
                    case 2: // Xoa phong
                        XoaPhong();
                        break;
                    case 3: // Sua thong tin phong
                        SuaTTPhong();
                        break;
                    case 4: // Danh sach phong
                        InDSPhong();
                        break;
                    case 5: // Tim phong trong
                        TimPhongTrong();
                        break;
                    case 6: // Them don muon phong
                        ThemDonMuonMoi();
                        break;
                    case 7: // Xoa don muon phong
                        XoaDonDK();
                        break;
                    case 8: // Sua thong tin don muon phong
                        SuaDonDK();
                        break;
                    case 9: // danh sach don muon phong
                        InDSDonMuonPhong();
                        break;
                    case 10: // thong ke su dung phong theo thoi gian
                        InTheoThoiGian();
                        break;
                    case 11: // thong ke su dung phong theo nguoi muon
                        InTheoNgMuonVaThang();
                        break;
                    case 12: // thong ke su dung phong theo thang
                        ThongKeTanSuat();
                        break;
                    case 13: // tinh tien muon phong
                        TinhTienMuonPhong();
                        break;
                    case 14: // thoat khoi chuong trinh
                        if (XuLyCauHoiYesNo("Ban co chac muon thoat chuong trinh ? (Y/N): "))
                        {
                            // neu muon thoat thi luu du lieu xuong file, neu da co thay doi trong du lieu
                            if (quanlyPhong.CoThayDoi)
                            {
                                try
                                {
                                    XuLyTapTin.LuuDuLieu(QuanLyPhong.DuongDanMacDinh, quanlyPhong);
                                    Console.WriteLine("\nDa luu thay doi trong co so du lieu vao tap tin data.txt");
                                }
                                catch (XuLyTapTinException ex)
                                {
                                    Console.WriteLine("LOI: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                            }
                            // roi moi thoat
                            Console.WriteLine("Cam on ban da su dung chuong trinh !");
                            Console.ReadKey();
                            kt = false;
                        }
                        break;
                }
            }
        }

        // Ham in menu cac loai phong, loai giao dich
        // chuoiLoai : la chuoi ky tu ten cac loai phong hoac loai giao dich
        static void InMenuCacLoai(string tieude, string chuoiLoai)
        {
            Console.Write("He thong ho tro cac {0} sau : ", tieude);
            Console.WriteLine(chuoiLoai);
        }
        #endregion

        #region Them phong hoc
        // Ham cho phep nhap vao cac phong hoc moi
        // viec nhap chi dung lai khi nguoi su dung muon
        static void ThemPhongMoi()
        {
            Console.Clear();
            Console.WriteLine("THEM PHONG MOI");

            do
            {
                try
                {
                    ThemMotPhongMoi(); // nhap thong tin cho mot phong moi
                }
                catch (CtrlZException ex)
                {
                    break;
                }
                // hoi nguoi su dung co muon tiep tuc khong
            } while (XuLyCauHoiYesNo("\nBan co muon nhap them phong khac khong ? (Y/N) : "));
        }

        // Ham xu ly nhap thong tin cho mot phong moi
        static void ThemMotPhongMoi()
        {
            Console.WriteLine("\nMoi ban nhap thong tin phong moi :");

            string ma = XuLyNhapMaPhong(); // nhap ma phong
            QuanLyPhong.LoaiPhong loaiPhong = XulyNhapLoaiPhong(); // nhap loai phong
            int succhua = XulyNhapSucChua(); // nhap suc chua

            // them phong moi vao vi tri thich hop
            // sap xep theo ma phong
            quanlyPhong.ThemPhongVoiViTriThichHop(ma, loaiPhong, succhua);
        }
        #endregion

        #region Them don muon phong
        // Ham cho phep nhap vao cac don muon phong moi
        // viec nhap chi dung lai khi nguoi su dung muon
        static void ThemDonMuonMoi()
        {
            Console.Clear();

            // neu chua co phong nao tong co so du lieu thi bat phai nhap
            if (LaKhongCoPhong("de muon"))
                return;

            Console.WriteLine("THEM DON MUON PHONG MOI");

            do
            {
                try
                {
                    ThemMotDonMuonMoi(); // nhap thong tin cho mot don muon phong moi
                }
                catch (CtrlZException ex)
                {
                    break;
                }
                // hoi nguoi su dung co muon tiep tuc khong
            } while (XuLyCauHoiYesNo("\nBan co muon nhap them don muon phong khac khong ? (Y/N) : "));
        }

        // Ham xu ly nhap thong tin cho mot don muon phong moi
        static void ThemMotDonMuonMoi()
        {
            Console.WriteLine("\nMoi ban nhap thong tin don muon phong moi :");

            Console.Write("Ten nguoi muon: ");
            string tenNgMuon = Console.ReadLine(); // nhap ten
            if (tenNgMuon == null) throw new CtrlZException(); // neu bam Ctrl+Z thi nem ra ngoai le
            Console.Write("Don vi cong tac: ");
            string dvCongtac = Console.ReadLine(); // nhap don vi cong tac
            if (dvCongtac == null) throw new CtrlZException(); // neu bam Ctrl+Z thi nem ra ngoai le

            string tieude = string.Format("Nha truong co {0} phong", quanlyPhong.SoPhong);
            InMenuCacLoai(tieude, quanlyPhong.LayDSMaPhong()); // in danh sach phong
            string maphong = XuLyNhapMaPhong(""); // nhap ma phong
            NgayThang ngayMuon = XuLyNhapNgayMuon(); // nhap ngay muon

            // lay ra danh sach cac tiet chua duoc muon cua phong trong ngay muon do
            List<int> tiettrong = quanlyPhong.LayDSTietTrong(maphong, ngayMuon);
            if (tiettrong.Count == 0) // neu danh sach tren rong
                Console.WriteLine("Phong {0} da duoc muon ca ngay {1}", maphong, ngayMuon);
            else
            {
                Console.Write("Trong ngay {0} tai phong {1}, ban co the muon cac tiet sau: ", ngayMuon, maphong);
                Console.WriteLine(LayChuoiSoTuList(tiettrong)); // in danh sach tren
                Console.WriteLine();

                Console.WriteLine("Moi ban chon cac tiet muon:");
                int tietBDMuon = XuLyNhapTiet("bat dau", 0, tiettrong); // nhap tiet bat dau muon
                // dua tren tiet bat dau muon, loai bo cac tiet khong the chon
                tiettrong = QuanLyPhong.LayDSTietTrong(tiettrong, tietBDMuon);

                if (tiettrong.Count == 0) // sau khi loc xong, neu danh sach tiet trong la rong
                    Console.WriteLine("Ban nen bat dau lai tu dau"); // thi khong the chon tiet ket thuc muon
                else
                {
                    Console.Write("Ban chi con co the chon cac tiet sau: ");
                    Console.WriteLine(LayChuoiSoTuList(tiettrong)); // in lai danh sach moi
                    int tietKTMuon = XuLyNhapTiet("ket thuc", tietBDMuon, tiettrong); // nhap tiet ket thuc muon
                    // them don muon phong moi vao vi tri thich hop
                    // duoc sap theo thu tu cua NgayMuon, TietBDMuon
                    quanlyPhong.ThemDonVoiViTriThichHop(tenNgMuon, dvCongtac, maphong, ngayMuon, tietBDMuon, tietKTMuon);
                }
            }
        }

        // Ham chuyen doi tu List<int> thanh chuoi ky tu
        // moi thanh phan duoc cach nhau boi dau phay
        static string LayChuoiSoTuList(List<int> list)
        {
            string temp = "";
            foreach (int i in list)
                temp += i + ", ";
            temp = temp.Remove(temp.Length - 2);
            return temp;
        }
        #endregion

        #region Xu ly nhap du lieu
        // Ham xu ly nhap ma phong
        static string XuLyNhapMaPhong()
        {
            Console.Write("Ma phong: ");
            string ma;
            do
            {
                ma = Console.ReadLine(); // nhap ma phong
                if (ma == null) throw new CtrlZException();
                if (!quanlyPhong.LaCoMaPhong(ma)) // neu do la ma moi thi thoat
                    break;
                // con khong thi yeu cau nhap lai
                Console.Write("Phong ban nhap da co trong CSDL, moi ban nhap lai hoac bam Ctrl+Z de thoat : ");
            } while (true);
            return ma;
        }

        // Ham xu ly nhap ma phong
        // ham nay duoc dung boi chuc nang sua thong tin va xoa
        static string XuLyNhapMaPhong(string mucdich)
        {
            Console.Write("Nhap ma phong {0} : ", mucdich);
            string ma;
            do
            {
                ma = Console.ReadLine();
                if (ma == null) throw new CtrlZException();
                if (quanlyPhong.LaCoMaPhong(ma)) // neu nhap dung ma
                    break;
                // neu chua dung thi yeu cau nhap lai
                Console.WriteLine("Phong ban nhap khong ton tai.");
                Console.Write("Moi ban nhap lai hoac bam Ctrl+Z de thoat : ");
            } while (true);
            return ma;
        }

        // Ham xu ly nhap ma don dang ky muon phong
        static string XulyNhapMaDon(string mucdich)
        {
            Console.Write("Nhap ma don muon phong {0} : ", mucdich);
            string ma;
            do
            {
                ma = Console.ReadLine();
                if (ma == null) throw new CtrlZException();
                if (quanlyPhong.LaCoMaDon(ma)) // neu nhap dung
                    break;
                // neu nhap sai thi yeu cau nhap lai
                Console.WriteLine("Ma don dang ky ban nhap khong ton tai.");
                Console.Write("Moi ban nhap lai hoac bam Ctrl+Z de thoat : ");
            } while (true);
            return ma;
        }

        // Ham xu ly nhap loai phong
        static QuanLyPhong.LoaiPhong XulyNhapLoaiPhong()
        {
            QuanLyPhong.LoaiPhong loaiPhong = QuanLyPhong.LoaiPhong.PhongHoc;

            // in menu cac loai phong
            InMenuCacLoai("loai phong", QuanLyPhong.ChuoiLoaiPhong);
            Console.Write("Loai phong:");
            string temp;
            do
            {
                temp = Console.ReadLine();
                if (temp == null) throw new CtrlZException();
                if (Enum.TryParse(temp, out loaiPhong)) // neu nhap dung
                    break;
                // neu nhap khong dung thi yeu cau nhap lai
                Console.Write("Ban nhap khong dung, moi ban nhap lai hoac bam Ctrl+Z de thoat: ");
            } while (true);
            return loaiPhong;
        }

        // Ham xu ly nhap suc chua
        static int XulyNhapSucChua()
        {
            int succhua;
            Console.Write("Suc chua: ");
            string temp;
            do
            {
                temp = Console.ReadLine();
                if (temp == null) throw new CtrlZException();
                if (int.TryParse(temp, out succhua)) // neu nhap dung
                    break;
                // neu nhap sai thi yeu cau nhap lai
                Console.Write("Ban phai nhap mot so. Moi ban nhap lai hoac bam Ctrl+Z de thoat: ");
            } while (true);
            return succhua;
        }

        // Ham xu ly nhap ngay muon
        static NgayThang XuLyNhapNgayMuon()
        {
            NgayThang ngayMuon = null;

            Console.Write("Ngay thang (dd/mm/yyyy) : "); // Yeu cau nhap ngay thang dung dang
            string ngaythang;
            do
            {
                ngaythang = Console.ReadLine();
                if (ngaythang == null) throw new CtrlZException();
                if (NgayThang.TryParse(ngaythang, out ngayMuon)) // ngay thang nhap dung
                    break;
                // neu sai thi yeu cau nhap lai
                Console.WriteLine("Ngay ban nhap khong dung.");
                Console.Write("De nghi ban nhap lai dung dang dd/mm/yyyy hoac bam Ctrl+Z de thoat: ");
            } while (true);
            return ngayMuon;
        }

        // Ham xu ly nhap thang va nam
        static NgayThang XuLyNhapThangNam()
        {
            NgayThang ngay;
            int thang, nam;
            string temp;
            Console.Write("Nhap thang : ");
            do
            {
                temp = Console.ReadLine();
                if (temp == null) throw new CtrlZException();
                // neu gia tri nhap vao khong phai so hoac nho hon, bang 0 hoac lon hon 12 
                if (int.TryParse(temp, out thang) && thang > 0 && thang <= 12)
                    break;
                Console.Write("Ban nhap sai, moi ban nhap lai : "); // yeu cau nhap lai
            }while (true);

            Console.Write("Nhap nam : ");
            do
            {
                temp = Console.ReadLine();
                if (temp == null) throw new CtrlZException();
                // neu gia tri nhap vao khong phai so hoac nho hon 0
                if (int.TryParse(temp, out nam) && nam > 0)
                    break;
                Console.Write("Ban nhap sai, moi ban nhap lai : "); // yeu cau nhap lai
            } while (true);

            ngay = new NgayThang(1, thang, nam);
            return ngay;
        }

        // Ham xu ly nhap tiet
        static int XuLyNhapTiet(string mess, int tietbd, List<int> tiettrong)
        {
            int tiet;
            Console.Write("Tiet {0} : ", mess);
            // tiet nhap vao phai nam trong doan tu 1-10 (1 ngay hoc co 10 tiet)
            // va phai lon hon tietbd
            string input;
            do
            {
                input = Console.ReadLine();
                if (input == null) throw new CtrlZException();
                if (int.TryParse(input, out tiet)
                    && tiet > tietbd && (tiettrong.IndexOf(tiet) > -1)) // neu nhap dung
                    break;
                // neu nhap sai thi nhap lai
                string thongbao = mess == "" ? "" : string.Format(" va phai lon hon {0}", tietbd);
                Console.WriteLine("Tiet ban nhap la sai (phai trong cac so {0}{1}).", LayChuoiSoTuList(tiettrong), thongbao);
                Console.Write("Moi ban nhap lai : ");
            } while (true);
            return tiet;
        }
        #endregion

        #region Tim phong trong
        // Ham tim phong trong
        static void TimPhongTrong()
        {
            Console.Clear();
            Console.WriteLine("TIM PHONG TRONG");

            do
            {
                // nhap ngay thang nam
                Console.WriteLine("Nhap ngay muon tim phong trong.");
                NgayThang ngay = XuLyNhapNgayMuon();
                // nhap dieu kien loai phong
                Console.WriteLine("Nhap loai phong muon tim.");
                QuanLyPhong.LoaiPhong loaiphong = XulyNhapLoaiPhong();
                // nhap dieu kien ve so luong
                Console.WriteLine("Nhap suc chua toi thieu.");
                int succhua = XulyNhapSucChua();

                // lay danh sach ma phong trong
                List<string> listPhong = quanlyPhong.TimPhongTrong(ngay, loaiphong, succhua);
                if (listPhong.Count == 0) // neu khong co phong nao trong
                    Console.WriteLine("Khong co phong nao trong trong ngay {0} dung voi yeu cau.", ngay);
                else
                {// in danh sach phong trong
                    Console.WriteLine("Co {0} phong trong trong ngay {1} dung voi yeu cau",
                                      listPhong.Count, ngay);
                    foreach (string maphong in listPhong)
                        Console.WriteLine(maphong);
                }
            } while (XuLyCauHoiYesNo("\nBan co muon tiep tuc tim phong trong khong ? (Y/N) : "));
        }
        #endregion

        #region Sua thong tin phong
        // Ham xu ly sua thong tin cac phong
        static void SuaTTPhong()
        {
            Console.Clear();
            if (LaKhongCoPhong("de sua thong tin.")) // chua co du lieu phong
                return;

            Console.WriteLine("SUA THONG TIN PHONG");
            Console.WriteLine("\nBan muon sua thong tin cho phong nao ?");
            do
            {
                try
                {
                    // nhap ma phong muon sua
                    string ma = XuLyNhapMaPhong("muon sua");

                    Console.WriteLine("Phong {0} ban muon sua co thong tin nhu sau :", ma);
                    Console.WriteLine(quanlyPhong.LayTTMotPhong(ma)); // in lai thong tin hien tai cua phong muon sua

                    Console.WriteLine("\nMoi ban nhap thong tin moi cho phong tren.");
                    SuaMotPhong(ma); // thuc hien nhap thong tin moi cho phong
                }
                catch (CtrlZException ex)
                {
                    break;
                }
            } while (XuLyCauHoiYesNo("\nBan co tiep tuc sua thong tin cho phong khac? (Y/N):"));
        }

        // Ham cho nhap thong tin moi vao cho phong muon sua thong tin
        static void SuaMotPhong(string maphong)
        {
            QuanLyPhong.LoaiPhong loaiPhong = XulyNhapLoaiPhong(); // nhap loai phong moi
            int succhua = XulyNhapSucChua(); // nhap suc chua moi

            quanlyPhong.SuaTTMotPhong(maphong, loaiPhong, succhua);
        }
        #endregion

        #region Sua Don Muon Phong
        // Ham xu ly sua thong tin cac don muon phong
        static void SuaDonDK()
        {
            Console.Clear();
            if (LaKhongCoDon("de sua thong tin.")) // chua co du lieu don muon phong
                return;

            Console.WriteLine("\nSUA THONG TIN DON MUON PHONG");
            Console.WriteLine("\nBan muon sua thong tin cho don muon phong nao ?");
            do
            {
                try
                {
                    // nhap ma don muon phong muon sua
                    string ma = XulyNhapMaDon("de sua");

                    Console.WriteLine("Don muon phong voi ma {0} co thong tin nhu sau :", ma);
                    Console.WriteLine(quanlyPhong.LayTTMotDonMuonPhong(ma)); // in lai thong tin hien tai cua don muon sua

                    Console.WriteLine("\nMoi ban nhap thong tin moi cho don muon phong tren.");
                    SuaMotDonMP(ma); // thuc hien nhap thong tin moi cho don muon phong
                }
                catch (CtrlZException ex)
                {
                    break;
                }
            } while (XuLyCauHoiYesNo("\nBan co tiep tuc sua thong tin cho don muon phong khac? (Y/N):"));
        }

        // Ham cho nhap thong tin moi vao cho don muon phong muon sua thong tin
        static void SuaMotDonMP(string madon)
        {
            Console.Write("Ten nguoi muon: ");
            string tenNgMuon = Console.ReadLine(); // nhap ten moi
            if (tenNgMuon == null) throw new CtrlZException();
            Console.Write("Don vi cong tac: ");
            string dvCongtac = Console.ReadLine(); // nhap don vi cong tac moi
            if (dvCongtac == null) throw new CtrlZException();

            string tieude = string.Format("Nha truong co {0} phong", quanlyPhong.SoPhong);
            InMenuCacLoai(tieude, quanlyPhong.LayDSMaPhong()); // in danh sach phong
            string maphong = XuLyNhapMaPhong(""); // chon ma phong moi
            NgayThang ngayMuon = XuLyNhapNgayMuon(); // nhap ngay moi

            // lay ra danh sach cac tiet chua duoc muon
            List<int> tiettrong = quanlyPhong.LayDSTietTrong(maphong, ngayMuon);
            if (tiettrong.Count == 0) // neu khong con tiet nao trong
                Console.WriteLine("Phong {0} da duoc muon ca ngay {1}", maphong, ngayMuon);
            else
            { // neu con trong
                Console.Write("Trong ngay {0} tai phong {1}, ban co the muon cac tiet sau: ", ngayMuon, maphong);
                Console.WriteLine(LayChuoiSoTuList(tiettrong)); // in ra ca tiet trong
                Console.WriteLine();

                Console.WriteLine("Moi ban chon cac tiet muon:");
                int tietBDMuon = XuLyNhapTiet("bat dau", 0, tiettrong); // nhap tiet bat dau muon
                tiettrong = QuanLyPhong.LayDSTietTrong(tiettrong, tietBDMuon); // loc lai danh sach tiet co the muon

                if (tiettrong.Count == 0) // neu sau khi loc khong con tiet nao
                    Console.WriteLine("Ban nen bat dau lai tu dau");
                else
                {
                    Console.Write("Ban chi con co the chon cac tiet sau: ");
                    Console.WriteLine(LayChuoiSoTuList(tiettrong)); // in lai danh sach tiet trong sau khi da loc
                    int tietKTMuon = XuLyNhapTiet("ket thuc", tietBDMuon, tiettrong); // nhap tiet ket thuc muon

                    quanlyPhong.SuaTTMotDonMuonPhong(madon, tenNgMuon, dvCongtac, maphong, ngayMuon, tietBDMuon, tietKTMuon);
                }
            }
        }
        #endregion

        #region Xoa phong
        // Ham xu ly xoa phong
        static void XoaPhong()
        {
            Console.Clear();
            if (LaKhongCoPhong("de xoa.")) // chua co du lieu phong
                return;

            Console.WriteLine("XOA PHONG");
            Console.WriteLine("\nBan can xoa phong nao ?");
            do
            {
                try
                {
                    // xu ly nhap ma phong
                    string ma = XuLyNhapMaPhong("de xoa");
                    Console.WriteLine("Phong {0} ban muon xoa co thong tin nhu sau :", ma);
                    // in thong tin hien co cua phong muon xoa
                    Console.WriteLine(quanlyPhong.LayTTMotPhong(ma));

                    // xac dinh lai y muon xoa don dang ky cua nguoi dung
                    if (XuLyCauHoiYesNo("\nBan co that su muon xoa phong nay ? (Y/N):"))
                    {
                        quanlyPhong.XoaPhong(ma); // xoa phong khoi danh sach
                        Console.WriteLine("Phong {0} da duoc xoa.", ma);
                        Console.ReadKey();
                    }
                }
                catch (CtrlZException ex)
                {
                    break;
                }
            } while (XuLyCauHoiYesNo("\nBan co tiep tuc xoa nhung phong khac? (Y/N):"));
        }
        #endregion

        #region Xoa don muon phong
        // Ham xu ly xoa don muon phong
        static void XoaDonDK()
        {
            Console.Clear();
            if (LaKhongCoDon("de xoa."))
                return;

            Console.WriteLine("\nXOA DON MUON PHONG");
            Console.WriteLine("\nBan can xoa don muon phong nao ?");
            do
            {
                try
                {
                    // xu ly nhap ma don muon phong
                    string ma = XulyNhapMaDon("de xoa");
                    Console.WriteLine("Don muon phong voi ma {0} co thong tin nhu sau :", ma);
                    // in thong tin hien co cua don muon phong muon xoa
                    Console.WriteLine(quanlyPhong.LayTTMotDonMuonPhong(ma));

                    // xac dinh lai y muon xoa don muon phong cua nguoi dung
                    if (XuLyCauHoiYesNo("\nBan co that su muon xoa don nay ? (Y/N):"))
                    {
                        quanlyPhong.XoaDonMuonPhong(ma); // xoa don muon phong khoi danh sach
                        Console.WriteLine("Don muon phong voi ma {0} da duoc xoa.", ma);
                        Console.ReadKey();
                    }
                }
                catch (CtrlZException ex)
                {
                    break;
                }
            } while (XuLyCauHoiYesNo("\nBan co tiep tuc xoa nhung muc thu chi khac? (Y/N):"));
        }
        #endregion

        #region In an Phong
        // Ham kiem tra danh sach rong va in thong bao
        static bool LaKhongCoPhong(string mucdich)
        {
            if (quanlyPhong.LaKhongCoPhong)
            {
                Console.WriteLine("Co so du lieu chua co phong nao {0}.", mucdich);
                Console.ReadKey();
                return true;
            }
            return false;
        }

        // Ham xu ly in thong tin cua tat ca cac phong
        static void InDSPhong()
        {
            Console.Clear();
            if (LaKhongCoPhong("de in danh sach phong"))
                return;

            Console.WriteLine("DANH SACH CAC PHONG");

            Console.WriteLine("Co so du lieu co tat ca {0} phong.", quanlyPhong.SoPhong);
            for (int i = 0; i < (quanlyPhong.SoPhong / 10 + 1); i++)
            {
                Console.Write(quanlyPhong.LayThongTinMuoiPhong(i));
                Console.ReadKey(); // bam phim bat ky de in 10 don tiep theo
            }
        }
        #endregion

        #region In an Don muon phong
        // Ham kiem tra danh sach rong va in thong bao
        static bool LaKhongCoDon(string mucdich)
        {
            if (quanlyPhong.LaKhongCoDon)
            {
                Console.WriteLine("Co so du lieu chua co don muon phong nao {0}.", mucdich);
                Console.ReadKey();
                return true;
            }
            return false;
        }

        // Ham xu ly in thong tin cua tat ca cac don muon phong
        static void InDSDonMuonPhong()
        {
            Console.Clear();
            if (LaKhongCoDon("de in danh sach don muon phong"))
                return;

            Console.WriteLine("DANH SACH CAC DON MUON PHONG");

            Console.WriteLine("Co so du lieu co tat ca {0} don muon phong.", quanlyPhong.SoDonMuonPhong);
            for (int i = 0; i < (quanlyPhong.SoDonMuonPhong / 10 + 1); i++)
            {
                Console.Write(quanlyPhong.LayThongTinMuoiDonMuonPhong(i));
                Console.ReadKey(); // bam phim bat ky de in 10 don tiep theo
            }
        }
        #endregion

        #region Thong ke
        // Ham xu ly in danh sach cac don muon phong trong khoang thoi gian
        static void InTheoThoiGian()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("DANH SACH DON MUON PHONG THEO THOI GIAN");

                Console.WriteLine("Moi ban nhap ngay bat dau.");
                NgayThang ngayBD = XuLyNhapNgayMuon(); // nhap ngay bat dau muon thong ke
                Console.WriteLine("Moi ban nhap ngay ket thuc.");
                NgayThang ngayKT = XuLyNhapNgayMuon(); // nhap ngay ket thuc thong ke

                List<string> listDon = quanlyPhong.LayTTCacDonMPThuocThoiGian(ngayBD, ngayKT);

                if (listDon.Count == 0)
                    Console.WriteLine("Khong co don nao thuoc khoang thoi gian tren!");
                else
                {
                    Console.WriteLine("Co {2} don muon phong tu ngay {0} den ngay {1} :",
                                   ngayBD, ngayKT, listDon.Count);

                    int i = 0;
                    foreach (string don in listDon)
                    {
                        Console.WriteLine("{0}.", ++i);
                        Console.WriteLine(don);

                        if (i % 10 == 0) // in 10 don roi dung lai
                            Console.ReadKey(); // bam phim bat ky de in 10 don tiep theo
                    }
                }
            } while (XuLyCauHoiYesNo("\nBan co muon thong ke tiep khong ? (Y/N) : "));
        }

        // Ham xu ly in danh sach cac don muon phong theo nguoi muon va thang
        static void InTheoNgMuonVaThang()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("DANH SACH DON MUON PHONG THEO NGUOI MUON VA THANG");

                Console.Write("Nhap ten nguoi muon : ");
                string ten = Console.ReadLine();
                NgayThang thangnam = XuLyNhapThangNam();

                List<string> listDon = quanlyPhong.LayTTCacDonMPTheoNguoiMuon(ten, thangnam);

                if (listDon.Count == 0)
                    Console.WriteLine("{0} khong muon phong nao trong thang {1} nam {2}.", ten, thangnam.Thang, thangnam.Nam);
                else
                {
                    Console.WriteLine("Co {0} don muon phong do {1} muon trong thang {2} nam {3} :",
                                      listDon.Count, ten, thangnam.Thang, thangnam.Nam);

                    int i = 0;
                    foreach (string don in listDon)
                    {
                        Console.WriteLine("{0}.", ++i);
                        Console.WriteLine(don);

                        if (i % 10 == 0) // in 10 don roi dung lai
                            Console.ReadKey(); // bam phim bat ky de in 10 don tiep theo
                    }
                }
            } while (XuLyCauHoiYesNo("\nBan co muon thong ke tiep khong ? (Y/N) : "));
        }

        // Ham thong ke tan suat su dung theo thang
        static void ThongKeTanSuat()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("THONG KE SU DUNG PHONG THEO THANG");
                NgayThang thangnam = XuLyNhapThangNam(); // nhap thang can thong ke

                // lay danh sach ma phong duoc su dung trong thang can thong ke
                List<string> dsMaPhong = quanlyPhong.LayDSMaPhongDuocSDTrongThang(thangnam);
                if (dsMaPhong.Count == 0) // neu khong co ma phong nao
                    Console.WriteLine("Khong co phong nao duoc su dung trong thang {0} nam {1}.",
                                      thangnam.Thang, thangnam.Nam);
                else
                {
                    // neu co, thi lay tiep danh sach so lan su dung cua cac phong do trong thang thong ke
                    List<int> dsSoLanSD = quanlyPhong.TinhSoLanSD(dsMaPhong, thangnam);
                    // tinh so tiet toi da co the su dung phong may trong thang
                    int sotiet = quanlyPhong.TinhTongSoTietSDTrongThang(thangnam);

                    Console.WriteLine("Thong ke thang {0} nam {1}:", thangnam.Thang, thangnam.Nam);
                    for (int i = 0; i < dsMaPhong.Count; i++)
                    {
                        string maphong = dsMaPhong[i];
                        int solan = dsSoLanSD[i];
                        Console.WriteLine("Phong {0} : {1} tiet, {2:f2}%",
                                          maphong, solan, (double)solan / sotiet * 100); // in ket qua thong ke
                    }
                }
            } while (XuLyCauHoiYesNo("\nBan co muon thong ke tiep khong ? (Y/N) : "));
        }
        #endregion

        #region Tinh tien muon phong
        // Ham tinh tien muon phong theo don muon phong
        static void TinhTienMuonPhong()
        {
            Console.Clear();
            Console.WriteLine("TINH TIEN MUON PHONG");

            do
            {
                // nhap ma don muon phong muon thanh toan
                string madon = XulyNhapMaDon("muon thanh toan");

                Console.WriteLine("So tien phai thanh toan: {0:f2}", quanlyPhong.TinhTienMuonPhong(madon));

            } while (XuLyCauHoiYesNo("\nBan co muon tinh tien cho don muon phong khac khong ? (Y/N) : "));
        }
        #endregion

        #region CHUNG
        // Ham xu ly in cau hoi yes no
        static bool XuLyCauHoiYesNo(string cauhoi)
        {
            Console.Write(cauhoi); // in cau hoi
            ConsoleKeyInfo c = Console.ReadKey(); // nguoi su dung nhap vao lua chon
            Console.WriteLine();

            if (c.KeyChar == 'Y' || c.KeyChar == 'y')
                return true;
            return false;
        }

        // ham nay tra ra mot so tu 1 den 13
        // tuong ung voi cac so tren menu ma nguoi su dung nhap
        // hai tham so canduoi va cantren giup cho ham nay co the xu ly cho bat ky menu nao
        static int XulyChonMenu(int canduoi, int cantren)
        {
            int menu;
            // ham TryParse thuc hien chuyen doi tu chuoi sang so
            // neu co the chuyen duoc thi ham nay tra ra gia tri true
            // va ket qua o tham chieu menu
            while (!int.TryParse(Console.ReadLine(), out menu)
                   && ((menu < canduoi) || (menu > cantren)))
            {
                Console.WriteLine("Ban chon so nam ngoai pham vi cac so tren menu.");
                Console.WriteLine("Hay chon so tu {0} den {1} phu hop voi chuc nang tuong ung.", canduoi, cantren);
                Console.Write("Moi ban chon lai :");
            }
            return menu;
        }
        #endregion
    }
}
