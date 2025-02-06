using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
using DAL;
namespace BUS
{
    public class BUS_PHANLOAITK
    {
        DAL_PHANLOAITK loaiTk = new DAL_PHANLOAITK();

        public DataTable getPhanLoaiTK()
        {
            return loaiTk.getPhanLoaiTK();
        }
        public bool ThemPhanLoaiTK(DTO_PHANLOAITK pltk)
        {
            return loaiTk.ThemPhanLoaiTK(pltk);
        }

        public bool SuaPhanLoaiTK(DTO_PHANLOAITK pltk)
        {
            return loaiTk.SuaPhanLoaiTK(pltk);
        }

        public bool XoaPhanLoaiTK(int maloaitk)
        {
            return loaiTk.XoaPhanLoaiTK(maloaitk);
        }
    }
}
