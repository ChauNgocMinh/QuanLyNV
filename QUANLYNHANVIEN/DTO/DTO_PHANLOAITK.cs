using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_PHANLOAITK
    {
        private int maloaitk;
        private string tenloaitk;
        private string quyenhan;
        
        //Getter-Setter

        public int MALOAITK
        {
            get { return maloaitk; }
            set { maloaitk = value; }
        }

        public string TENLOAITK
        {
            get { return tenloaitk; }
            set { tenloaitk = value; }
        }

        public string QUYENHAN
        {
            get { return quyenhan; }
            set { quyenhan = value; }
        }

        //Constructor

        public DTO_PHANLOAITK()
        {

        }

        public DTO_PHANLOAITK(int maloaitk, string tenloaitk, string quyenhan)
        {
            this.maloaitk = maloaitk;
            this.tenloaitk = tenloaitk;
            this.quyenhan = quyenhan;
        }


    }
}
