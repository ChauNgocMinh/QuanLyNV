using System;
using DAL;
using DTO;
using System.Data;
namespace BUS
{
    public class BUS_SOTHAISAN
    {
        DAL_SOTHAISAN thaisan = new DAL_SOTHAISAN();

        public DataTable getSoThaiSan()
        {
            return thaisan.getSoThaiSan();
        }

        public bool ThemSoThaiSan(DTO_SOTHAISAN sts)
        {
            return thaisan.ThemSoThaiSan(sts);
        }

        public bool SuaSoThaiSan(DTO_SOTHAISAN sts)
        {
            return thaisan.SuaSoThaiSan(sts);
        }

        public bool XoaSoThaiSan(int masts)
        {
            return thaisan.XoaSoThaiSan(masts);
        }

        public bool KiemTraTonTai(string maNV)
        {
            return thaisan.KiemTraTonTai(maNV);
        }

        public DateTime TimNgayLamTroLai(string maNV)
        { 
            return thaisan.TimNgayLamTroLai(maNV); 
        }

        public bool SuaGhiChu(string ghiChu, string maNV)
        {
            return thaisan.SuaGhiChu(ghiChu, maNV);
        }
    }
}
