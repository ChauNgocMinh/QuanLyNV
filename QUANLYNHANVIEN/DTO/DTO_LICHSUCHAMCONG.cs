using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /*CREATE TABLE LICHSUCHAMCONG
(
	MALSCHAMCONG INT IDENTITY(1,1) PRIMARY KEY,
	MANV INT,
	NGAYCHAMCONGGANNHAT DATETIME,
	GHICHU NVARCHAR(50)
)*/
    public class DTO_LICHSUCHAMCONG
    {
        private int malschamcong;
        private int manv;
        private DateTime ngaychamconggannhat;
        private string ghichu;

        public DTO_LICHSUCHAMCONG()
        {
        }

        public DTO_LICHSUCHAMCONG(int malschamcong, int manv, DateTime ngaychamconggannhat, string ghichu)
        {
            this.malschamcong = malschamcong;
            this.manv = manv;
            this.ngaychamconggannhat = ngaychamconggannhat;
            this.ghichu = ghichu;
        }

        public int Malschamcong { get => malschamcong; set => malschamcong = value; }
        public int Manv { get => manv; set => manv = value; }
        public DateTime Ngaychamconggannhat { get => ngaychamconggannhat; set => ngaychamconggannhat = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
    }
}
