using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_HOSOTHUVIEC
    {
        private int manvtv;
        private string hoten;
        private DateTime ngaysinh;
        private string gioitinh;
        private string cmnd_cccd;
        private string noicap;
        private string vitrithuviec;
        private DateTime ngaytv;
        private int sothangtv;
        private string sdt;
        private string hocvan;
        private string ghichu;

        public DTO_HOSOTHUVIEC()
        {
        }

        public DTO_HOSOTHUVIEC(int manvtv, string hoten, DateTime ngaysinh, string gioitinh, string cmnd_cccd, string noicap, string vitrithuviec, DateTime ngaytv, int sothangtv, string sdt, string hocvan, string ghichu)
        {
            this.manvtv = manvtv;
            this.hoten = hoten;
            this.ngaysinh = ngaysinh;
            this.gioitinh = gioitinh;
            this.cmnd_cccd = cmnd_cccd;
            this.noicap = noicap;
            this.vitrithuviec = vitrithuviec;
            this.ngaytv = ngaytv;
            this.sothangtv = sothangtv;
            this.sdt = sdt;
            this.hocvan = hocvan;
            this.ghichu = ghichu;
        }

        public int Manvtv { get => manvtv; set => manvtv = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public DateTime Ngaysinh { get => ngaysinh; set => ngaysinh = value; }
        public string Gioitinh { get => gioitinh; set => gioitinh = value; }
        public string Cmnd_cccd { get => cmnd_cccd; set => cmnd_cccd = value; }
        public string Noicap { get => noicap; set => noicap = value; }
        public string Vitrithuviec { get => vitrithuviec; set => vitrithuviec = value; }
        public DateTime Ngaytv { get => ngaytv; set => ngaytv = value; }
        public int Sothangtv { get => sothangtv; set => sothangtv = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string Hocvan { get => hocvan; set => hocvan = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
    }
}
