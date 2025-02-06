using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DAL
{
    public class DAL_NHANVIENHIENTAI : KetNoi
    {

        public string getNhanVienHienTai()
        {
            string maNV = string.Empty;
            CheckConnection();
            string sql = string.Format("SELECT MANV FROM NHANVIENHIENTAI", maNV);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                maNV = sdr["MANV"].ToString();
            }
            connection.Close();
            return maNV;
        }

        public bool ThemNhanVienHienTai(DTO_NHANVIENHIENTAI nhanVienHienTai)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO NHANVIENHIENTAI VALUES ('{0}')"
                , nhanVienHienTai.Manv);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
    MABP VARCHAR(8) PRIMARY KEY,
	TENNHANVIENHIENTAI NVARCHAR(20),
	NGAYTHANHLAP DATETIME,
	GHICHU NVARCHAR(70)
 */
        /*public bool SuaNhanVienHienTai(DTO_NHANVIENHIENTAI nhanVienHienTai)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE NHANVIENHIENTAI " +
                "SET " + "WHERE MABP = '{3}'",
            nhanVienHienTai.Tenbophan, nhanVienHienTai.Ngaythanhlap, nhanVienHienTai.Ghichu, nhanVienHienTai.Mabp);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }*/

        public bool XoaNhanVienHienTai()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM NHANVIENHIENTAI");
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        //public bool KiemTraTonTai(string maNV)
        //{
        //    if (connection.State != ConnectionState.Open)
        //        connection.Open();
        //    string sql = string.Format("SELECT * FROM NHANVIENHIENTAI WHERE MANV='{0}' ", maNV);
        //    SqlCommand cmd = new SqlCommand(sql, connection);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read() == true)
        //    {
        //        if (!reader.IsClosed)
        //            reader.Close();
        //        return true;

        //    }
        //    if (!reader.IsClosed)
        //        reader.Close();
        //    return false;

        //}
    }
}
