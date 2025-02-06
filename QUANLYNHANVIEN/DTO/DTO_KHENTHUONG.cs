using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_KHENTHUONG
    {
        private int makt;
        private double tien;
        private string lydo;

        public DTO_KHENTHUONG()
        {
        }

        public DTO_KHENTHUONG(int makt, double tien, string lydo)
        {
            this.makt = makt;
            this.tien = tien;
            this.lydo = lydo;
        }

        public int Makt { get => makt; set => makt = value; }
        public double Tien { get => tien; set => tien = value; }
        public string Lydo { get => lydo; set => lydo = value; }
    }
}
