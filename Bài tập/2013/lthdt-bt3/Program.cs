using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XuLySoLieu
{
    class Program
    {
        // Kieu cu phap the hien cu phap dong lenh khi goi chuong trinh
        // Dang 1 : XuLySoLieu.exe
        // Dang 2 : XuLySoLieu.exe -h
        // Dang 3 : XuLySoLieu.exe <num>
        // Dang 4 : XuLySoLieu.exe <path>
        // Dang 5 : XuLySoLieu.exe <path1> <path2>
        enum KieuCuPhap { Dang1, Dang2, Dang3, Dang4, Dang5 }
        static KieuCuPhap kieuCP;

        static MauNgauNhien mau = null; // chuong trinh chi can 1 bien de luu 1 mau ngau nhien

        static void Main(string[] args)
        {
            // Kiem tra doi so dong lenh
            // de xac dinh dang cu phap dong lenh
            if (args.Length == 0)
                kieuCP = KieuCuPhap.Dang1; // khi khong co doi so nao thi thuc hien theo dang 1
            else if (args.Length == 1)
            { // khi co 1 doi so, co the thuoc vao dang 2, 3, 4
                int num;
                if (int.TryParse(args[0], out num)) // neu doi so do kieu int
                    kieuCP = KieuCuPhap.Dang3; // thi thuoc dang 3
                else if (args[0] == "-h") // neu la chuoi "-h"
                    kieuCP = KieuCuPhap.Dang2; // thuoc dang 2
                else // nguoc lai, thuoc dang 4
                    kieuCP = KieuCuPhap.Dang4;
            }
            else if (args.Length == 2) // hai doi so, thuoc dang 5
                kieuCP = KieuCuPhap.Dang5;
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
                    // Hien thi Menu
                    XuLyMenu();
                    break;
                case KieuCuPhap.Dang2:
                    InHuongDanSuDung(); // In ra huong dan su dung
                    Console.ReadKey(); // Cho nguoi sd doc huong dan va bam enter
                    Environment.Exit(0); // roi thoat khoi chuong trinh
                    break;
                case KieuCuPhap.Dang3:
                    // Goi ham nhap du lieu voi so luong phan tu da biet
                    NhapDuLieu(int.Parse(args[0]));
                    // Hien thi Menu
                    XuLyMenu();
                    break;
                case KieuCuPhap.Dang4:
                    // Doc du lieu tu file
                    mau = XuLyFile.DocFile(args[0]);

                    if (mau == null)
                        Console.WriteLine("Loi trong qua trinh doc file.");
                    // Hien thi Menu
                    XuLyMenu();
                    break;
                case KieuCuPhap.Dang5:
                    // Doc du lieu tu file
                    mau = XuLyFile.DocFile(args[0]);

                    if (mau == null)
                    {
                        Console.WriteLine("Loi trong qua trinh doc file.");
                        InHuongDanSuDung();
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    // Tinh toan dua ket qua ra file
                    UocLuong();
                    KiemDinh();
                    LuuKQRaFile();
                    // roi thoat chuong trinh
                    Environment.Exit(0);
                    break;
            }
        }

        // Ham thuc hien in ra huong dan su dung cho chuong trinh
        static void InHuongDanSuDung()
        {
            Console.WriteLine();
            Console.WriteLine("CHUONG TRINH XU LY SO LIEU THONG KE");
            Console.WriteLine("Cu phap cau lenh theo 1 trong 5 dang sau :");
            Console.WriteLine("XuLySoLieu.exe"); // Dang 1
            Console.WriteLine("XuLySoLieu.exe -h"); // Dang 2
            Console.WriteLine("XuLySoLieu.exe <num>"); // Dang 3
            Console.WriteLine("XuLySoLieu.exe <path>"); // Dang 4
            Console.WriteLine("XuLySoLieu.exe <path1> <path2>"); // Dang 5
            Console.WriteLine();
        }

        // Ham thuc hien in ra menu cua chuong trinh
        static void InMenu()
        {
            // In Menu
            Console.WriteLine();
            Console.WriteLine("MENU CHUONG TRINH");
            Console.WriteLine("1 - Nhap du lieu");
            Console.WriteLine("2 - Xem du lieu");
            Console.WriteLine("3 - Uoc luong");
            Console.WriteLine("4 - Kiem dinh");
            Console.WriteLine("5 - Luu du lieu mau ra file");
            Console.WriteLine("6 - Luu ket qua ra file");
            Console.WriteLine("7 - Thoat");
            Console.WriteLine();
            if (mau != null)
                Console.WriteLine("Ban da nhap 1 mau du lieu voi {0} quan sat.", mau.Count);
            Console.Write("Hay nhap vao so tuong ung voi chuc nang ma ban muon thuc hien :");
        }

        // Ham goi ham InMenu roi thuc hien theo yeu cau cua nguoi su dung
        static void XuLyMenu()
        {
            InMenu();

            int menu;
            // cho nguoi su dung nhap vao 1 so tuong ung voi cac chuc nang cua chuong trinh
            while (!int.TryParse(Console.ReadLine(), out menu) && (menu < 1 || menu > 7))
                Console.WriteLine("Ngu vua thoi. Nhap so tu 1 den 7 ay. Nhap lai di :");

            switch (menu)
            {
                case 1: // Nhap du lieu
                    NhapDuLieu();
                    break;
                case 2: // Xem du lieu
                    XemDuLieu();
                    break;
                case 3: // Uoc luong
                    UocLuong();
                    break;
                case 4: // Kiem dinh
                    KiemDinh();
                    break;
                case 5: // Luu du lieu mau ra file
                    LuuMauRaFile();
                    break;
                case 6: // Luu ket qua ra file
                    LuuKQRaFile();
                    break;
                case 7: // Thoat
                    Environment.Exit(0);
                    break;
            }
            XuLyMenu();
        }

        // Nhap du lieu voi so luong phan tu bat ky
        // hoac cho truoc hoac tu file
        // voi truong hop nhap gia tri voi so luong biet truoc thi num != 0
        static void NhapDuLieu(int num = 0)
        {
            // Da co du lieu va nguoi su dung khong muon nhap du lieu moi
            // thi thoat khoi ham nay
            if (mau != null && !LaMuonNhapMoi()) return;

            // Nguoi su dung muon nhap moi
            // doc du lieu moi tu file
            Console.WriteLine("Ban muon nhap du lieu moi tu file ? (Yy/Nn) : ");
            string traloi = Console.ReadLine();
            if (traloi == "Y" || traloi == "y")
            {
                NhapDuLieuTuFile();
            }
            else // hoac thu cong
            {
                // Truoc tien, phai chon kieu quan sat
                MauNgauNhien.KieuQuanSat kieuQS = ChonKieuQuanSat();

                // Tao moi MauNgauNhien
                mau = new MauNgauNhien(kieuQS);

                InHuongDanNhapLieu(num == 0);

                //bien dem dung de kiem soat trong truong hop nhap voi so luong da biet truoc
                int dem = 0; 
                if (num != 0)
                    Console.WriteLine("Moi ban nhap {0} quan sat :", num);
                else
                    Console.WriteLine("Moi ban bat dau nhap du lieu cac quan sat :");
                string quansat = NhapQuanSat(dem + 1);

                // Dieu kien 1 cho truong hop nhap voi so luong chua xac dinh
                // Dieu kien 2 cho truong hop nhap voi so luong da xac dinh
                // thay gop chung vao xu ly trong mot vong lap luon
                while ((quansat != null) || (dem != num))
                {
                    if (quansat == null) // nguoi su dung nhap CTRL+Z
                        if (num != 0) // ma dang nhap voi so luong xac dinh
                        {
                            Console.WriteLine("Ban nhap chua du so luong. Moi ban nhap tiep.");
                            quansat = NhapQuanSat(dem + 1); // yeu cau nhap lai
                            continue;
                        }
                        else
                            break; // nhap voi so luong chua xac dinh thi dung lai

                    double x, y; int m;
                    // tach chuoi du lieu nhap cua user ra cac thanh phan (phu thuoc vao kieu quan sat)
                    if (!TachDuLieu(kieuQS, quansat, out x, out y, out m))
                    { // neu khong tach duoc
                        InThongBaoLoiNhapLieu(); // in thong bao loi
                        quansat = NhapQuanSat(dem + 1); // yeu cua nhap lai
                        continue;
                    }

                    // neu tach duoc
                    // phu thuoc kieu quan sat, them quan sat moi vao mau ngau nhien
                    switch (kieuQS)
                    {
                        case MauNgauNhien.KieuQuanSat.ThongThuong:
                            mau.AddQuanSat(x);
                            break;
                        case MauNgauNhien.KieuQuanSat.ThuGon:
                            mau.AddQuanSat(x, m);
                            break;
                        case MauNgauNhien.KieuQuanSat.Khoang:
                            mau.AddQuanSat(x, y, m);
                            break;
                    }
                    dem++; // tang bien dem cho nhap da xac dinh so luong

                    // khi so luong da xac dinh ma da nhap du so luong 
                    if (num != 0 && dem == num) break;

                    // nhap quan sat tiep theo
                    quansat = NhapQuanSat(dem + 1);
                }
            }
        }

        // Nhap du lieu tu file
        static void NhapDuLieuTuFile()
        {
            Console.Write("Ban nhap duong dan va ten file chua du lieu mau : ");
            string pathin = Console.ReadLine();

            // doc mau vao mot bien tam
            // ly do vi co the doc bi loi, ham DocFile se tra ra null
            // lam mat du lieu da co trong bien mau cua chuong trinh
            MauNgauNhien mautam = XuLyFile.DocFile(pathin);

            if (mautam == null)
                Console.WriteLine("Loi trong qua trinh doc file.");
            else
            {
                mau = mautam; // gan chinh thuc vao bien mau cua chuong trinh
                Console.WriteLine("Doc file hoan tat");
            }
        }

        // Ham gop chung dong thong bao nhap quan sat voi thu tu lan nhap
        // roi doc vao dong ky tu nguoi su dung nhap vao tu ban phim
        static string NhapQuanSat(int index)
        {
            Console.Write("Quan sat {0} : ", index);
            return Console.ReadLine();
        }

        // Ham thuc hien tach chuoi du lieu user nhap vao
        // thanh cac thanh phan tuy kieu quan sat
        static bool TachDuLieu(MauNgauNhien.KieuQuanSat kieuQS, string qs, out double x, out double y, out int m)
        {
            x = 0.0;
            y = 0.0;
            m = 0;

            // tach chuoi ra theo cac ky tu khoang trong
            string[] giatris = qs.Split();

            // phu thuoc vao kieu quan sat
            switch (kieuQS)
            {
                case MauNgauNhien.KieuQuanSat.ThongThuong: // kieu thong thuong chi can nhap 1 gia tri
                    if (giatris.Length != 1) return false; // nhung lai nhap khac 1 gia tri, tra lai false
                    // su dung TryParse de chuyen doi kieu chuoi ve double, ket qua chuyen duoc tra ra bien tham chieu x
                    // ham nay tra ra true neu chuyen doi duoc, false neu sai
                    if (!double.TryParse(giatris[0], out x)) return false;
                    break;
                case MauNgauNhien.KieuQuanSat.ThuGon: // tuong tu tren
                    if (giatris.Length != 2) return false;
                    if (!double.TryParse(giatris[0], out x)) return false;
                    if (!int.TryParse(giatris[1], out m)) return false;
                    break;
                case MauNgauNhien.KieuQuanSat.Khoang: // tuong tu tren
                    if (giatris.Length != 3) return false;
                    if (!double.TryParse(giatris[0], out x)) return false;
                    if (!double.TryParse(giatris[1], out y)) return false;
                    if (!int.TryParse(giatris[2], out m)) return false;
                    break;
            }

            return true;
        }

        // In thong bao loi khi nhap lieu sai voi kieu quan sat
        static void InThongBaoLoiNhapLieu()
        {
            Console.WriteLine("LOI! Du lieu vua roi khong duoc them vao mau.");
        }

        // In huong dan nhap lieu
        // dabietSL - da biet so luong
        static void InHuongDanNhapLieu(bool dabietSL)
        {
            Console.WriteLine();
            Console.WriteLine("Huong dan nhap lieu :");
            Console.WriteLine("Nhap du lieu cua moi quan sat tren cung mot dong,");
            Console.WriteLine("moi gia tri cach nhau mot khoang trong");
            Console.WriteLine("Nhap cung cach tren cho ca 3 dang quan sat.");
            Console.WriteLine("Vi du:");
            Console.WriteLine("Dang thong thuong, ta nhap: 100");
            Console.WriteLine("Dang thu gon, ta nhap: 100 10");
            Console.WriteLine("Dang khoang, ta nhap: 100 120 10");
            if (dabietSL)
                Console.WriteLine("Viec nhap ket thuc khi ban nhap chuoi rong CTRL+Z");
            Console.WriteLine();
        }

        // In huong dan chon kieu quan sat va cho user nhap kieu quan sat cho dung
        static MauNgauNhien.KieuQuanSat ChonKieuQuanSat()
        {
            Console.WriteLine("Ban muon nhap du lieu dang nao trong 3 dang du lieu :");
            Console.Write("(Ban nhap dung 1 trong 3 tu sau - ThongThuong, ThuGon, Khoang)");

            MauNgauNhien.KieuQuanSat kieuQS;
            // Ham TryParse thuc hien ep chuoi ve kieu Enum, neu sai thi tra ra gia tri false
            // neu dung thi tra ra true va gia tri bien tham chieu kieuQS se nhan duoc ket qua ep kieu do
            while (!Enum.TryParse(Console.ReadLine(), out kieuQS))
            {
                Console.WriteLine("Nhap sai roi. Nhap cho dung 1 trong 3 tu ThongThuong, ThuGon, Khoang.");
                Console.Write("Moi nhap lai : ");
            }

            return kieuQS;
        }

        // Ham thuc hien xac dinh y muon cua user ve viec nhap du lieu moi cho mau
        // Tra ra false khi user nhap N hoac n
        // True khi user nhap Y hoac y
        static bool LaMuonNhapMoi()
        {
            Console.WriteLine("Chuong trinh da co mot mau du lieu");
            Console.WriteLine("Ban muon nhap du lieu moi ? (Yy/Nn)");

            string traloi = Console.ReadLine();
            if (traloi == "Y" || traloi == "y") return true;

            return false;
        }

        // Ham nay in ra du lieu mau
        // Dong thoi in ra ca ky vong va phuong sai cua mau
        static void XemDuLieu()
        {
            // neu chua co mau du lieu nao trong chuong trinh
            if (mau == null)
            {
                InThongBaoYeuCauNhapDuLieu("xem du lieu"); // in thong bao phai nhap du lieu cai da
                return; // roi thoat khoi ham nay
            }

            Console.WriteLine();
            Console.WriteLine("Mau co {0} quan sat.", mau.CoMau); // so luong quan sat
            Console.WriteLine("Du lieu cua cac quan sat trong mau :");
            // in tieu de dau cot du lieu
            switch (mau.KieuQS)
            {
                case MauNgauNhien.KieuQuanSat.ThongThuong:
                    Console.WriteLine("Gia tri");
                    break;
                case MauNgauNhien.KieuQuanSat.ThuGon:
                    Console.WriteLine("Gia tri\tTan suat");
                    break;
                case MauNgauNhien.KieuQuanSat.Khoang:
                    Console.WriteLine("Dau khoang\tCuoi khoang\tTan suat");
                    break;
            }
            Console.WriteLine(mau.ToString()); // Goi ham ToString cua kieu MauNgauNhien
            Console.WriteLine("Ky vong cua mau : {0:f4}", mau.KyVong); // ky vong
            Console.WriteLine("Phuong sai cua mau : {0:f4}", mau.PhuongSai); // phuong sai

            Console.ReadKey();
        }

        static void InThongBaoYeuCauNhapDuLieu(string thongbao)
        {
            Console.WriteLine();
            Console.WriteLine("CANH BAO! Ban chua nhap du lieu. Vui long nhap du lieu truoc khi {0}.",thongbao);
        }

        // Thuc hien tinh uoc luong
        static void UocLuong()
        {
            if (mau == null) // chua co du lieu mau
            {
                InThongBaoYeuCauNhapDuLieu("tinh uoc luong");
                return;
            }

            MauNgauNhien.KieuUocLuong kieuUL = ChonKieuUocLuong();

            double alpha = 0.0;
            double xichma = double.MinValue;
            if (kieuUL == MauNgauNhien.KieuUocLuong.Khoang)
            {
                alpha = NhapGiaTri("Moi nhap gia tri do tin cay : ");
                xichma = NhapGiaTri("Moi ban nhap gia tri phuong sai tong the. Neu chua biet thi nhap gia tri 0 : ");

                if (xichma == 0) xichma = double.MinValue;
            }

            // thuc hien tinh uoc luong
            MauNgauNhien.KetQuaUocLuong kqUL = mau.UocLuong(kieuUL, alpha, xichma);

            // in ket qua uoc luong
            switch (kqUL.kieuUL)
            {
                case MauNgauNhien.KieuUocLuong.Diem:
                    Console.WriteLine("Uoc luong diem cho gia tri trung binh tong the la {0:f4}", kqUL.giatriULDiem);
                    break;
                case MauNgauNhien.KieuUocLuong.Khoang:
                    Console.WriteLine("Uoc luong khoang voi do tin cay (1-{0}) la ({1:f4};{2:f4})", alpha, kqUL.giatriDauKhoang, kqUL.giatriCuoiKhoang);
                    break;
            }

            Console.ReadKey();
        }

        static MauNgauNhien.KieuUocLuong ChonKieuUocLuong()
        {
            Console.WriteLine("Ban muon uoc luong diem hay khoang ?");
            Console.Write("Hay nhap Diem hoac Khoang :");

            MauNgauNhien.KieuUocLuong kieuUL;
            while (!Enum.TryParse(Console.ReadLine(), out kieuUL))
            {
                Console.Write("Nhap sai roi. Moi nhap lai cho dung : ");
            }

            return kieuUL;
        }

        // Thuc hien tinh kiem dinh
        static void KiemDinh()
        {
            if (mau == null) // chua co du lieu mau
            {
                InThongBaoYeuCauNhapDuLieu("thuc hien kiem dinh.");
                return;
            }

            double muy = NhapGiaTri("Moi nhap gia tri trung binh can kiem dinh : ");

            double xichma = NhapGiaTri("Moi ban nhap gia tri phuong sai tong the. Neu chua biet thi nhap gia tri 0 : ");
            if (xichma == 0) xichma = double.MinValue;

            double alpha = NhapGiaTri("Moi nhap gia tri muc y nghia : ");

            MauNgauNhien.KieuGiaThiet kieuGT = ChonKieuGiaThiet();

            // thuc hien tinh kiem dinh
            MauNgauNhien.KetQuaKiemDinh kqKD = mau.KiemDinh(kieuGT, muy, alpha, xichma);

            // in ket qua
            Console.WriteLine("Voi muc y nghia {0}, ta co tieu chuan kiem dinh la {1} = {2}", 
                           alpha,
                           (xichma == double.MinValue) ? "T" : "U",
                           kqKD.tieuchuanKD);

            Console.Write("Voi cap gia thiet ");
            switch (kqKD.kieuGT)
            {
                case MauNgauNhien.KieuGiaThiet.Khac:
                    Console.WriteLine("KHAC");
                    Console.WriteLine("Mien bac bo H0 la (-∞ ; {0})U({1} ; +∞)", kqKD.cantren, kqKD.canduoi);
                    break;
                case MauNgauNhien.KieuGiaThiet.LonHon:
                    Console.WriteLine("LON HON");
                    Console.WriteLine("Mien bac bo H0 la ({0} ; +∞)", kqKD.canduoi);
                    break;
                case MauNgauNhien.KieuGiaThiet.NhoHon:
                    Console.WriteLine("NHO HON");
                    Console.WriteLine("Mien bac bo H0 la (-∞ ; {0})", kqKD.cantren);
                    break;
            }

            switch (kqKD.kqKD)
            {
                case MauNgauNhien.KieuKetQuaKiemDinh.BacBo:
                    Console.WriteLine("Vi {0} thuoc mien bac bo, nen ta bac bo gia thiet H0, chap nhan gia thiet H1",
                                       (xichma == double.MinValue) ? "T" : "U");
                    break;
                case MauNgauNhien.KieuKetQuaKiemDinh.KhongBacBo:
                    Console.WriteLine("Vi {0} khong thuoc mien bac bo, nen ta chua du co so de bac bo gia thiet H0",
                                       (xichma == double.MinValue) ? "T" : "U");
                    break;
            }

            Console.ReadKey();
        }

        // Ham in thong bao nhap so lieu
        // va cho phep user nhap du lieu tu ban phim
        // Ham nay dung de nhap so lieu cho gia tri do tin cay, phuong sai tong the
        // muc y nghia, gia tri trung binh can kiem dinh
        static double NhapGiaTri(string message)
        {
            Console.Write(message);
            double tam;
            while (!double.TryParse(Console.ReadLine(), out tam))
            {
                Console.Write("Ban nhap sai, moi ban nhap lai : ");
            }
            return tam;
        }

        static MauNgauNhien.KieuGiaThiet ChonKieuGiaThiet()
        {
            Console.WriteLine("Ban muon thuc hien kiem dinh tren cap gia thiet nao ?");
            Console.Write("Hay chon 1 trong 3 gia tri Khac, LonHon va NhoHon : ");
            MauNgauNhien.KieuGiaThiet kieuGT;
            while (!Enum.TryParse(Console.ReadLine(), out kieuGT))
            {
                Console.Write("Nhap sai roi. Moi nhap lai cho dung : ");
            }
            return kieuGT;
        }

        // Ham thuc hien luu du lieu cua mau ra file
        // Khuon dang luu phu hop de chuong trinh co the doc lai
        static void LuuMauRaFile()
        {
            if (mau == null)
            {
                InThongBaoYeuCauNhapDuLieu("luu ra file");
                return;
            }

            Console.Write("Ban nhap duong dan va ten file de luu mau : ");
            string pathout = Console.ReadLine();

            if (!XuLyFile.LuuMauRaFile(mau, pathout))
            {
                Console.WriteLine("Loi trong qua trinh luu mau ra file.");
                Console.WriteLine("Moi ban thu lai.");
                return;
            }
        }

        // Luu ket qua ra file
        // Khuon dang luu ket qua nhu mo ta trong huong dan bai tap 1
        static void LuuKQRaFile()
        {
            if (mau == null)
            {
                InThongBaoYeuCauNhapDuLieu("luu ket qua ra file");
                return;
            }

            Console.Write("Ban nhap duong dan va ten file de luu ket qua : ");
            string pathout = Console.ReadLine();

            if (!XuLyFile.LuuKQRaFile(mau, pathout))
            {
                Console.WriteLine("Loi trong qua trinh luu ket qua ra file.");
                Console.WriteLine("Moi ban thu lai.");
                return;
            }
        }
    }
}
