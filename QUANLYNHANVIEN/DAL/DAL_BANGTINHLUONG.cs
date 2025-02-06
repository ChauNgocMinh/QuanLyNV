using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class DAL_BANGTINHLUONG : KetNoi
    {
        /*		MANV INT,
	LUONG MONEY,
	THANG INT,
	NAM INT,
	PRIMARY KEY (MANV, THANG, NAM),
	GHICHU NVARCHAR(80)*/
        public DataTable getBangTinhLuong()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MANV 'Mã nhân viên', LUONG 'Lương', THANG 'Tháng', NAM 'Năm', GHICHU 'Ghi chú' FROM BANGTINHLUONG", connection);
            DataTable dtBANGTINHLUONG = new DataTable();
            da.Fill(dtBANGTINHLUONG);
            return dtBANGTINHLUONG;
        }
        public bool ThemBangTinhLuong(DTO_BANGTINHLUONG bangTinhLuong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO BANGTINHLUONG VALUES ('{0}','{1}','{2}','{3}',N'{4}')",
                bangTinhLuong.Manv, bangTinhLuong.Luong, bangTinhLuong.Thang, bangTinhLuong.Nam, bangTinhLuong.Ghichu);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        public bool SuaBangTinhLuong(DTO_BANGTINHLUONG bangTinhLuong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE BANGTINHLUONG " +
                "SET LUONG='{0}' ,GHICHU=N'{1}'" + "WHERE MANV = '{2}' AND THANG = '{3}' AND NAM = '{4}' ",
            bangTinhLuong.Luong, bangTinhLuong.Ghichu, bangTinhLuong.Manv, bangTinhLuong.Thang, bangTinhLuong.Nam);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaBangTinhLuong(int manv, int thang, int nam)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM BANGTINHLUONG WHERE MANV = '{0}' AND THANG = '{1}' AND NAM = '{2}' ",manv, thang, nam);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public DataTable getBangTinhLuongTheoThang(string thang, string nam)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            if (thang == "")
            {
                da = new SqlDataAdapter("SELECT MANV 'Mã nhân viên', LUONG 'Lương', THANG 'Tháng', NAM 'Năm', GHICHU 'Ghi chú' FROM BANGTINHLUONG WHERE NAM ='" + nam + "'", connection);
            }    
            else
            {
                da = new SqlDataAdapter("SELECT MANV 'Mã nhân viên', LUONG 'Lương', THANG 'Tháng', NAM 'Năm', GHICHU 'Ghi chú' FROM BANGTINHLUONG WHERE THANG ='" + thang + "' AND NAM ='" + nam + "'", connection);
            }
            DataTable dtBANGTINHLUONG = new DataTable();
            da.Fill(dtBANGTINHLUONG);
            return dtBANGTINHLUONG;
        }

        public bool KiemTraTonTaiNhanVien(string maNV)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM BANGTINHLUONG WHERE MANV='{0}'", maNV);
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() == true)
            {
                if (!reader.IsClosed)
                    reader.Close();
                return true;
            }
            if (!reader.IsClosed)
                reader.Close();
            return false;
        }

        public bool SuaGhiChu(string ghiChu, string maNV)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE BANGTINHLUONG " +
                "SET GHICHU=N'{0}' WHERE MANV = '{1}'", ghiChu, maNV);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
    }
}
