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

            string sql = "INSERT INTO LICHSUCHAMCONG (Manv, Ngaychamconggannhat, Ghichu) VALUES (@Manv, @Ngaychamconggannhat, @Ghichu)";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@Manv", lichSuChamCong.Manv);
                cmd.Parameters.AddWithValue("@Ngaychamconggannhat", lichSuChamCong.Ngaychamconggannhat);
                cmd.Parameters.AddWithValue("@Ghichu", lichSuChamCong.Ghichu ?? (object)DBNull.Value);

                bool success = cmd.ExecuteNonQuery() > 0;
                connection.Close();
                return success;
            }
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
                "SET NGAYCHAMCONGGANNHAT ='{0}',GHICHU = N'{1}'" + "WHERE MANV = '{2}'",
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
            DateTime lanChamCongGanNhat = DateTime.MinValue; 
            CheckConnection();
            string sql = $"SELECT NGAYCHAMCONGGANNHAT FROM LICHSUCHAMCONG WHERE MANV = '{maNV}'";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    DateTime ngayChamCong = DateTime.Parse(sdr["NGAYCHAMCONGGANNHAT"].ToString());
                    if (ngayChamCong.Date == DateTime.Now.Date)
                    {
                        lanChamCongGanNhat = ngayChamCong;
                    }
                }
            }
            connection.Close();
            return lanChamCongGanNhat;
        }


        public bool CheckChamCong(string maNV)
        {
            CheckConnection();
            string sql = $@"
                SELECT 1 
                FROM LICHSUCHAMCONG 
                WHERE MANV = '{maNV}' 
                AND (GHICHU = N'Đã chốt' OR NGAYCHAMCONGGANNHAT > DATEADD(HOUR, -1, GETDATE()))";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                bool hasData = sdr.Read();
                connection.Close();
                return hasData;
            }
        }


    }
}
