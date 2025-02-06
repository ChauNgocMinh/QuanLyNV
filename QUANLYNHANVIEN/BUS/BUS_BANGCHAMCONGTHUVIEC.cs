using System;
using DAL;
using DTO;
using System.Data;
namespace BUS
{
    public class BUS_BANGCHAMCONGTHUVIEC
    {
        DAL_BANGCHAMCONGTHUVIEC bangchamcongthuviec = new DAL_BANGCHAMCONGTHUVIEC();

        public DataTable getBangChamCongThuViec()
        {
            return bangchamcongthuviec.getBangChamCongThuViec();
        }

        public DataTable xuatBangChamCongThuViec()
        {
            return bangchamcongthuviec.xuatBangChamCongThuViec();
        }

        public bool ThemBangChamCongThuViec(DTO_BANGCHAMCONGTHUVIEC bcctv)
        {
            return bangchamcongthuviec.ThemBangChamCongThuViec(bcctv);
        }

        public bool SuaBangChamCongThuViec(DTO_BANGCHAMCONGTHUVIEC bcctv)
        {
            return bangchamcongthuviec.SuaBangChamCongThuViec(bcctv);
        }

        public bool XoaBangChamCongThuViec(int manvtv, int thang, int nam)
        {
            return bangchamcongthuviec.XoaBangChamCongThuViec(manvtv, thang, nam);
        }

        public DataTable getBangChamCongThuViecTheoThang(string thang, string nam)
        {
            return bangchamcongthuviec.getBangChamCongThuViecTheoThang(thang, nam);
        }

        public DataTable xuatBangChamCongThuViecTheoThang(string thang, string nam)
        {
            return bangchamcongthuviec.xuatBangChamCongThuViecTheoThang(thang, nam);
        }
    }
}
