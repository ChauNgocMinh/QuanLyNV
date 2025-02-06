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
    public class DAL_KHENTHUONG : KetNoi
    {

        public DataTable getKhenThuong()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MAKT 'Mã khen thưởng', TIEN 'Tiền', LYDO 'Lý do' FROM KHENTHUONG", connection);
            DataTable dtKHENTHUONG = new DataTable();
            da.Fill(dtKHENTHUONG);
            return dtKHENTHUONG;
        }
        public bool ThemKhenThuong(DTO_KHENTHUONG khenThuong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO KHENTHUONG VALUES ('{0}', N'{1}')"
                , khenThuong.Tien, khenThuong.Lydo);
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
        public bool SuaKhenThuong(DTO_KHENTHUONG khenThuong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE KHENTHUONG " +
                "SET TIEN='{0}', LYDO=N'{1}'" + "WHERE MAKT = '{2}'", khenThuong.Tien, khenThuong.Lydo, khenThuong.Makt);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaKhenThuong(int makt)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM KHENTHUONG WHERE MAKT = '{0}'", makt);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public List<string> TongHopMaKhenThuong()
        {
            List<string> listMaKhenThuong = new List<string>();
            CheckConnection();
            string sql = string.Format("SELECT MAKT FROM KHENTHUONG");

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                listMaKhenThuong.Add(sdr[0].ToString());
            }
            connection.Close();
            return listMaKhenThuong;
        }
    }
}
