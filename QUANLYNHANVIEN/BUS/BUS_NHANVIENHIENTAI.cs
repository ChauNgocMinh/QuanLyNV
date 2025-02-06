using System;
using DAL;
using DTO;
using System.Data;
using System.Collections.Generic;

namespace BUS
{
    public class BUS_NHANVIENHIENTAI
    {
        DAL_NHANVIENHIENTAI nvhientai = new DAL_NHANVIENHIENTAI();

        public string getNhanVienHienTai()
        {
            return nvhientai.getNhanVienHienTai();
        }

        public bool ThemNhanVienHienTai(DTO_NHANVIENHIENTAI nvht)
        {
            return nvhientai.ThemNhanVienHienTai(nvht);
        }

        /*public bool SuaNhanVienHienTai(DTO_NHANVIENHIENTAI bp)
        {
            return nvhientai.SuaNhanVienHienTai(bp);
        }*/

        public bool XoaNhanVienHienTai()
        {
            return nvhientai.XoaNhanVienHienTai();
        }

        //public bool KiemTraTonTai(string manv)
        //{
        //    return nvhientai.KiemTraTonTai(manv);
        //}
    }
}
