using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_THAYDOIBANGLUONG
    {
        private int manv;
        private string maluong;
        private string maluongmoi;
        private DateTime ngaysua;
        private string lydo;

        public DTO_THAYDOIBANGLUONG()
        {
        }

        public DTO_THAYDOIBANGLUONG(int manv, string maluong, string maluongmoi, DateTime ngaysua, string lydo)
        {
            this.manv = manv;
            this.maluong = maluong;
            this.maluongmoi = maluongmoi;
            this.ngaysua = ngaysua;
            this.lydo = lydo;
        }

        public int Manv { get => manv; set => manv = value; }
        public string Maluong { get => maluong; set => maluong = value; }
        public string Maluongmoi { get => maluongmoi; set => maluongmoi = value; }
        public DateTime Ngaysua { get => ngaysua; set => ngaysua = value; }
        public string Lydo { get => lydo; set => lydo = value; }
    }
}
