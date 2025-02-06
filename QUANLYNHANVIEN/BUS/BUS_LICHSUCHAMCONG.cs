using System;
using DAL;
using DTO;
using System.Data;
namespace BUS
{
    public class BUS_LICHSUCHAMCONG
    {
        DAL_LICHSUCHAMCONG lichsuchamcong = new DAL_LICHSUCHAMCONG();

        public DataTable getLichSuChamCong()
        {
            return lichsuchamcong.getLichSuChamCong();
        }

        public bool ThemLichSuChamCong(DTO_LICHSUCHAMCONG lscc)
        {
            return lichsuchamcong.ThemLichSuChamCong(lscc);
        }

        public bool SuaLichSuChamCong(DTO_LICHSUCHAMCONG lscc)
        {
            return lichsuchamcong.SuaLichSuChamCong(lscc);
        }

        public bool XoaLichSuChamCong(int malscc)
        {
            return lichsuchamcong.XoaLichSuChamCong(malscc);
        }

        public bool KiemTraChamCong(string maNV, string ngayLamTruoc)
        {
            return lichsuchamcong.KiemTraChamCong(maNV, ngayLamTruoc);
        }

        public bool KiemTraTonTai(string maNV)
        {
            return lichsuchamcong.KiemTraTonTai(maNV);
        }

        public DateTime TimLanCuoiChamCongTheoMa(string maNV)
        {
            return lichsuchamcong.TimLanCuoiChamCongTheoMa(maNV);
        }
    }
}
