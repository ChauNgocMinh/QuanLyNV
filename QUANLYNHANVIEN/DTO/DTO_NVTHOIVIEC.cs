using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_NVTHOIVIEC
    {
        private int manv;
        private string hoten;
        private string cmnd_cccd;
        private string lydo;
        private DateTime ngaythoiviec;

        public DTO_NVTHOIVIEC()
        {
        }

        public DTO_NVTHOIVIEC(int manv, string hoten, string cmnd_cccd, string lydo, DateTime ngaythoiviec)
        {
            this.manv = manv;
            this.hoten = hoten;
            this.cmnd_cccd = cmnd_cccd;
            this.lydo = lydo;
            this.ngaythoiviec = ngaythoiviec;
        }

        public int Manv { get => manv; set => manv = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public string Cmnd_cccd { get => cmnd_cccd; set => cmnd_cccd = value; }
        public string Lydo { get => lydo; set => lydo = value; }
        public DateTime Ngaythoiviec { get => ngaythoiviec; set => ngaythoiviec = value; }
    }
}
