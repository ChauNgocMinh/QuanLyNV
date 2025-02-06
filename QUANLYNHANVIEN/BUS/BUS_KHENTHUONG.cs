using System;
using DAL;
using DTO;
using System.Data;
using System.Collections.Generic;

namespace BUS
{
    public class BUS_KHENTHUONG
    {
        DAL_KHENTHUONG khenthuong = new DAL_KHENTHUONG();

        public DataTable getKhenThuong()
        {
            return khenthuong.getKhenThuong();
        }

        public bool ThemKhenThuong(DTO_KHENTHUONG kt)
        {
            return khenthuong.ThemKhenThuong(kt);
        }

        public bool SuaKhenThuong(DTO_KHENTHUONG kt)
        {
            return khenthuong.SuaKhenThuong(kt);
        }

        public bool XoaKhenThuong(int makt)
        {
            return khenthuong.XoaKhenThuong(makt);
        }

        public List<string> TongHopMaKhenThuong()
        {
            return khenthuong.TongHopMaKhenThuong();
        }
    }
}
