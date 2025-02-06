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
    public class DAL_BAOCAOLUONG : KetNoi
    {
        /*	THANG INT,
	NAM INT,
	TONGTIEN MONEY,
	PRIMARY KEY (THANG, NAM),
	GHICHU NVARCHAR(50)*/
        public DataTable getBaoCaoLuong()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM BAOCAOLUONG", connection);
            DataTable dtBAOCAOLUONG = new DataTable();
            da.Fill(dtBAOCAOLUONG);
            return dtBAOCAOLUONG;
        }
        public bool ThemBaoCaoLuong(DTO_BAOCAOLUONG baoCaoLuong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO BAOCAOLUONG VALUES ('{0}','{1}',N'{2}')"
                , baoCaoLuong.Thang, baoCaoLuong.Nam, baoCaoLuong.Ghichu);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        public bool SuaBaoCaoLuong(DTO_BAOCAOLUONG baoCaoLuong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE BAOCAOLUONG " +
                "SET GHICHU=N'{0}'" + "WHERE THANG = '{1}' AND NAM = '{2}' ",
            baoCaoLuong.Ghichu, baoCaoLuong.Thang, baoCaoLuong.Nam);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaBaoCaoLuong(int thang, int nam)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM BAOCAOLUONG WHERE THANG = '{0}' AND NAM = '{1}'", thang,nam);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
    }
}
