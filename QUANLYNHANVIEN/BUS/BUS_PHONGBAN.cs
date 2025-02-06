using System;
using DAL;
using DTO;
using System.Data;
using System.Collections.Generic;

namespace BUS
{
    public class BUS_PHONGBAN
    {
        DAL_PHONGBAN phongban = new DAL_PHONGBAN();

        public DataTable getPhongBan()
        {
            return phongban.getPhongBan();
        }

        public bool ThemPhongBan(DTO_PHONGBAN phong)
        {
            return phongban.ThemPhongBan(phong);
        }

        public bool SuaPhongBan(DTO_PHONGBAN phong)
        {
            return phongban.SuaPhongBan(phong);
        }

        public bool XoaPhongBan(string maphong)
        {
            return phongban.XoaPhongBan(maphong);
        }

        public string TimKiemMaPhongBan(string tenPhong)
        {
            return phongban.TimKiemMaPhongBan(tenPhong);
        }

        public string TimKiemTenPhongBanTheoMa(string maPhong)
        {
            return phongban.TimKiemTenPhongBanTheoMa(maPhong);
        }

        public List<string> TongHopPhongBan(string maBP)
        {
            return phongban.TongHopPhongBan(maBP);
        }

        public string TimKiemBoPhanTheoPhong(string maPhong)
        {
            return phongban.TimKiemBoPhanTheoPhong(maPhong);
        }
        public List<string> TongHopMaPhongBan()
        {
            return phongban.TongHopMaPhongBan();
        }
    }
}
