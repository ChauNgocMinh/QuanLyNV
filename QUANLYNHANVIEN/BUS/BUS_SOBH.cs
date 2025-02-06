using System;
using DAL;
using DTO;
using System.Data;
namespace BUS
{
    public class BUS_SOBH
    {
        DAL_SOBH sobh = new DAL_SOBH();

        public DataTable getSoBH()
        {
            return sobh.getSoBH();
        }

        public bool ThemSoBH(DTO_SOBH bh)
        {
            return sobh.ThemSoBH(bh);
        }

        public bool SuaSoBH(DTO_SOBH bh)
        {
            return sobh.SuaSoBH(bh);
        }

        public bool XoaSoBH(int mabh)
        {
            return sobh.XoaSoBH(mabh);
        }

        public bool KiemTraTonTaiNhanVien(string maNV)
        {
            return sobh.KiemTraTonTaiNhanVien(maNV);
        }

        public bool SuaGhiChu(string ghiChu, string maNV)
        {
            return sobh.SuaGhiChu(ghiChu, maNV);
        }
    }
}
