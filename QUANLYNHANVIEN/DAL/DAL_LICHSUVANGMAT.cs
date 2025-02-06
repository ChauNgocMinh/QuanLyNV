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
    public class DAL_LICHSUVANGMAT : KetNoi
    {
        /*			MANV INT,
	NGAYNGHI DATETIME,
	PRIMARY KEY (MANV, NGAYNGHI),
	GHICHU NVARCHAR(50)*/
        public DataTable getLichSuVangMat(string maNV)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MANV 'Mã nhân viên', FORMAT(NGAYNGHI, 'MM/dd/yyyy') 'Ngày nghỉ', GHICHU 'Ghi chú' FROM LICHSUVANGMAT WHERE MANV = '" + maNV + "'", connection);
            DataTable dtLICHSUVANGMAT = new DataTable();
            da.Fill(dtLICHSUVANGMAT);
            return dtLICHSUVANGMAT;
        }
        public bool ThemLichSuVangMat(DTO_LICHSUVANGMAT lichSuVangMat)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO LICHSUVANGMAT VALUES ('{0}','{1}',N'{2}')"
                , lichSuVangMat.Manv, lichSuVangMat.Ngaynghi, lichSuVangMat.Ghichu);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        public bool SuaLichSuVangMat(DTO_LICHSUVANGMAT lichSuVangMat)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE LICHSUVANGMAT " +
                "SET NGAYNGHI = '{0}', GHICHU=N'{1}'" + "WHERE MANV = '{2}' ",
            lichSuVangMat.Ngaynghi, lichSuVangMat.Ghichu, lichSuVangMat.Manv);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaLichSuVangMat(int manv)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM LICHSUVANGMAT WHERE MANV = '{0}'", manv);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public int DemSoNgayNghiTrongThang(int maNV, int thang, int nam)
        {
            int soNgayNghi = 0;
            CheckConnection();
            string sql = string.Format("SELECT COUNT(NGAYNGHI) 'SONGAYNGHI' FROM LICHSUVANGMAT WHERE MANV = '{0}' AND MONTH(NGAYNGHI) = '{1}' AND YEAR(NGAYNGHI) = '{2}'", maNV.ToString(), thang.ToString(), nam.ToString());

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                soNgayNghi = int.Parse(sdr["SONGAYNGHI"].ToString());
            }
            connection.Close();
            return soNgayNghi;
        }

        public bool KiemTraTonTaiNhanVien(string maNV)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM LICHSUVANGMAT WHERE MANV='{0}'", maNV);
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
    }
}
