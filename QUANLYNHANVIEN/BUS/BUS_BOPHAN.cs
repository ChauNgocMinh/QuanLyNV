using System;
using DAL;
using DTO;
using System.Data;
using System.Collections.Generic;

namespace BUS
{
    public class BUS_BOPHAN
    {
        DAL_BOPHAN bophan = new DAL_BOPHAN();

        public DataTable getBoPhan()
        {
            return bophan.getBoPhan();
        }

        public bool ThemBoPhan(DTO_BOPHAN bp)
        {
            return bophan.ThemBoPhan(bp);
        }

        public bool SuaBoPhan(DTO_BOPHAN bp)
        {
            return bophan.SuaBoPhan(bp);
        }

        public bool XoaBoPhan(string mabp)
        {
            return bophan.XoaBoPhan(mabp);
        }

        public string TimKiemTheoTenBoPhan(string tenBP)
        {
            return bophan.TimKiemTheoTenBoPhan(tenBP);
        }

        public List<string> TongHopTenBoPhan()
        {
            return bophan.TongHopTenBoPhan();
        }

        public string TimKiemTheoMaBoPhan(string maBP)
        {
            return bophan.TimKiemTheoMaBoPhan(maBP);
        }
        public List<string> TongHopMaBoPhan()
        {
            return bophan.TongHopMaBoPhan();
        }
    }
}
