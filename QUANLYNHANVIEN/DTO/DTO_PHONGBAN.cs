using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_PHONGBAN
    {
        private string maphong;
        private string mabp;
        private string tenphong;
        private DateTime ngaythanhlap;
        private string ghichu;

        public DTO_PHONGBAN()
        {
        }

        public DTO_PHONGBAN(string maphong, string mabp, string tenphong, DateTime ngaythanhlap, string ghichu)
        {
            this.maphong = maphong;
            this.mabp = mabp;
            this.tenphong = tenphong;
            this.ngaythanhlap = ngaythanhlap;
            this.ghichu = ghichu;
        }

        public string Maphong { get => maphong; set => maphong = value; }
        public string Mabp { get => mabp; set => mabp = value; }
        public string Tenphong { get => tenphong; set => tenphong = value; }
        public DateTime Ngaythanhlap { get => ngaythanhlap; set => ngaythanhlap = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
    }
}
