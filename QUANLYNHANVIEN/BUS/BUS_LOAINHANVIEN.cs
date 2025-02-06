using System;
using DAL;
using DTO;
using System.Data;
using System.Collections.Generic;

namespace BUS
{
    public class BUS_LOAINHANVIEN
    {
        DAL_LOAINHANVIEN loainhanvien = new DAL_LOAINHANVIEN();

        public DataTable getLoaiNhanVien()
        {
            return loainhanvien.getLoaiNhanVien();
        }

        public bool ThemLoaiNhanVien(DTO_LOAINHANVIEN lnv)
        {
            return loainhanvien.ThemLoaiNhanVien(lnv);
        }

        public bool SuaLoaiNhanVien(DTO_LOAINHANVIEN lnv)
        {
            return loainhanvien.SuaLoaiNhanVien(lnv);
        }

        public bool XoaLoaiNhanVien(string malnv)
        {
            return loainhanvien.XoaLoaiNhanVien(malnv);
        }

        public string TimKiemTheoLoaiNhanVien(string loaiNV)
        {
            return loainhanvien.TimKiemTheoLoaiNhanVien(loaiNV);
        }

        public string TimKiemTheoMaLoaiNhanVien(string maLoaiNV)
        {
            return loainhanvien.TimKiemTheoMaLoaiNhanVien(maLoaiNV);
        }

        public List<string> TongHopLoaiNhanVien()
        {
            return loainhanvien.TongHopLoaiNhanVien();
        }
    }
}
