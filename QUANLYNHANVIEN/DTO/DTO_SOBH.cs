using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_SOBH
    {
        private int mabh;
        private int manv;
        private DateTime ngaycapso;
        private string noicapso;
        private string ghichu;

        public DTO_SOBH()
        {
        }

        public DTO_SOBH(int mabh, int manv, DateTime ngaycapso, string noicapso, string ghichu)
        {
            this.mabh = mabh;
            this.manv = manv;
            this.ngaycapso = ngaycapso;
            this.noicapso = noicapso;
            this.ghichu = ghichu;
        }

        public int Mabh { get => mabh; set => mabh = value; }
        public int Manv { get => manv; set => manv = value; }
        public DateTime Ngaycapso { get => ngaycapso; set => ngaycapso = value; }
        public string Noicapso { get => noicapso; set => noicapso = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
    }
}
