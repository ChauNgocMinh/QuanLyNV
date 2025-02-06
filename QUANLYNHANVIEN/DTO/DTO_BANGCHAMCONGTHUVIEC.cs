using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_BANGCHAMCONGTHUVIEC
    {
        private int manvtv;
        private int thang;
        private int nam;
        private int songaycong;
        private int songaynghi;
        private int sogiolamthem;
        private double luongtv;
        private string ghichu;

        public DTO_BANGCHAMCONGTHUVIEC()
        {
        }

        public DTO_BANGCHAMCONGTHUVIEC(int manvtv, int thang, int nam, int songaycong, int songaynghi, int sogiolamthem, double luongtv, string ghichu)
        {
            this.manvtv = manvtv;
            this.thang = thang;
            this.nam = nam;
            this.songaycong = songaycong;
            this.songaynghi = songaynghi;
            this.sogiolamthem = sogiolamthem;
            this.ghichu = ghichu;
            this.luongtv = luongtv;
        }

        public int Manvtv { get => manvtv; set => manvtv = value; }
        public int Thang { get => thang; set => thang = value; }
        public int Nam { get => nam; set => nam = value; }
        public int Songaycong { get => songaycong; set => songaycong = value; }
        public int Songaynghi { get => songaynghi; set => songaynghi = value; }
        public int Sogiolamthem { get => sogiolamthem; set => sogiolamthem = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
        public double Luongtv { get => luongtv; set => luongtv = value; }
    }
}
