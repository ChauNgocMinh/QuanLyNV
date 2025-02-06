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
    public class DAL_LOAINHANVIEN : KetNoi
    {

        public DataTable getLoaiNhanVien()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM LOAINHANVIEN", connection);
            DataTable dtLOAINHANVIEN = new DataTable();
            da.Fill(dtLOAINHANVIEN);
            return dtLOAINHANVIEN;
        }
        public bool ThemLoaiNhanVien(DTO_LOAINHANVIEN loaiNhanVien)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO LOAINHANVIEN VALUES ('{0}', N'{1}','{2}'"
                , loaiNhanVien.Maloainv, loaiNhanVien.Tenloainv, loaiNhanVien.Mucluongcoban);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
	MALOAINV VARCHAR(10) PRIMARY KEY,
	TENLOAINV NVARCHAR(30),
	MUCLUONGCOBAN MONEY,
 */
        public bool SuaLoaiNhanVien(DTO_LOAINHANVIEN loaiNhanVien)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE LOAINHANVIEN " +
                "SET TENLOAINV=N'{0}', MUCLUONGCOBAN='{1}'" + "WHERE MALOAINV = '{2}'",
            loaiNhanVien.Tenloainv, loaiNhanVien.Mucluongcoban, loaiNhanVien.Maloainv);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaLoaiNhanVien(string maloainv)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM LOAINHANVIEN WHERE MALOAINV = '{0}'", maloainv);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public string TimKiemTheoLoaiNhanVien(string loaiNV)
        {
            string maLoaiNV = string.Empty;
            CheckConnection();
            string sql = string.Format("SELECT * FROM LOAINHANVIEN WHERE TENLOAINV = N'{0}'", loaiNV);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                maLoaiNV = sdr["MALOAINV"].ToString();
            }
            connection.Close();
            return maLoaiNV;
        }

        public string TimKiemTheoMaLoaiNhanVien(string maLoaiNV)
        {
            string loaiNV = string.Empty;
            CheckConnection();
            string sql = string.Format("SELECT * FROM LOAINHANVIEN WHERE MALOAINV = N'{0}'", maLoaiNV);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                loaiNV = sdr["TENLOAINV"].ToString();
            }
            connection.Close();
            return loaiNV;
        }

        public List<string> TongHopLoaiNhanVien()
        {
            List<string> listLoaiNhanVien = new List<string>();
            CheckConnection();
            string sql = string.Format("SELECT TENLOAINV FROM LOAINHANVIEN");

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                listLoaiNhanVien.Add(sdr[0].ToString());
            }
            connection.Close();
            return listLoaiNhanVien;
        }
    }
}
