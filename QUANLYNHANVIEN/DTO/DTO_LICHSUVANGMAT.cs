using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_LICHSUVANGMAT
    {
        /*
    MANV INT,
	NGAYNGHI DATETIME,
	PRIMARY KEY (MANV, NGAYNGHI),
	GHICHU NVARCHAR(50)
         */
        private int manv;
        private DateTime ngaynghi;
        private string ghichu;

        public DTO_LICHSUVANGMAT()
        {
        }

        public DTO_LICHSUVANGMAT(int manv, DateTime ngaynghi, string ghichu)
        {
            this.Manv = manv;
            this.Ngaynghi = ngaynghi;
            this.Ghichu = ghichu;
        }

        public int Manv { get => manv; set => manv = value; }
        public DateTime Ngaynghi { get => ngaynghi; set => ngaynghi = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
    }
}
