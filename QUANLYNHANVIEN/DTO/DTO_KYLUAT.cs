using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_KYLUAT
    {
        private int makl;
        private double tien;
        private string lydo;

        public DTO_KYLUAT()
        {
        }

        public DTO_KYLUAT(int makl, double tien, string lydo)
        {
            this.makl = makl;
            this.tien = tien;
            this.lydo = lydo;
        }

        public int Makl { get => makl; set => makl = value; }
        public double Tien { get => tien; set => tien = value; }
        public string Lydo { get => lydo; set => lydo = value; }
    }
}
