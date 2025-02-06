using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_BANGCHAMCONG
    {
        private int manv;
        private int thang;
        private int nam;
        private string maluong;
        private double tienkyluat;
        private double tienkhenthuong;
        private int songaycong;
        private int songaynghi;
        private int sogiolamthem;
        private string ghichu;

        public DTO_BANGCHAMCONG()
        {
        }

        public DTO_BANGCHAMCONG(int manv, int thang, int nam, string maluong, double tienkyluat, double tienkhenthuong, int songaycong, int songaynghi, int sogiolamthem, string ghichu)
        {
            this.manv = manv;
            this.thang = thang;
            this.nam = nam;
            this.maluong = maluong;
            this.tienkyluat = tienkyluat;
            this.tienkhenthuong = tienkhenthuong;
            this.songaycong = songaycong;
            this.songaynghi = songaynghi;
            this.sogiolamthem = sogiolamthem;
            this.ghichu = ghichu;
        }

        public int Manv { get => manv; set => manv = value; }
        public int Thang { get => thang; set => thang = value; }
        public int Nam { get => nam; set => nam = value; }
        public string Maluong { get => maluong; set => maluong = value; }
        public int Songaycong { get => songaycong; set => songaycong = value; }
        public int Songaynghi { get => songaynghi; set => songaynghi = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
        public int Sogiolamthem { get => sogiolamthem; set => sogiolamthem = value; }
        public double Tienkyluat { get => tienkyluat; set => tienkyluat = value; }
        public double Tienkhenthuong { get => tienkhenthuong; set => tienkhenthuong = value; }
    }
}
