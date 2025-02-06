using System;
using DAL;
using DTO;
using System.Data;
namespace BUS
{
    public class BUS_THAYDOIBANGLUONG
    {
        DAL_THAYDOIBANGLUONG tdbangluong = new DAL_THAYDOIBANGLUONG();

        public DataTable getThayDoiBangLuong()
        {
            return tdbangluong.getThayDoiBangLuong();
        }
        public DataTable getThayDoiBangLuongCaNhan(string manv)
        {
            return tdbangluong.getThayDoiBangLuongCaNhan(manv);
        }

        public bool ThemThayDoiBangLuong(DTO_THAYDOIBANGLUONG bp)
        {
            return tdbangluong.ThemThayDoiBangLuong(bp);
        }

        public bool SuaThayDoiBangLuong(DTO_THAYDOIBANGLUONG bp)
        {
            return tdbangluong.SuaThayDoiBangLuong(bp);
        }

        public bool XoaThayDoiBangLuong(int manv,string maluong,string maluongmoi)
        {
            return tdbangluong.XoaThayDoiBangLuong(manv,maluong,maluongmoi);
        }

        public bool XoaThayDoiBangLuongCuaNhanVien(int manv)
        {
            return tdbangluong.XoaThayDoiBangLuongCuaNhanVien(manv);
        }

        public bool KiemTraTonTaiThayDoiBangLuong(string maNV, string maLuong, string maLuongMoi)
        {
            return tdbangluong.KiemTraTonTaiThayDoiBangLuong(maNV, maLuong, maLuongMoi);
        }

        public bool KiemTraTonTaiThayDoiBangLuongTheoNhanVien(string maNV)
        {
            return tdbangluong.KiemTraTonTaiThayDoiBangLuongTheoNhanVien(maNV);
        }

        public string TimMaLuongNVThangNay(string maNV, string thang, string nam)
        {
            return tdbangluong.TimMaLuongNVThangNay(maNV, thang, nam);
        }
    }
}
