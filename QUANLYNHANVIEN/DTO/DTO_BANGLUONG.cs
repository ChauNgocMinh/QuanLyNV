using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_BANGLUONG
    {
        private string maluong;
        private double lcb;
        private double phucapchucvu;
        private double phucapkhac;
        private string ghichu;

        public DTO_BANGLUONG()
        {
        }

        public DTO_BANGLUONG(string maluong, double lcb, double phucapchucvu, double phucapkhac, string ghichu)
        {
            this.maluong = maluong;
            this.lcb = lcb;
            this.phucapchucvu = phucapchucvu;
            this.phucapkhac = phucapkhac;
            this.ghichu = ghichu;
        }

        public string Maluong { get => maluong; set => maluong = value; }
        public double Lcb { get => lcb; set => lcb = value; }
        public double Phucapchucvu { get => this.phucapchucvu; set => this.phucapchucvu = value; }
        public double Phucapkhac { get => phucapkhac; set => phucapkhac = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
    }
}
