using System;

namespace DTO
{
    public class DTO_BANGTHONGBAO
    {
        private int matb;
        private string tieude;
        private string noidung;
        private string type;
        private DateTime createdate;

        public DTO_BANGTHONGBAO()
        {
        }

        public DTO_BANGTHONGBAO(int matb, string tieude, string noidung, string type, DateTime createdate)
        {
            this.matb = matb;
            this.tieude = tieude;
            this.noidung = noidung;
            this.type = type;
            this.createdate = createdate;
        }

        public int Matb { get => matb; set => matb = value; }
        public string Tieude { get => tieude; set => tieude = value; }
        public string Noidung { get => noidung; set => noidung = value; }
        public string Type { get => type; set => type = value; }
        public DateTime Createdate { get => createdate; set => createdate = value; }
    }
}
