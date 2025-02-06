using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /*CREATE TABLE NHANVIENHIENTAI
(
	MANV INT PRIMARY KEY,
)*/
    public class DTO_NHANVIENHIENTAI
    {
        private int manv;

        public DTO_NHANVIENHIENTAI()
        {
        }

        public DTO_NHANVIENHIENTAI(int manv)
        {
            this.Manv = manv;
        }

        public int Manv { get => manv; set => manv = value; }
    }
}
