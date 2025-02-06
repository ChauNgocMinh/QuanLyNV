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
    public class DAL_THAYDOIBANGLUONG : KetNoi
    {

        public DataTable getThayDoiBangLuong()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MANV 'Mã nhân viên', MALUONG 'Mã lương cũ', MALUONGMOI 'Mã lương mới', FORMAT(NGAYSUA, 'MM/dd/yyyy') 'Ngày sửa', LYDO 'Lý do' FROM THAYDOIBANGLUONG", connection);
            DataTable dtTHAYDOIBANGLUONG = new DataTable();
            da.Fill(dtTHAYDOIBANGLUONG);
            return dtTHAYDOIBANGLUONG;
        }
        public bool ThemThayDoiBangLuong(DTO_THAYDOIBANGLUONG tdbl)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO THAYDOIBANGLUONG VALUES ('{0}', '{1}','{2}','{3}',N'{4}')"
                , tdbl.Manv, tdbl.Maluong, tdbl.Maluongmoi, tdbl.Ngaysua,tdbl.Lydo);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
	MANV INT,
	MALUONG VARCHAR(8),
	MALUONGMOI VARCHAR(8),
	PRIMARY KEY (MANV, MALUONG, MALUONGMOI),
	NGAYSUA DATETIME,
	LYDO NVARCHAR(70)
 */
        public bool SuaThayDoiBangLuong(DTO_THAYDOIBANGLUONG tdbl)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE THAYDOIBANGLUONG " +
                "SET NGAYSUA='{0}', LYDO=N'{1}' " + "WHERE MANV = '{2}' AND MALUONG='{3}' AND MALUONGMOI='{4}' ",
            tdbl.Ngaysua, tdbl.Lydo,tdbl.Manv, tdbl.Maluong, tdbl.Maluongmoi);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaThayDoiBangLuong(int manv,string maluong,string maluongmoi)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM THAYDOIBANGLUONG WHERE MANV = '{0}' AND MALUONG='{1}' AND MALUONGMOI='{2}'", manv,maluong,maluongmoi);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaThayDoiBangLuongCuaNhanVien(int manv)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM THAYDOIBANGLUONG WHERE MANV = '{0}'", manv);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool KiemTraTonTaiThayDoiBangLuong(string maNV, string maLuong, string maLuongMoi)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM THAYDOIBANGLUONG " +
                "WHERE MANV = '{0}' AND MALUONG='{1}' AND MALUONGMOI='{2}' ",
            maNV, maLuong, maLuongMoi);
            
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

        public bool KiemTraTonTaiThayDoiBangLuongTheoNhanVien(string maNV)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM THAYDOIBANGLUONG " +
                "WHERE MANV = '{0}'",
            maNV);

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

        public DataTable getThayDoiBangLuongCaNhan(string manv)
        { 
            SqlDataAdapter da = new SqlDataAdapter("SELECT MANV 'Mã nhân viên', MALUONG 'Mã lương cũ', MALUONGMOI 'Mã lương mới', FORMAT(NGAYSUA, 'MM/dd/yyyy') 'Ngày sửa', LYDO 'Lý do' FROM THAYDOIBANGLUONG WHERE MANV = N'" + manv + "'", connection);
            DataTable dtBANGLUONGCANHAN = new DataTable();
            da.Fill(dtBANGLUONGCANHAN);
            return dtBANGLUONGCANHAN;
        }

        public string TimMaLuongNVThangNay(string maNV, string thang, string nam)
        {
            string maLuong = string.Empty;
            string ngayDauCuaThang, ngayDauThangsau;
            ngayDauCuaThang = "01/" + thang + "/" + nam;
            if (thang == "12")
            {
                ngayDauThangsau = "01/01/" + (int.Parse(nam + 1)).ToString();
            }
            else ngayDauThangsau = "01/" + (int.Parse(thang + 1)).ToString() + "/" + (int.Parse(nam + 1)).ToString();

            CheckConnection();
            string sql = string.Format("SELECT TOP 1 NGAYSUA, MALUONG FROM THAYDOIBANGLUONG WHERE MANV = '{0}' AND NGAYSUA < '{1}' AND NGAYSUA >= '{3}' ORDER BY NGAYSUA DESC", maNV, ngayDauCuaThang, ngayDauThangsau);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                maLuong = sdr["MALUONG"].ToString();
            }
            connection.Close();
            return maLuong;
        }
    }
}
