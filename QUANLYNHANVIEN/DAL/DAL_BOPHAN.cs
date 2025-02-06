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
    public class DAL_BOPHAN : KetNoi
    {
        public DataTable getBoPhan()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MABP 'Mã bộ phận', TENBOPHAN 'Tên bộ phận', FORMAT(NGAYTHANHLAP, 'MM/dd/yyyy') 'Ngày thành lập', GHICHU 'Ghi chú' FROM BOPHAN", connection);
            DataTable dtBOPHAN = new DataTable();
            da.Fill(dtBOPHAN);
            return dtBOPHAN;
        }
        public bool ThemBoPhan(DTO_BOPHAN boPhan)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO BOPHAN VALUES ('{0}', N'{1}','{2}',N'{3}')"
                , boPhan.Mabp, boPhan.Tenbophan, boPhan.Ngaythanhlap, boPhan.Ghichu);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
    MABP VARCHAR(8) PRIMARY KEY,
	TENBOPHAN NVARCHAR(20),
	NGAYTHANHLAP DATETIME,
	GHICHU NVARCHAR(70)
 */
        public bool SuaBoPhan(DTO_BOPHAN boPhan)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE BOPHAN " +
                "SET TENBOPHAN=N'{0}', NGAYTHANHLAP='{1}',GHICHU=N'{2}'" + "WHERE MABP = '{3}'",
            boPhan.Tenbophan, boPhan.Ngaythanhlap, boPhan.Ghichu, boPhan.Mabp);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaBoPhan(string mabp)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM BOPHAN WHERE MABP = '{0}'", mabp);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }


        public string TimKiemTheoTenBoPhan(string tenBP)
        {
            string maBP = string.Empty;
            CheckConnection();
            string sql = string.Format("SELECT * FROM BOPHAN WHERE TENBOPHAN = N'{0}'", tenBP);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                maBP = sdr["MABP"].ToString();
            }
            connection.Close();
            return maBP;
        }

        public string TimKiemTheoMaBoPhan(string maBP)
        {
            string tenBP = string.Empty;
            CheckConnection();
            string sql = string.Format("SELECT * FROM BOPHAN WHERE MABP = N'{0}'", maBP);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                tenBP = sdr["TENBOPHAN"].ToString();
            }
            connection.Close();
            return tenBP;
        }

        public List<string> TongHopTenBoPhan()
        {
            List<string> listTenBoPhan = new List<string>();
            CheckConnection();
            string sql = string.Format("SELECT TENBOPHAN FROM BOPHAN");

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                listTenBoPhan.Add(sdr[0].ToString());
            }
            connection.Close();
            return listTenBoPhan;
        }
        public List<string> TongHopMaBoPhan()
        {
            List<string> listMaBoPhan = new List<string>();
            CheckConnection();
            string sql = string.Format("SELECT MABP FROM BOPHAN");

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                listMaBoPhan.Add(sdr[0].ToString());
            }
            connection.Close();
            return listMaBoPhan;
        }
    }
}
