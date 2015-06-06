using System;
using System.Collections;
using System.Collections.Generic;

namespace XuLySoLieu
{
    class MauNgauNhien : IEnumerable
    {
        // Định dạng enum 3 kiểu quan sát
        public enum KieuQuanSat { ThongThuong, ThuGon, Khoang }
        public KieuQuanSat KieuQS { get; set; }

        public enum KieuUocLuong { Diem, Khoang }
        public struct KetQuaUocLuong
        {
            public KieuUocLuong kieuUL;
            public double giatriULDiem;
            public double giatriDauKhoang;
            public double giatriCuoiKhoang;
        }
        public string KetQuaULMoiNhat { get; set; }

        public enum KieuGiaThiet { Khac, LonHon, NhoHon }
        public enum KieuKetQuaKiemDinh { BacBo, KhongBacBo }
        public struct KetQuaKiemDinh
        {
            public KieuKetQuaKiemDinh kqKD;
            public KieuGiaThiet kieuGT;
            public double tieuchuanKD; // gia tri cua U hoac T
            public double cantren; // gia tri can tren cua mien bac bo
            public double canduoi; // gia tri can duoi cua mien bac bo
        }
        public string KetQuaKDMoiNhat { get; set; }

        // [Quan hệ]Quan hệ thành phần với lớp QuanSat
        List<QuanSat> listQS;
        // cỡ mẫu
        public int CoMau { get; set; }

        // Thuộc tính chỉ mục
        public QuanSat this [int index]
        {
            get
            {
                if (index < 0 || index >= listQS.Count)
                    return null;
                return listQS[index];
            }
            set
            {
                if (index >= 0 && index < listQS.Count)
                    listQS[index] = value;
            }
        }

        // Cấu tử mặc định
        public MauNgauNhien() 
        {
            KieuQS = KieuQuanSat.ThongThuong;
            listQS = new List<QuanSat>();
            CoMau = 0;
        }

        public MauNgauNhien(KieuQuanSat kieuQS)
        {
            this.KieuQS = kieuQS;
            listQS = new List<QuanSat>();
            CoMau = 0;
        }

        // [Quan hệ]Xóa đối tượng bên trong hàm hủy tử
        ~MauNgauNhien()
        {
            listQS.Clear();
        }

        // [Quan hệ]Tạo đối tượng (trong quan hệ) bên trong lớp
        public bool AddQuanSat(double x)
        {
            if (KieuQS != KieuQuanSat.ThongThuong) return false;

            QSThongThuong qs = new QSThongThuong(x);
            listQS.Add(qs);
            CoMau++;
            return true;
        }

        public bool AddQuanSat(double x, int m)
        {
            if (KieuQS != KieuQuanSat.ThuGon) return false;

            QSThuGon qs = new QSThuGon(x, m);
            listQS.Add(qs);
            CoMau += m;
            return true;
        }

        public bool AddQuanSat(double x, double y, int m)
        {
            if (KieuQS != KieuQuanSat.Khoang) return false;

            QSKhoang qs = new QSKhoang(x, y, m);
            listQS.Add(qs);
            CoMau += m;
            return true;
        }

        public double KyVong
        {
            get
            {
                double tongqs = 0.0;
                foreach (QuanSat qs in listQS)
                    tongqs += qs.TongQuanSat;

                return tongqs / CoMau;
            }
        }

        public double PhuongSai
        {
            get
            {
                double tongqs2 = 0.0;
                foreach (QuanSat qs in listQS)
                    tongqs2 += qs.TongQuanSat2;

                return tongqs2 / CoMau - KyVong * KyVong;
            }
        }

        public int Count
        {
            get { return listQS.Count; }
        }

        public KetQuaUocLuong UocLuong(KieuUocLuong kieuUL, double alpha = 0.0, double xichma = double.MinValue)
        {
            KetQuaUocLuong kq = new KetQuaUocLuong();
            kq.kieuUL = kieuUL;

            string tam = null; // chuoi tao ket qua cho KetQuaULMoiNhat dung de xuat ra file
            switch (kieuUL)
            {
                case KieuUocLuong.Diem:
                    kq.giatriULDiem = this.KyVong;

                    tam = string.Format("uld\n{0:f4}", kq.giatriULDiem);
                    break;
                case KieuUocLuong.Khoang:
                    tam = string.Format("ulk {0}", alpha);
                    if (xichma != double.MinValue)
                    {
                        double gtToiHanU = GiaTriToiHan.LayGiaTriU(alpha / 2);
                        kq.giatriDauKhoang = this.KyVong - gtToiHanU * xichma / Math.Sqrt(this.CoMau);
                        kq.giatriCuoiKhoang = this.KyVong + gtToiHanU * xichma / Math.Sqrt(this.CoMau);

                        tam += string.Format(" {0}", xichma);
                    }
                    else
                    {
                        double gtToiHanT = GiaTriToiHan.LayGiaTriT(alpha / 2, this.CoMau - 1);
                        kq.giatriDauKhoang = this.KyVong - gtToiHanT * this.PhuongSai / Math.Sqrt(this.CoMau - 1);
                        kq.giatriCuoiKhoang = this.KyVong + gtToiHanT * this.PhuongSai / Math.Sqrt(this.CoMau - 1);
                    }
                    tam += string.Format("\n{0:f4} {1:f4}", kq.giatriDauKhoang, kq.giatriCuoiKhoang);
                    break;
            }
            KetQuaULMoiNhat = tam;

            return kq;
        }

        public KetQuaKiemDinh KiemDinh(KieuGiaThiet kieuGT, double muy0 = 0.0, double alpha = 0.0, double xichma = double.MinValue)
        {
            KetQuaKiemDinh kq = new KetQuaKiemDinh();
            kq.kieuGT = kieuGT;

            string tam; // xu ly chuoi cho KetQuaKDMoiNhat

            tam = string.Format("{0} {1}", kieuGT, alpha);

            double gtToiHan;
            if (xichma != double.MinValue)
            { // truong hop da biet phuong sai tong the
                // tinh U
                kq.tieuchuanKD = (this.KyVong - muy0) * Math.Sqrt(this.CoMau) / Math.Sqrt(xichma);
                gtToiHan = GiaTriToiHan.LayGiaTriU(alpha / 2);

                tam += string.Format(" {0}", xichma);
            }
            else
            { // truong hop chua biet phuong sai tong the
                // tinh T
                kq.tieuchuanKD = (this.KyVong - muy0) * Math.Sqrt(this.CoMau) / Math.Sqrt(this.PhuongSai);
                gtToiHan = GiaTriToiHan.LayGiaTriT(alpha / 2, this.CoMau - 1);
            }
            tam += string.Format("\n{0:f4}", kq.tieuchuanKD);

            kq.kqKD = KieuKetQuaKiemDinh.KhongBacBo;
            switch (kieuGT)
            {
                case KieuGiaThiet.Khac:
                    kq.cantren = -gtToiHan;
                    kq.canduoi = gtToiHan;
                    if (kq.tieuchuanKD < kq.cantren || kq.tieuchuanKD > kq.canduoi)
                        kq.kqKD = KieuKetQuaKiemDinh.BacBo;

                    tam += string.Format("\n{0:f4} {1:f4}", kq.cantren, kq.canduoi);
                    break;
                case KieuGiaThiet.LonHon:
                    kq.cantren = double.MinValue;
                    kq.canduoi = gtToiHan;
                    if (kq.tieuchuanKD > kq.canduoi)
                        kq.kqKD = KieuKetQuaKiemDinh.BacBo;

                    tam += string.Format("\n{0:f4}", kq.canduoi);
                    break;
                case KieuGiaThiet.NhoHon:
                    kq.cantren = -gtToiHan;
                    kq.canduoi = double.MaxValue;
                    if (kq.tieuchuanKD < kq.cantren)
                        kq.kqKD = KieuKetQuaKiemDinh.BacBo;

                    tam += string.Format("\n{0:f4}", kq.cantren);
                    break;
            }
            tam += string.Format("\n{0}", kq.kqKD);

            KetQuaKDMoiNhat = tam;

            return kq;
        }

        // [Quan hệ] Chỉ thay đổi dữ liệu, không gán đối tượng mới

        // Nạp chồng hàm ToString của lớp Object
        // để in ra giá trị của các quan sát trong mẫu ngẫu nhiên
        public override string ToString()
        {
            String temp = null;

            foreach (QuanSat qs in listQS)
                temp += qs.ToString() + "\n";

            return temp;
        }

        // Nạp chồng IEnumerable cho vong lap foreach
        public IEnumerator GetEnumerator()
        {
            return listQS.GetEnumerator();
        }
    }
}
