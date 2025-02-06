using System;
using DAL;
using DTO;
using System.Data;
using System.Collections.Generic;

namespace BUS
{
    public class BUS_KYLUAT
    {
        DAL_KYLUAT kyluat = new DAL_KYLUAT();

        public DataTable getKyLuat()
        {
            return kyluat.getKyLuat();
        }

        public bool ThemKyLuat(DTO_KYLUAT kl)
        {
            return kyluat.ThemKyLuat(kl);
        }

        public bool SuaKyLuat(DTO_KYLUAT kl)
        {
            return kyluat.SuaKyLuat(kl);
        }

        public bool XoaKyLuat(int makl)
        {
            return kyluat.XoaKyLuat(makl);
        }

        public List<string> TongHopMaKyLuat()
        {
            return kyluat.TongHopMaKyLuat();
        }
    }
}
