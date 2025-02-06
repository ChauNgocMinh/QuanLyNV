using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_BAOCAOLUONG
    {
        /*THANG INT,
	NAM INT,
	TONGTIEN MONEY,
	PRIMARY KEY (THANG, NAM),
	GHICHU NVARCHAR(50)*/

        private int thang;
        private int nam;
        private string ghichu;

        public DTO_BAOCAOLUONG()
        {
        }

        public DTO_BAOCAOLUONG(int thang, int nam, string ghichu)
        {
            this.thang = thang;
            this.nam = nam;
            this.ghichu = ghichu;
        }

        public int Thang { get => thang; set => thang = value; }
        public int Nam { get => nam; set => nam = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
    }
}
