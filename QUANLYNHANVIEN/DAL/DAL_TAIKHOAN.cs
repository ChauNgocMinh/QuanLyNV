using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DTO;
namespace DAL
{
    public class DAL_TAIKHOAN : KetNoi
    {
        public DataTable getTaiKhoan()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MATK 'Mã tài khoản', MALOAITK 'Mã loại tài khoản', TENCHUTAIKHOAN 'Tên chủ tài khoản', TENDANGNHAP 'Tên đăng nhập', MATKHAU 'Mật khẩu mã hóa' FROM TAIKHOAN", connection);
            DataTable dtTAIKHOAN = new DataTable();
            da.Fill(dtTAIKHOAN);
            return dtTAIKHOAN;
        }
        public bool ThemTaikhoan(DTO_TAIKHOAN tk)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            Hash256 h = new Hash256();
            SHA256 sha256Hash = SHA256.Create();
            string hash = h.GetHash(sha256Hash, tk._MATKHAU);
            string sql = string.Format("INSERT INTO TAIKHOAN(MALOAITK,TENCHUTAIKHOAN, TENDANGNHAP, MATKHAU) VALUES ('{0}', N'{1}', '{2}','{3}')", tk._MALOAITK, tk._TENCHUTAIKHOAN, tk._TENDANGNHAP, hash);
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                if (!reader.IsClosed)
                    reader.Close();
                return true;
            }
            else
            {
                if (!reader.IsClosed)
                    reader.Close();
                return false;
            }
            connection.Close();
        }

        public bool SuaTaiKhoan(DTO_TAIKHOAN tk)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            Hash256 h = new Hash256();
            SHA256 sha256Hash = SHA256.Create();
            string hash = h.GetHash(sha256Hash, tk._MATKHAU);
            string sql = string.Format("UPDATE TAIKHOAN SET MALOAITK='{0}', TENCHUTAIKHOAN=N'{1}', TENDANGNHAP='{2}', MATKHAU='{3}' WHERE MATK = '{4}'", tk._MALOAITK, tk._TENCHUTAIKHOAN, tk._TENDANGNHAP, hash, tk._MATK);
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
            connection.Close();
        }

        public bool XoaTaiKhoan(int tenDangNhap)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM TAIKHOAN WHERE TENDANGNHAP = '{0}'", tenDangNhap);
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
            connection.Close();
        }


        public bool KiemTraTaiKhoan(DTO_TAIKHOAN tk)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            Hash256 h = new Hash256();
            SHA256 sha256Hash = SHA256.Create();
            string hash = h.GetHash(sha256Hash, tk._MATKHAU);
            string sql = string.Format("SELECT * FROM TAIKHOAN WHERE TENDANGNHAP='{0}' AND MATKHAU='{1}'", tk._TENDANGNHAP, hash);
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                tk._TENCHUTAIKHOAN = reader[2].ToString();
                tk._MALOAITK = Convert.ToInt32(reader[1]);
                if (!reader.IsClosed)
                    reader.Close();
                return true;
            }
            else
            {
                if (!reader.IsClosed)
                    reader.Close();
                return false;
            }
            connection.Close();
        }

        public bool KiemTraTonTai(DTO_TAIKHOAN tk)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM TAIKHOAN WHERE TENDANGNHAP='{0}' AND TENCHUTAIKHOAN=N'{1}'", tk._TENDANGNHAP, tk._TENCHUTAIKHOAN);
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() == true)
            {
                tk._MATK = Convert.ToInt32(reader[0].ToString());
                tk._MALOAITK = Convert.ToInt32(reader[1].ToString());
                if (!reader.IsClosed)
                    reader.Close();
                return true;
            }
            if (!reader.IsClosed)
                reader.Close();
            return false;

        }
        public bool KiemTraTonTai(string tenDangNhap)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM TAIKHOAN WHERE TENDANGNHAP='{0}' ", tenDangNhap);
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

        public bool LayMatKhau(DTO_TAIKHOAN tk)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM TAIKHOAN WHERE MATK='{0}' ", tk._MATK);
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() == true)
            {

                Hash256 h = new Hash256();
                SHA256 sha256Hash = SHA256.Create();
                string hash = h.GetHash(sha256Hash,reader[4].ToString());
                tk._MATKHAU = hash;
                if (!reader.IsClosed)
                    reader.Close();
                return true;

            }
            if (!reader.IsClosed)
                reader.Close();
            return false;

        }
    }
}
