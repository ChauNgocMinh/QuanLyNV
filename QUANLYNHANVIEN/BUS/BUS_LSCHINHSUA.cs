using System;
using DAL;
using DTO;
using System.Data;
namespace BUS
{
    public class BUS_LSCHINHSUA
    {
        DAL_LSCHINHSUA lsChinhSua = new DAL_LSCHINHSUA();

        public DataTable getLSChinhSua()
        {
            return lsChinhSua.getLSChinhSua();
        }

        public bool ThemLSChinhSua(DTO_LSCHINHSUA nv)
        {
            return lsChinhSua.ThemLSChinhSua(nv);
        }

        public bool XoaLSChinhSua(int maCS)
        {
            return lsChinhSua.XoaLSChinhSua(maCS);
        }

        public DataTable TongHopLSChinhSuaNhanVienTheoPhong(string maPhong, string maNV)
        {
            return lsChinhSua.TongHopLSChinhSuaNhanVienTheoPhong(maPhong, maNV);
        }

        public int TimLanChinhSuaGanNhat(string maNV)
        {
            return lsChinhSua.TimLanChinhSuaGanNhat(maNV);
        }

        public DataTable getLSChinhSuaCuaTungNhanVien(string maNV)
        {
            return lsChinhSua.getLSChinhSuaCuaTungNhanVien(maNV);
        }

        public bool KiemTraTonTaiNhanVien(string maNV)
        {
            return lsChinhSua.KiemTraTonTaiNhanVien(maNV);
        }

        public bool SuaGhiChu(string ghiChu, string maNV)
        {
            return lsChinhSua.SuaGhiChu(ghiChu, maNV);
        }
    }
}
