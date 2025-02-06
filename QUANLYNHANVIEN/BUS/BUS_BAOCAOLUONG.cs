using System;
using DAL;
using DTO;
using System.Data;
using System.Collections.Generic;

namespace BUS
{
    public class BUS_BAOCAOLUONG
    {
        DAL_BAOCAOLUONG baocaoluong = new DAL_BAOCAOLUONG();

        public DataTable getBaoCaoLuong()
        {
            return baocaoluong.getBaoCaoLuong();
        }

        public bool ThemBaoCaoLuong(DTO_BAOCAOLUONG bp)
        {
            return baocaoluong.ThemBaoCaoLuong(bp);
        }

        public bool SuaBaoCaoLuong(DTO_BAOCAOLUONG bp)
        {
            return baocaoluong.SuaBaoCaoLuong(bp);
        }

        public bool XoaBaoCaoLuong(int thang, int nam)
        {
            return baocaoluong.XoaBaoCaoLuong(thang, nam);
        }
    }
}
