using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_BANGTINHLUONG
    {
		/*CREATE TABLE BANGTINHLUONG
(
	MANV INT,
	LUONG MONEY,
	THANG INT,
	NAM INT,
	PRIMARY KEY (MANV, THANG, NAM),
	GHICHU NVARCHAR(80)
)*/
		private int manv;
		private double luong;
		private int nam;
		private int thang;
		private string ghichu;

		public DTO_BANGTINHLUONG()
		{
		}

		public DTO_BANGTINHLUONG(int manv, double luong, int nam, int thang, string ghichu)
		{
			this.Manv = manv;
			this.Luong = luong;
			this.Nam = nam;
			this.Thang = thang;
			this.Ghichu = ghichu;
		}

        public int Manv { get => manv; set => manv = value; }
        public double Luong { get => luong; set => luong = value; }
        public int Nam { get => nam; set => nam = value; }
        public int Thang { get => thang; set => thang = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
    }
}
