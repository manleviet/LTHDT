using System;
using System.Collections.Generic;
using System.IO;

namespace QLTC_HDT
{
    class Program
    {
        #region Vung khai bao bien
        const string dataPath = "data.txt";

        // Kieu cu phap the hien cu phap dong lenh khi goi chuong trinh
        // Dang 1 : XuLySoLieu.exe
        // Dang 2 : XuLySoLieu.exe -h
        enum KieuCuPhap { Dang1, Dang2 };
        // bien kieuCP de nhan dang cu phap dong lenh
        static KieuCuPhap kieuCP;

        static DSTaiKhoan listTK; // Danh sach cac tai khoan
        static DSGiaoDich listGD; // Danh sach cac giao dich
        #endregion

        #region Main
        static void Main(string[] args)
        {
            // khoi tao danh sach tai khoan va giao dich
            listTK = new DSTaiKhoan();
            listGD = new DSGiaoDich();

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
                    // Load du lieu tu file
                    XuLyFile.LayDuLieuTuFile(dataPath, listTK, listGD);
                    // Hien thi Menu
                    XuLyMenu();
                    // Luu du lieu xuong file
                    if (!XuLyFile.LuuDuLieuRaFile(listTK, listGD, dataPath))
                    {
                        Console.WriteLine("LOI trong qua trinh luu du lieu ra file.");
                        Console.ReadLine();
                        File.Delete(dataPath); // neu luu bi loi thi xoa file
                    }
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
            Console.WriteLine("CHUONG TRINH QUAN LY THU CHI");
            Console.WriteLine("Cu phap cau lenh theo 1 trong 2 dang sau :");
            Console.WriteLine("qltc.exe"); // Dang 1
            Console.WriteLine("qltc.exe -h"); // Dang 2
            Console.WriteLine("Chuong trinh luu du lieu tu dong vao file data.txt.");
            Console.WriteLine();
        }
        #endregion

        #region Xu ly menu
        // In menu cua chuong trinh
        static void InMenu()
        {
            Console.WriteLine();
            Console.Write("CHUONG TRINH QUAN LY THU CHI");
            Console.WriteLine();
            Console.WriteLine("1 - Them tai khoan moi");
            Console.WriteLine("2 - Xoa tai khoan");
            Console.WriteLine("3 - Sua thong tin tai khoan");
            Console.WriteLine("4 - Danh sach tai khoan");

            Console.WriteLine("5 - Them giao dich");
            Console.WriteLine("6 - Xoa giao dich");
            Console.WriteLine("7 - Sua thong tin giao dich");
            Console.WriteLine("8 - Danh sach giao dich");

            Console.WriteLine("9 - Xem thong tin tong quat");
            Console.WriteLine("10 - Xem thong tin theo tai khoan");
            Console.WriteLine("11 - Xem thong tin theo thoi gian");
            Console.WriteLine("12 - Xem thong tin theo phan loai va thoi gian");

            Console.WriteLine("13 - Thoat");
            Console.WriteLine("Hay chon so tu 1 den 13 phu hop voi chuc nang tuong ung.");
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
                menu = XulyChonMenu(1, 13); // cho nguoi su dung chon menu

                switch (menu)
                {
                    case 1: // Them tai khoan moi
                        ThemTKMoi();
                        break;
                    case 2: // Xoa tai khoan
                        XoaTK();
                        break;
                    case 3: // Sua thong tin tai khoan
                        SuaThongTinTK();
                        break;
                    case 4: // Danh sach tai khoan
                        Console.WriteLine(listTK);
                        Console.ReadKey();
                        break;
                    case 5: // Them giao dich moi
                        ThemGDMoi();
                        break;
                    case 6: // Xoa giao dich
                        XoaGD();
                        break;
                    case 7: // Sua thong tin giao dich
                        SuaThongTinGD();
                        break;
                    case 8: // danh sach giao dich
                        Console.WriteLine(listGD);
                        Console.ReadKey();
                        break;
                    case 9: // in tat ca cac khoan thu chi
                        InTKTongQuat();
                        break;
                    case 10: // in thong ke theo tai khoan
                        InTKTheoTK();
                        break;
                    case 11: // in thong ke theo thoi gian
                        InTKThoiGian();
                        break;
                    case 12: // in thong ke theo thoi gian va phan loai
                        InTKTheoPhanLoai();
                        break;
                    case 13: // thoat khoi chuong trinh
                        if (XuLyCauHoiYesNo("Ban co chac muon thoat chuong trinh ? (Y/N): "))
                        {
                            Console.WriteLine("\nCam on ban da su dung chuong trinh !");
                            Console.ReadKey();
                            kt = false;
                        }
                        break;
                }
            }
        }

        // Ham in menu cac kieu tai khoan, loai giao dich
        // chuoiLoai : la chuoi ky tu ten cac kieu tai khoan hoac loai giao dich
        static void InMenuCacLoai(string tieude, string chuoiLoai)
        {
            Console.Write("He thong ho tro cac {0} sau : ", tieude);
            Console.WriteLine(chuoiLoai);
            Console.Write("Moi ban nhap : ");
        }
        #endregion

        #region THEM TAI KHOAN MOI
        // Ham cho phep nhap vao cac tai khoan moi
        // viec nhap chi dung lai khi nguoi su dung muon
        static void ThemTKMoi()
        {
            do
            {
                Console.WriteLine("\nMoi ban nhap thong tin tai khoan :");
                TaiKhoan tk = ThemMotTKMoi(); // nhap thong tin cho tung khoan thu chi
                listTK.ThemTK(tk); // add vao listTK
                listGD.ThemTK(tk); // add vao listGD de tao mot giao dich "mo tai khoan"
                // hoi nguoi su dung co muon tiep tuc khong
            } while (XuLyCauHoiYesNo("\nBan co muon nhap them tai khoan khac khong ? (Y/N) : "));
        }

        // Ham xu ly nhap thong tin cho tung tai khoan
        // Ham nay con duoc dung khi sua thong tin tai khoan
        static TaiKhoan ThemMotTKMoi()
        {
            Console.Write("Ten : ");
            string ten = Console.ReadLine(); // Nhap ten tai khoan
            TaiKhoan.KieuTaiKhoan kieuTK = XulyNhapKieuTK(); // Nhap kieu tai khoan
            ThoiGian ngaymo = XulyNhapNgayThangNam("Ngay thang"); // Nhap ngay tao
            double sodu = XulyNhapTien("So tien ban dau : "); // Nhap so tien ban dau

            return new TaiKhoan(ten, sodu, ngaymo, kieuTK, listGD); // tao doi tuong Taikhoan
        }

        // Ham xu ly nhap kieu tai khoan
        static TaiKhoan.KieuTaiKhoan XulyNhapKieuTK()
        {
            TaiKhoan.KieuTaiKhoan kieuTK = TaiKhoan.KieuTaiKhoan.TienMat;
            // cho phep nguoi su dung bo qua khong nhap kieu tai khoan
            if (!XuLyCauHoiYesNo("Ban co nhap kieu tai khoan khong ? (Y/N):"))
                return kieuTK;

            // in menu cac kieu tai khoan
            InMenuCacLoai("kieu tai khoan", TaiKhoan.ChuoiKieuTaiKhoan);
            while (!Enum.TryParse(Console.ReadLine(), out kieuTK))
            {
                Console.Write("Ban nhap khong dung, moi ban nhap lai : ");
            }

            return kieuTK;
        }

        // Ham xu ly nhap ngay thang
        static ThoiGian XulyNhapNgayThangNam(string message)
        {
            Console.Write("{0} (mm/dd/yyyy) : ", message); // Yeu cau nhap ngay thang dung dang
            ThoiGian ngay = Console.ReadLine(); // o day, co su dung ep kieu ngam dinh

            // kiem tra :
            while (ngay.LaNgayMacDinh) // ngay la ngay mac dinh khi chuoi ngay nhap vao la sai
            { // neu sai thi yeu cau nhap lai
                Console.WriteLine("Ngay ban nhap khong dung.");
                Console.Write("De nghi ban nhap lai theo dang mm/dd/yyyy : ");
                ngay = Console.ReadLine();
            }

            return ngay;
        }

        // Ham xu ly nhap tien
        static double XulyNhapTien(string message)
        {
            double sotien;
            Console.Write(message);
            // kiem tra du lieu nhap vao phai la so va lon hon 0
            while (!double.TryParse(Console.ReadLine(), out sotien)
                   || sotien < 0)
            { // khong phai thi nhap lai
                Console.WriteLine("So tien ban nhap la sai (Dung nhap so am).");
                Console.Write("Moi ban nhap lai : ");
            }
            return sotien;
        }
        #endregion

        #region XOA TAI KHOAN
        // Ham xu ly xoa tai khoan
        static void XoaTK()
        {
            // neu la rong thi bo qua
            if (listTK.LaRong)
            {
                Console.WriteLine("Co so du lieu khong co tai khoan nao de xoa.");
                return;
            }

            int index;
            string ten;
            do
            {
                // xu ly nhap ten tai khoan
                Console.Write("Moi nhap ten tai khoan can xoa : ");
                ten = Console.ReadLine();

                if ((index = listTK.LayViTriCuaTKCoTen(ten)) == -1) // neu ten tai khoan khong ton tai trong danh sach
                    Console.WriteLine("Tai khoan ban tim khong co trong du lieu.");
                else // nguoc lai
                {
                    TaiKhoan tk = listTK[index]; // lay tai khoan voi ten su dung Toan tu Chi muc
                    Console.WriteLine("Tai khoan voi ten {0} co thong tin nhu sau :", tk.Ten);
                    Console.WriteLine(tk);
                    Console.WriteLine("CHU Y : Khi xoa tai khoan cac giao dich kem theo no cung se bi xoa.");

                    // xac dinh lai y muon xoa tai khoan cua nguoi dung
                    if (XuLyCauHoiYesNo("\nBan co that su muon xoa tai khoan nay ? (Y/N):"))
                    {
                        // Xoa cac giao dich thuoc ve tai khoan do trong listGD
                        listGD.XoaGiaoDichCua(tk);
                        // Xoa tai khoan khoi listTK
                        listTK.XoaTKCoViTri(index);
                        Console.WriteLine("Tai khoan voi ten {0} da duoc xoa.", ten);
                        Console.ReadKey();
                    }
                }
            } while (XuLyCauHoiYesNo("\nBan co tiep tuc xoa nhung tai khoan khac? (Y/N):"));
        }
        #endregion

        #region SUA THONG TIN TAI KHOAN
        // Ham xu ly sua thong tin cac tai khoan
        static void SuaThongTinTK()
        {
            // neu danh sach rong thi bo qua
            if (listTK.LaRong)
            {
                Console.WriteLine("Co so du lieu khong co tai khoan nao de sua thong tin.");
                return;
            }

            int index;
            string ten;
            Console.WriteLine("\nBan muon sua thong tin cho tai khoan nao ?");
            do
            {
                // nhap ma khoan thu chi muon sua
                Console.Write("Moi nhap ten tai khoan can sua : ");
                ten = Console.ReadLine();

                if ((index = listTK.LayViTriCuaTKCoTen(ten)) == -1) // neu khong co
                    Console.WriteLine("Tai khoan ban nhap khong co trong du lieu.");
                else // nguoc lai
                {
                    TaiKhoan tk = listTK[index]; // lay tai khoan voi ten
                    Console.WriteLine("Tai khoan voi ten {0} co thong tin nhu sau :", tk.Ten);
                    Console.WriteLine(tk); // in lai thong tin hien tai cua tai khoan muon sua

                    Console.WriteLine("\nMoi ban nhap thong tin moi cho tai khoan tren.");
                    TaiKhoan tkNew = ThemMotTKMoi(); // thuc hien nhap thong tin moi cho khoan thu chi
                    listGD.SuaTTTK(listTK[index], tkNew); // sua thong tin cua giao dich Mo tai khoan
                    listTK[index] = tkNew;
                }
            } while (XuLyCauHoiYesNo("\nBan co tiep tuc sua thong tin tai khoan khac? (Y/N):"));
        }
        #endregion

        #region THEM GIAO DICH
        // Ham cho phep nhap vao cac giao dich moi
        // viec nhap chi dung lai khi nguoi su dung muon
        static void ThemGDMoi()
        {
            // Kiem tra co tai khoan nao khong
            if (listTK.LaRong)
            { // neu khong thi khong the thuc hien giao dich
                Console.WriteLine("Co so du lieu khong co tai khoan nao de thuc hien giao dich.");
                return;
            }

            do
            {
                Console.WriteLine("\nMoi ban nhap thong tin giao dich :");
                GiaoDich gd = ThemMotGDMoi(-1); // nhap thong tin cho tung giao dich
                listGD.ThemGD(gd);
                // hoi nguoi su dung co muon tiep tuc khong
            } while (XuLyCauHoiYesNo("\nBan co muon nhap them giao dich khac khong ? (Y/N) : "));
        }

        // Ham xu ly nhap thong tin cho tung giao dich
        // dung duoc cho ca xu ly sua thong tin giao dich
        // ma : = -1 - nhap mot giao dich moi
        //      != -1 - sua thong tin giao dich da co
        static GiaoDich ThemMotGDMoi(int ma)
        {
            if (ma == -1)
                ma = listGD.MaChoGiaoDichMoi; // lay ma giao dich tiep theo
            Console.WriteLine("Ma : {0}", ma);

            GiaoDich.KieuGiaoDich kGD = XulyNhapKieuGD(); // nhap kieu giao dich

            Console.Write("Chon tai khoan phat sinh giao dich - ");
            TaiKhoan tk = XulyChonTaiKhoan(); // chon tai khoan phat sinh giao dich

            TaiKhoan tkNhan = null; // neu la chuyen khoan thi co tai khoan nhan
            if (kGD == GiaoDich.KieuGiaoDich.ChuyenKhoan)
            {
                Console.Write("Chon tai khoan nhan - ");
                tkNhan = XulyChonTaiKhoan(); // chon tai khoan nhan
                // cho nay chua xu ly truong hop tkChuyen va tkNhan trung nhau
            }

            ThoiGian ngaygd = XulyNhapNgayThangNam("Ngay thang"); // nhap ngay giao dich
            double sotien = XulyNhapTien("So tien : "); // nhap so tien giao dich
            GiaoDich.LoaiGiaoDich lGD = XulyNhapLoaiGD(); // nhap loai giao dich
            Console.Write("Ghi chu : ");
            string ghichu = Console.ReadLine(); // nhap ghi chu

            GiaoDich gd = null;
            switch (kGD) // tao doi tuong phu thuoc vao kieu giao dich
            {
                case GiaoDich.KieuGiaoDich.Thu: // tao doi tuong thu
                    gd = new Thu(ma, ngaygd, sotien, tk, lGD, ghichu);
                    break;
                case GiaoDich.KieuGiaoDich.Chi: // tao doi tuong chi
                    gd = new Chi(ma, ngaygd, sotien, tk, lGD, ghichu);
                    break;
                case GiaoDich.KieuGiaoDich.ChuyenKhoan: // tao doi tuong chuyen khoan
                    gd = new ChuyenKhoan(ma, ngaygd, sotien, tk, tkNhan, lGD, ghichu);
                    break;
            }

            return gd;
        }

        // Ham xu ly nhap kieu giao dich
        static GiaoDich.KieuGiaoDich XulyNhapKieuGD()
        {
            GiaoDich.KieuGiaoDich kieuGD = GiaoDich.KieuGiaoDich.Thu;

            // in menu cac kieu giao dich
            InMenuCacLoai("kieu giao dich", GiaoDich.ChuoiKieuGiaoDich);
            while (!Enum.TryParse(Console.ReadLine(), out kieuGD))
            {
                Console.WriteLine("Ban nhap khong dung, moi ban nhap lai : ");
            }

            return kieuGD;
        }

        // Ham xu ly chon tai khoan
        static TaiKhoan XulyChonTaiKhoan()
        {
            // In danh sach cac tai khoan
            Console.WriteLine(listTK.LayDanhSachTenCacTaiKhoan() + ".");
            Console.Write("Moi ban chon : ");

            int index = -1;
            while ((index = listTK.LayViTriCuaTKCoTen(Console.ReadLine())) == -1)
            {
                Console.Write("Ban nhap ten tai khoan sai, moi ban nhap lai : ");
            }

            return listTK[index];
        }

        // Ham xu ly chon loai giao dich
        static GiaoDich.LoaiGiaoDich XulyNhapLoaiGD()
        {
            GiaoDich.LoaiGiaoDich loaiGD = GiaoDich.LoaiGiaoDich.Khac;

            // in menu cac loai giao dich
            InMenuCacLoai("loai giao dich", GiaoDich.ChuoiLoaiGiaoDich);
            while (!Enum.TryParse(Console.ReadLine(), out loaiGD))
            {
                Console.WriteLine("Ban nhap khong dung, moi ban nhap lai : ");
            }

            return loaiGD;
        }
        #endregion

        #region XOA GIAO DICH
        // Ham xu ly xoa giao dich
        static void XoaGD()
        {
            // neu la rong thi bo qua
            if (listGD.LaRong)
            {
                Console.WriteLine("Co so du lieu khong co giao dich nao de xoa.");
                return;
            }

            int ma;
            do
            {
                ma = XulyNhapMaGD("de xoa");
                GiaoDich gd = listGD.LayGDVoiMa(ma);

                if (gd == null) // neu ma giao dich khong ton tai trong danh sach
                    Console.WriteLine("Giao dich ban tim khong co trong du lieu.");
                else // nguoc lai
                {
                    Console.WriteLine("Giao dich voi ma {0} co thong tin nhu sau :", gd.Ma);
                    Console.WriteLine(gd);

                    // xac dinh lai y muon xoa giao dich cua nguoi dung
                    if (XuLyCauHoiYesNo("\nBan co that su muon xoa giao dich nay ? (Y/N):"))
                    {
                        // Xoa giao dich khoi danh sach giao dich
                        listGD.XoaGD(gd);

                        Console.WriteLine("Giao dich voi ma {0} da duoc xoa.", ma);
                        Console.ReadKey();
                    }
                }
            } while (XuLyCauHoiYesNo("\nBan co tiep tuc xoa nhung giao dich khac? (Y/N):"));
        }

        // xu ly nhap ma giao dich
        static int XulyNhapMaGD(string mucdich)
        {
            int ma;
            Console.Write("Nhap ma giao dich {0} : ", mucdich);
            while (!int.TryParse(Console.ReadLine(), out ma)
                  || ma < 0)
            {
                Console.WriteLine("Ma giao dich ban nhap khong dung.");
                Console.Write("Moi ban nhap lai :");
            }
            return ma;
        }
        #endregion

        #region SUA THONG TIN GIAO DICH
        // Ham xu ly sua thong tin cac giao dich
        static void SuaThongTinGD()
        {
            // neu danh sach rong thi bo qua
            if (listGD.LaRong)
            {
                Console.WriteLine("Co so du lieu khong co giao dich nao de sua thong tin.");
                return;
            }

            int ma;
            Console.WriteLine("\nBan muon sua thong tin cho giao dich nao ?");
            do
            {
                ma = XulyNhapMaGD("muon sua");
                GiaoDich gd = listGD.LayGDVoiMa(ma);

                if (gd == null) // neu khong co
                    Console.WriteLine("Giao dich ban nhap khong co trong du lieu.");
                else // nguoc lai
                {
                    Console.WriteLine("Giao dich voi ma {0} co thong tin nhu sau :", gd.Ma);
                    Console.WriteLine(gd); // in lai thong tin hien tai cua giao dich muon sua

                    Console.WriteLine("\nMoi ban nhap thong tin moi cho giao dich tren.");
                    GiaoDich gdMoi = ThemMotGDMoi(ma); // thuc hien nhap thong tin moi cho giao dich
                    listGD.XoaGD(gd); // xoa giao dich cu
                    listGD.ThemGD(gdMoi); // them giao dich moi
                }
            } while (XuLyCauHoiYesNo("\nBan co tiep tuc sua thong tin giao dich khac? (Y/N):"));
        }
        #endregion

        #region THONG KE
        // Ham xu ly chuc nang In thong ke tong quat
        static void InTKTongQuat()
        {
            // neu la rong thi bo qua
            if (listTK.LaRong)
            {
                Console.WriteLine("Co so du lieu khong co tai khoan nao de thong ke.");
                return;
            }

            double tongthu = 0;
            double tongchi = 0;
            double sodu = 0;
            foreach (TaiKhoan tk in listTK)
            {
                tongthu += listGD.LayTongThu(tk);
                tongchi += listGD.LayTongChi(tk);
                sodu += listGD.LaySoDuCuaTK(tk);
            }

            Console.WriteLine("Ban co {0} tai khoan voi so du hien tai {1}.", listTK.SoLuong, sodu);
            Console.WriteLine("Tong thu : {0}", tongthu);
            Console.WriteLine("Tong chi : {0}", tongchi);
            Console.ReadLine();
        }

        // Ham xu ly chuc nang In thong ke theo tai khoan
        static void InTKTheoTK()
        {
            // neu la rong thi bo qua
            if (listTK.LaRong)
            {
                Console.WriteLine("Co so du lieu khong co tai khoan nao de thong ke.");
                return;
            }

            int index;
            string ten;
            do
            {
                Console.WriteLine(listTK);
                // xu ly nhap ten tai khoan
                Console.Write("Moi nhap ten tai khoan can thong ke : ");
                ten = Console.ReadLine();

                if ((index = listTK.LayViTriCuaTKCoTen(ten)) == -1) // neu ten tai khoan khong ton tai trong danh sach
                    Console.WriteLine("Tai khoan ban tim khong co trong du lieu.");
                else // nguoc lai
                {
                    TaiKhoan tk = listTK[index]; // lay doi tuong tai khoan
                    Console.WriteLine("Tai khoan voi ten {0} co thong tin nhu sau :", tk.Ten);
                    Console.WriteLine(tk);
                    Console.WriteLine("So tien ban dau : {0}", tk.SoTienBanDau);
                    Console.WriteLine("Tong thu : {0}", listGD.LayTongThu(tk));
                    Console.WriteLine("Tong chi : {0}", listGD.LayTongChi(tk));

                    List<GiaoDich> list = listGD.LayCacGDCuaTK(tk); // lay ra danh sach cac giao dich cua tai khoan do
                    Console.WriteLine("\nCac giao dich :");
                    Console.WriteLine();
                    for (int i = 0; i < list.Count; i++)
                    {
                        Console.Write("{0}.", i + 1);
                        Console.Write(list[i]);
                        Console.WriteLine();
                        Console.ReadLine();
                    }
                }
            } while (XuLyCauHoiYesNo("\nBan co tiep tuc thong ke nhung tai khoan khac? (Y/N):"));
        }

        // Kieu enum cho viec nhap lieu cac khoang thoi gian thong ke
        enum KieuNhapThoiGian { Tuan, Thang, Nam, Khoang }
        // Ham xu ly chuc nang In thong ke theo thoi gian
        static void InTKThoiGian()
        {
            // neu la rong thi bo qua
            if (listGD.LaRong)
            {
                Console.WriteLine("Co so du lieu khong co giao dich nao de thong ke.");
                return;
            }

            KieuNhapThoiGian kieuNhapTG = XulyNhapKieuNhapTG(); // Chon kieu thong ke theo thoi gian
            double sodudauky = 0, tongthu = 0, tongchi = 0, soducuoiky = 0;
            List<GiaoDich> list = null;
            switch (kieuNhapTG)
            {
                case KieuNhapThoiGian.Tuan:
                    ThoiGian ngayDauTuan = XulyNhapNgayThangNam("Nhap ngay dau tuan");

                    list = listGD.ThongKeTheoThoiGian(ngayDauTuan, ngayDauTuan + 6,
                                                      out sodudauky,
                                                      out tongthu,
                                                      out tongchi,
                                                      out soducuoiky);

                    InKQTKTheoThoiGian(list, sodudauky, tongthu, tongchi, soducuoiky, "tuan");
                    break;
                case KieuNhapThoiGian.Thang:
                    ThoiGian thangnam = XulyNhapThangNam("Nhap thang nam muon thong ke");

                    list = listGD.ThongKeTheoThoiGian(thangnam, thangnam + ThoiGian.NgayCuoiCungCuaThang(thangnam) - 1,
                                                      out sodudauky,
                                                      out tongthu,
                                                      out tongchi,
                                                      out soducuoiky);

                    InKQTKTheoThoiGian(list, sodudauky, tongthu, tongchi, soducuoiky, "thang");
                    break;
                case KieuNhapThoiGian.Nam:
                    ThoiGian nam = XulyNhapNam("Nhap nam muon thong ke");

                    list = listGD.ThongKeTheoThoiGian(nam, nam + ThoiGian.SoNgayTrongNam(nam) - 1,
                                                      out sodudauky,
                                                      out tongthu,
                                                      out tongchi,
                                                      out soducuoiky);

                    InKQTKTheoThoiGian(list, sodudauky, tongthu, tongchi, soducuoiky, "nam");
                    break;
                case KieuNhapThoiGian.Khoang:
                    ThoiGian dauky = XulyNhapNgayThangNam("Nhap ngay dau ky");
                    ThoiGian cuoiky = XulyNhapNgayThangNam("Nhap ngay cuoi ky");

                    list = listGD.ThongKeTheoThoiGian(dauky, cuoiky,
                                                      out sodudauky,
                                                      out tongthu,
                                                      out tongchi,
                                                      out soducuoiky);

                    InKQTKTheoThoiGian(list, sodudauky, tongthu, tongchi, soducuoiky, "ky");
                    break;
            }
        }

        // Ham xu ly viec in ket qua thong ke theo thoi gian
        static void InKQTKTheoThoiGian(List<GiaoDich> list, double sodudauky, double tongthu, double tongchi, double soducuoiky, string message)
        {
            if (list.Count == 0)
                Console.WriteLine("Khong co giao dich nao duoc thuc hien trong {0}.", message);
            else
            {
                Console.WriteLine();
                Console.WriteLine("So du dau {1} : {0}", sodudauky, message);
                Console.WriteLine("Tong thu trong {1} : {0}", tongthu, message);
                Console.WriteLine("Tong chi trong {1} : {0}", tongchi, message);
                Console.WriteLine("So du cuoi {1} : {0}", soducuoiky, message);

                Console.WriteLine();
                Console.WriteLine("Danh sach cac giao dich trong {0} :", message);
                for (int i = 0; i < list.Count; i++)
                {
                    Console.Write("{0}.", i + 1);
                    Console.WriteLine(list[i]);
                }
            }
        }

        // Ham xu ly chon kieu thong ke theo thoi gian
        static KieuNhapThoiGian XulyNhapKieuNhapTG()
        {
            KieuNhapThoiGian kieuNhapTG = KieuNhapThoiGian.Thang;

            // in menu cac kieu tai khoan
            InMenuCacLoai("kieu thong ke theo thoi gian", "Tuan, Thang, Nam, Khoang");
            while (!Enum.TryParse(Console.ReadLine(), out kieuNhapTG))
            {
                Console.Write("Ban nhap khong dung, moi ban nhap lai : ");
            }

            return kieuNhapTG;
        }

        // Ham xu ly nhap thang nam
        static ThoiGian XulyNhapThangNam(string message)
        {
            Console.Write("{0} (dd/yyyy) : ", message); // Yeu cau nhap thang nam dung dang
            ThoiGian ngay = "01/" + Console.ReadLine();

            // kiem tra :
            while (ngay.LaNgayMacDinh)
            { // neu trai lai thi yeu cau nhap lai
                Console.WriteLine("Thang nam ban nhap khong dung.");
                Console.Write("De nghi ban nhap lai theo dang dd/yyyy : ");
                ngay = "01/" + Console.ReadLine();
            }

            return ngay;
        }

        // Ham xu ly nhap nam
        static ThoiGian XulyNhapNam(string message)
        {
            Console.Write("{0} (yyyy) : ", message); // Yeu cau nhap thang nam dung dang
            ThoiGian nam = "01/01/" + Console.ReadLine();

            // kiem tra :
            while (nam.LaNgayMacDinh)
            { // neu trai lai thi yeu cau nhap lai
                Console.WriteLine("Nam ban nhap khong dung.");
                Console.Write("De nghi ban nhap lai theo dang yyyy : ");
                nam = "01/01/" + Console.ReadLine();
            }

            return nam;
        }

        // Ham xu ly chuc nang In thong ke theo phan loai va thoi gian
        static void InTKTheoPhanLoai()
        {
            // neu la rong thi bo qua
            if (listGD.LaRong)
            {
                Console.WriteLine("Co so du lieu khong co giao dich nao de thong ke.");
                return;
            }

            KieuNhapThoiGian kieuNhapTG = XulyNhapKieuNhapTG();
            bool coGD;
            double tongthu = 0, tongchi = 0;
            double[] tongthus, tongchis;
            int size;
            //List<GiaoDich> list = null;
            switch (kieuNhapTG)
            {
                case KieuNhapThoiGian.Tuan:
                    ThoiGian ngayDauTuan = XulyNhapNgayThangNam("Nhap ngay dau tuan");

                    listGD.ThongKeTheoThoiGian(ngayDauTuan, ngayDauTuan + 6,
                                               out coGD,
                                               out tongthu,
                                               out tongchi,
                                               out tongthus,
                                               out tongchis,
                                               out size);

                    InKQTKTheoPhanLoai(coGD, tongthu, tongchi, tongthus, tongchis, size, "tuan");
                    break;
                case KieuNhapThoiGian.Thang:
                    ThoiGian thangnam = XulyNhapThangNam("Nhap thang nam muon thong ke");

                    listGD.ThongKeTheoThoiGian(thangnam, thangnam + ThoiGian.NgayCuoiCungCuaThang(thangnam) - 1,
                                               out coGD,
                                               out tongthu,
                                               out tongchi,
                                               out tongthus,
                                               out tongchis,
                                               out size);

                    InKQTKTheoPhanLoai(coGD, tongthu, tongchi, tongthus, tongchis, size, "thang");
                    break;
                case KieuNhapThoiGian.Nam:
                    ThoiGian nam = XulyNhapNam("Nhap nam muon thong ke");

                    listGD.ThongKeTheoThoiGian(nam, nam + ThoiGian.SoNgayTrongNam(nam) - 1,
                                               out coGD,
                                               out tongthu,
                                               out tongchi,
                                               out tongthus,
                                               out tongchis,
                                               out size);

                    InKQTKTheoPhanLoai(coGD, tongthu, tongchi, tongthus, tongchis, size, "nam");
                    break;
                case KieuNhapThoiGian.Khoang:
                    ThoiGian dauky = XulyNhapNgayThangNam("Nhap ngay dau ky");
                    ThoiGian cuoiky = XulyNhapNgayThangNam("Nhap ngay cuoi ky");

                    listGD.ThongKeTheoThoiGian(dauky, cuoiky,
                                               out coGD,
                                               out tongthu,
                                               out tongchi,
                                               out tongthus,
                                               out tongchis,
                                               out size);

                    InKQTKTheoPhanLoai(coGD, tongthu, tongchi, tongthus, tongchis, size, "ky");
                    break;
            }
        }

        // Ham xu ly in ket qua thong ke theo phan loai va thoi gian
        static void InKQTKTheoPhanLoai(bool coGD, double tongthu, double tongchi, double[] tongthus, double[] tongchis, int size, string message)
        {
            if (!coGD)
            {
                Console.WriteLine("Khong co giao dich nao trong thoi gian ban yeu cau thong ke.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Tong thu trong {1} : {0}", tongthu, message);
                Console.WriteLine("Tong chi trong {1} : {0}", tongchi, message);

                Console.WriteLine();
                for (int i = 0; i < size; i++)
                {
                    int phantramthu, phantramchi;
                    if (tongthu == 0)
                        phantramthu = 0;
                    else
                        phantramthu = (int)(tongthus[i] / Math.Abs(tongthu) * 100);

                    if (tongchi == 0)
                        phantramchi = 0;
                    else
                        phantramchi = (int)(tongchis[i] / Math.Abs(tongchi) * 100);

                    Console.WriteLine("Tong thu {0} la {1}, chiem {2}%.",
                                      GiaoDich.LayTenLoaiGiaoDich((GiaoDich.LoaiGiaoDich)i),
                                      tongthus[i],
                                      phantramthu);
                    Console.WriteLine("Tong chi {0} la {1}, chiem {2}%.",
                                      GiaoDich.LayTenLoaiGiaoDich((GiaoDich.LoaiGiaoDich)i),
                                      tongchis[i],
                                      phantramchi);
                }
            }
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

        // ham nay tra ra mot so tu 1 den 10
        // tuong ung voi cac so tren menu ma nguoi su dung nhap
        // hai tham so soduoi va sotren giup cho ham nay co the xu ly cho bat ky menu nao
        static int XulyChonMenu(int soduoi, int sotren)
        {
            int menu;
            // ham TryParse thuc hien chuyen doi tu chuoi sang so
            // neu co the chuyen duoc thi ham nay tra ra gia tri true
            // va ket qua o tham chieu menu
            while (!int.TryParse(Console.ReadLine(), out menu)
                   && ((menu < soduoi) || (menu > sotren)))
            {
                Console.WriteLine("Ban chon so nam ngoai pham vi cac so tren menu.");
                Console.WriteLine("Hay chon so tu {0} den {1} phu hop voi chuc nang tuong ung.", soduoi, sotren);
                Console.Write("Moi ban chon lai :");
            }
            return menu;
        }
        #endregion
    }
}
