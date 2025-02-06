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
    public class DAL_LICHSUCHAMCONG : KetNoi
    {

        public DataTable getLichSuChamCong()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM LICHSUCHAMCONG", connection);
            DataTable dtLICHSUCHAMCONG = new DataTable();
            da.Fill(dtLICHSUCHAMCONG);
            return dtLICHSUCHAMCONG;
        }

        public bool ThemLichSuChamCong(DTO_LICHSUCHAMCONG lichSuChamCong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO LICHSUCHAMCONG VALUES ('{0}','{1}', N'{2}')"
                , lichSuChamCong.Manv, lichSuChamCong.Ngaychamconggannhat, lichSuChamCong.Ghichu);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
CREATE TABLE LICHSUCHAMCONG
(
	MALSCHAMCONG INT IDENTITY(1,1) PRIMARY KEY,
	MANV INT,
	NGAYCHAMCONGGANNHAT DATETIME,
	GHICHU NVARCHAR(50)
)
 */
        public bool SuaLichSuChamCong(DTO_LICHSUCHAMCONG lichSuChamCong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE LICHSUCHAMCONG " +
                "SET NGAYCHAMCONGGANNHAT ='{0}',GHICHU = '{1}'" + "WHERE MANV = '{2}'",
            lichSuChamCong.Ngaychamconggannhat, lichSuChamCong.Ghichu,lichSuChamCong.Manv);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaLichSuChamCong(int maNV)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM LICHSUCHAMCONG WHERE MANV = '{0}'", maNV);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool KiemTraChamCong(string maNV, string ngayLamTruoc)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM LICHSUCHAMCONG WHERE MANV = '{0}' AND NGAYCHAMCONGGANNHAT = '{1}'", maNV, ngayLamTruoc);
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

        public bool KiemTraTonTai(string maNV)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM LICHSUCHAMCONG WHERE MANV = '{0}'", maNV);
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

        public DateTime TimLanCuoiChamCongTheoMa(string maNV)
        {
            DateTime lanChamCongGanNhat = new DateTime();
            CheckConnection();
            string sql = string.Format("SELECT NGAYCHAMCONGGANNHAT FROM LICHSUCHAMCONG WHERE MANV = '{0}'", maNV);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lanChamCongGanNhat = DateTime.Parse(sdr["NGAYCHAMCONGGANNHAT"].ToString());
            }
            connection.Close();
            return lanChamCongGanNhat;
        }
    }
}
