using System;
using DAL;
using DTO;
using System.Data;
namespace BUS
{
    public class BUS_TAIKHOAN
    {
        DAL_TAIKHOAN taikhoan = new DAL_TAIKHOAN();

        public DataTable getTaiKhoan()
        {
            return taikhoan.getTaiKhoan();
        }

        public bool ThemTaiKhoan(DTO_TAIKHOAN tk)
        {
            return taikhoan.ThemTaikhoan(tk);
        }

        public bool SuaTaiKhoan(DTO_TAIKHOAN tk)
        {
            return taikhoan.SuaTaiKhoan(tk);
        }

        public bool XoaTaiKhoan(int tenDangNhap)
        {
            return taikhoan.XoaTaiKhoan(tenDangNhap);
        }

        public bool KiemTraTaiKhoan(DTO_TAIKHOAN tk)
        {
            return taikhoan.KiemTraTaiKhoan(tk);
        }
        public bool KiemTraTonTai(DTO_TAIKHOAN tk)
        {
            return taikhoan.KiemTraTonTai(tk);
        }
        public bool KiemTraTonTai(string tenDangNhap)
        {
            return taikhoan.KiemTraTonTai(tenDangNhap);
        }

        public bool LayMatKhau(DTO_TAIKHOAN tk)
        {
            return taikhoan.LayMatKhau(tk);
        }
    }
}
