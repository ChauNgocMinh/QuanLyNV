using System;
using DAL;
using DTO;
using System.Data;
namespace BUS
{
    public class BUS_BANGCHAMCONG
    {
        DAL_BANGCHAMCONG bangchamcong = new DAL_BANGCHAMCONG();

        public DataTable getBangChamCong()
        {
            return bangchamcong.getBangChamCong();
        }

        public DataTable xuatBangChamCong()
        {
            return bangchamcong.xuatBangChamCong();
        }

        public bool ThemBangChamCong(DTO_BANGCHAMCONG bcc)
        {
            return bangchamcong.ThemBangChamCong(bcc);
        }

        public bool SuaBangChamCong(DTO_BANGCHAMCONG bcc)
        {
            return bangchamcong.SuaBangChamCong(bcc);
        }

        public bool XoaBangChamCong(int manvtv, int thang, int nam)
        {
            return bangchamcong.XoaBangChamCong(manvtv, thang, nam);
        }

        public DTO_BANGCHAMCONG getBangChamCongTheoNhanVien(string maNV, int thang, int nam)
        {
            return bangchamcong.getBangChamCongTheoNhanVien(maNV, thang, nam);
        }

        public bool KiemTraTonTai(string maNV, string thang, string nam)
        {
            return bangchamcong.KiemTraTonTai(maNV, thang, nam);
        }

        public DataTable getBangChiTietChamCongTheoNhanVien(string maNV, string thang, string nam)
        {
            return bangchamcong.getBangChiTietChamCongTheoNhanVien(maNV, thang, nam);
        }

        public string GetMaLuongTheoThang(string maNV, string thang, string nam)
        {
            return bangchamcong.GetMaLuongTheoThang(maNV, thang, nam);
        }

        public DTO_BANGCHAMCONG getBangChamCongNhanVienTheoThang(string maNV, int thang, int nam)
        {
            return bangchamcong.getBangChamCongNhanVienTheoThang(maNV, thang, nam);
        }

        public bool KiemTraTonTaiNhanVien(string maNV)
        {
            return bangchamcong.KiemTraTonTaiNhanVien(maNV);
        }

        public bool SuaGhiChu(string ghiChu, string maNV)
        {
            return bangchamcong.SuaGhiChu(ghiChu, maNV);
        }
    }
}
