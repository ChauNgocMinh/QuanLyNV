using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_BOPHAN
    {
        private string mabp;
        private string tenbophan;
        private DateTime ngaythanhlap;
        private string ghichu;

        public DTO_BOPHAN()
        {
        }

        public DTO_BOPHAN(string mabp, string tenbophan, DateTime ngaythanhlap, string ghichu)
        {
            this.mabp = mabp;
            this.tenbophan = tenbophan;
            this.ngaythanhlap = ngaythanhlap;
            this.ghichu = ghichu;
        }

        public string Mabp { get => mabp; set => mabp = value; }
        public string Tenbophan { get => tenbophan; set => tenbophan = value; }
        public DateTime Ngaythanhlap { get => ngaythanhlap; set => ngaythanhlap = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
    }
}
