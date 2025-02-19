using System;

namespace DTO
{
    public class DTO_THONGBAONHANVIEN
    {
        private int ma;
        private int manv;
        private int matb;
        private bool isopen;

        public DTO_THONGBAONHANVIEN()
        {
        }

        public DTO_THONGBAONHANVIEN(int ma, int manv, int matb, bool isopen)
        {
            this.ma = ma;
            this.manv = manv;
            this.matb = matb;
            this.isopen = isopen;
        }

        public int Ma { get => ma; set => ma = value; }
        public int Manv { get => manv; set => manv = value; }
        public int Matb { get => matb; set => matb = value; }
        public bool Isopen { get => isopen; set => isopen = value; }
    }
}
