using System;
using DAL;
using DTO;
using System.Data;
using System.Collections.Generic;

namespace BUS
{
    public class BUS_NHANVIEN
    {
        DAL_NHANVIEN nhanvien = new DAL_NHANVIEN();

        public DataTable getNhanVien()
        {
            return nhanvien.getNhanVien();
        }

        public DataTable xuatNhanVien()
        {
            return nhanvien.xuatNhanVien();
        }

        public bool ThemNhanVien(DTO_NHANVIEN nv)
        {
            return nhanvien.ThemNhanVien(nv);
        }

        public bool SuaNhanVien(DTO_NHANVIEN nv)
        {
            return nhanvien.SuaNhanVien(nv);
        }

        public bool XoaNhanVien(int manv)
        {
            return nhanvien.XoaNhanVien(manv);
        }

        public DataTable TongHopNhanVienTheoPhong(string maPhong, string ten)
        {
            return nhanvien.TongHopNhanVienTheoPhong(maPhong, ten);
        }

        public List<string> TongHopMaNhanVienTheoGioiTinh(string gioiTinh)
        {
            return nhanvien.TongHopMaNhanVienTheoGioiTinh(gioiTinh);
        }

        public List<string> TongHopMaNhanVien()
        {
            return nhanvien.TongHopMaNhanVien();
        }

        public string TimTenNVTheoMa(string maNV)
        {
            return nhanvien.TimTenNVTheoMa(maNV);
        }

        public int TimMaNVTheoTen(string tenNV)
        {
            return nhanvien.TimMaNVTheoTen(tenNV);
        }

        public string GetMaLuong(string maNV)
        {
            return nhanvien.GetMaLuong(maNV);
        }

        public bool SuaMaLuongNhanVien(string maNV, string maLuong)
        {
            return nhanvien.SuaMaLuongNhanVien(maNV, maLuong);
        }
        
        public int SoLuongNhanVienVaoLam(int thang,int nam )
        {
            return nhanvien.SoLuongNhanVienVaoLam(thang, nam);
        }

        public DTO_NHANVIEN GetChiTietNhanVienTheoMa(string maNV)
        {
            return nhanvien.GetChiTietNhanVienTheoMa(maNV);
        }
        public DataTable TimKiemNhanVienTheoMa(string manv)
        {
            return nhanvien.TimKiemNVTheoMa(manv);
        }
        public DataTable TimKiemNhanVienTheoTen(string ten)
        {
            return nhanvien.TimKiemNVTheoTen(ten);
        }
        public DataTable TimKiemNhanVienTheoSDT(string sdt)
        {
            return nhanvien.TimKiemNVTheoSDT(sdt);
        }

        public int TimNamDauTienNVVaoLam()
        {
            return nhanvien.TimNamDauTienNVVaoLam();
        }

        public int TimNamGanNhatNVVaoLam()
        {
            return nhanvien.TimNamGanNhatNVVaoLam();
        }
    }
}
