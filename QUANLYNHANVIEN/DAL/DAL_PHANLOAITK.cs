using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;
namespace DAL
{
    public class DAL_PHANLOAITK : KetNoi
    {
        public DataTable getPhanLoaiTK()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PHANLOAITAIKHOAN", connection);
            DataTable dtPHANLOAITK = new DataTable();
            da.Fill(dtPHANLOAITK);
            return dtPHANLOAITK;
        }

        public bool ThemPhanLoaiTK(DTO_PHANLOAITK pltk)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO PHANLOAITAIKHOAN(TENLOAITAIKHOAN,QUYENHAN) VALUES (N'{0}', N'{1}')", pltk.TENLOAITK, pltk.QUYENHAN);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool SuaPhanLoaiTK(DTO_PHANLOAITK pltk)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE PHANLOAITAIKHOAN SET TENLOAITAIKHOAN=N'{0}', QUYENHAN=N'{1}' WHERE MALOAITK = '{2}'", pltk.TENLOAITK, pltk.QUYENHAN, pltk.MALOAITK);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaPhanLoaiTK(int maltk)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM PHANLOAITAIKHOAN WHERE MALOAITK = '{0}'", maltk);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
    }
}
