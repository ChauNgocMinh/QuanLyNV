using DAL;
using DTO;
using System.Collections.Generic;
using System.Data;

namespace BUS
{
    public class BUS_THONGBAO
    {
        DAL_THONGBAO thamso = new DAL_THONGBAO();

        public DataTable getThongBao(int Ma)
        {
            return thamso.getThongBao(Ma);
        }

        public bool themThongBao(DTO_BANGTHONGBAO tb, List<int> Ma)
        {
            return thamso.ThemThongBao(tb, Ma);
        }
    }
}
