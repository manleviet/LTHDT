using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XuLySoLieu
{
    abstract class QuanSat
    {
        public double X { get; set; }

        // Tra ra gia tri X*M, tuy vao loai quan sat
        public abstract double TongQuanSat
        {
            get;
        }

        // Tra ra gia tri X*X*M, tuy vao loai quan sat
        public abstract double TongQuanSat2
        {
            get;
        }

        public QuanSat() { }

        public QuanSat(double x)
        {
            X = x;
        }
    }
}
