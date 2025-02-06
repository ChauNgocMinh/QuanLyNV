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
    public class DAL_SOBH : KetNoi
    {

        public DataTable getSoBH()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MABH 'Mã bảo hiểm', MANV 'Mã nhân viên', FORMAT(NGAYCAPSO, 'MM/dd/yyyy') 'Ngày cấp sổ', NOICAPSO 'Nơi cấp sổ', GHICHU 'Ghi chú' FROM SOBH", connection);
            DataTable dtSOBH = new DataTable();
            da.Fill(dtSOBH);
            return dtSOBH;
        }
        public bool ThemSoBH(DTO_SOBH soBH)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO SOBH VALUES ('{0}', '{1}','{2}',N'{3}')"
                , soBH.Manv, soBH.Ngaycapso, soBH.Noicapso, soBH.Ghichu);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
	MABH INT IDENTITY(1,1) PRIMARY KEY,
	MANV INT,
	NGAYCAPSO DATETIME,
	NOICAPSO NVARCHAR(20),
	GHICHU NVARCHAR(70),
 */
        public bool SuaSoBH(DTO_SOBH soBH)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE SOBH " +
                "SET MANV='{0}', NGAYCAPSO='{1}',NOICAPSO='{2}',GHICHU=N'{3}'"  + "WHERE MABH = '{4}'",
            soBH.Manv, soBH.Ngaycapso, soBH.Noicapso, soBH.Ghichu, soBH.Mabh);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaSoBH(int mabh)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM SOBH WHERE MABH = '{0}'", mabh);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool KiemTraTonTaiNhanVien(string maNV)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM SOBH WHERE MANV='{0}'", maNV);
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
            string sql = string.Format("UPDATE SOBH " +
                "SET GHICHU=N'{0}' WHERE MANV = '{1}'", ghiChu, maNV);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
    }
}
