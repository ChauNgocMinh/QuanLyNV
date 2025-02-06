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
    public class DAL_KYLUAT : KetNoi
    {

        public DataTable getKyLuat()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MAKL 'Mã kỷ luật', TIEN 'Tiền', LYDO 'Lý do' FROM KYLUAT", connection);
            DataTable dtKYLUAT = new DataTable();
            da.Fill(dtKYLUAT);
            return dtKYLUAT;
        }
        public bool ThemKyLuat(DTO_KYLUAT kyLuat)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO KYLUAT VALUES ('{0}', N'{1}')"
                , kyLuat.Tien, kyLuat.Lydo);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
	MAKL INT PRIMARY KEY,
	TIEN MONEY,
	LYDO NVARCHAR(50)
 */
        public bool SuaKyLuat(DTO_KYLUAT kyLuat)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE KYLUAT " +
                "SET TIEN='{0}', LYDO=N'{1}'" + "WHERE MAKL = '{2}'",
            kyLuat.Tien, kyLuat.Lydo, kyLuat.Makl);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaKyLuat(int makl)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM KYLUAT WHERE MAKL = '{0}'", makl);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public List<string> TongHopMaKyLuat()
        {
            List<string> listMaKyLuat = new List<string>();
            CheckConnection();
            string sql = string.Format("SELECT MAKL FROM KYLUAT");

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                listMaKyLuat.Add(sdr[0].ToString());
            }
            connection.Close();
            return listMaKyLuat;
        }
    }
}
