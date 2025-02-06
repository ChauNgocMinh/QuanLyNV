using System;
using DAL;
using DTO;
using System.Data;
using System.Collections.Generic;

namespace BUS
{
    public class BUS_BANGTINHLUONG
    {
        DAL_BANGTINHLUONG bangluong = new DAL_BANGTINHLUONG();

        public DataTable getBangTinhLuong()
        {
            return bangluong.getBangTinhLuong();
        }

        public bool ThemBangTinhLuong(DTO_BANGTINHLUONG luong)
        {
            return bangluong.ThemBangTinhLuong(luong);
        }

        public bool SuaBangTinhLuong(DTO_BANGTINHLUONG luong)
        {
            return bangluong.SuaBangTinhLuong(luong);
        }

        public bool XoaBangTinhLuong(int manv,int thang,int nam)
        {
            return bangluong.XoaBangTinhLuong(manv,thang,nam);
        }

        public DataTable getBangTinhLuongTheoThang(string thang, string nam)
        {
            return bangluong.getBangTinhLuongTheoThang(thang, nam);
        }

        public bool KiemTraTonTaiNhanVien(string maNV)
        {
            return bangluong.KiemTraTonTaiNhanVien(maNV);
        }

        public bool SuaGhiChu(string ghiChu, string maNV)
        {
            return bangluong.SuaGhiChu(ghiChu, maNV);
        }
    }
}