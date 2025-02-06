using System;

namespace DTO
{
    public class DTO_TAIKHOAN
    {
        private int MATK;
        private int MALOAITK;
        private string TENCHUTAIKHOAN;
        private string TENDANGNHAP;
        private string MATKHAU;

        //Getter-Setter
        public int _MATK
        {
            get { return MATK; }
            set { MATK = value; }
        }

        public int _MALOAITK
        {
            get { return MALOAITK; }
            set { MALOAITK = value; }
        }
        public string _TENCHUTAIKHOAN
        {
            get { return TENCHUTAIKHOAN; }
            set { TENCHUTAIKHOAN = value; }
        }

        public string _TENDANGNHAP
        {
            get { return TENDANGNHAP; }
            set { TENDANGNHAP = value; }
        }

        public string _MATKHAU
        {
            get { return MATKHAU; }
            set { MATKHAU = value; }
        }



        //Constructor

        public DTO_TAIKHOAN()
        {

        }

        public DTO_TAIKHOAN(int MATK, int MALOAITK, string TENCHUTAIKHOAN, string TENDANGNHAP, string MATKHAU)
        {
            this.MATK = MATK;
            this.MALOAITK = MALOAITK;
            this.TENCHUTAIKHOAN = TENCHUTAIKHOAN;
            this.TENDANGNHAP = TENDANGNHAP;
            this.MATKHAU = MATKHAU;
        }
    }
}
