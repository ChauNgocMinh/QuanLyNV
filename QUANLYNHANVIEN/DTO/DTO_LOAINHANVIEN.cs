using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_LOAINHANVIEN
    {
        private string maloainv;
        private string tenloainv;
        private double mucluongcoban;

        public DTO_LOAINHANVIEN()
        {
        }

        public DTO_LOAINHANVIEN(string maloainv, string tenloainv, double mucluongcoban)
        {
            this.maloainv = maloainv;
            this.tenloainv = tenloainv;
            this.mucluongcoban = mucluongcoban;
        }

        public string Maloainv { get => maloainv; set => maloainv = value; }
        public string Tenloainv { get => tenloainv; set => tenloainv = value; }
        public double Mucluongcoban { get => mucluongcoban; set => mucluongcoban = value; }
    }
}
