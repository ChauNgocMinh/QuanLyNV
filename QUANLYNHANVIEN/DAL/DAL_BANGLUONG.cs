using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_BANGLUONG : KetNoi
    {

        public DataTable getBangLuong()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MALUONG 'Mã lương', LCB 'Lương cơ bản', PHUCAPCHUCVU 'Phụ cấp chức vụ', PHUCAPKHAC 'Phụ cấp khác', GHICHU 'Ghi chú' FROM BANGLUONG", connection);
            DataTable dtBANGLUONG = new DataTable();
            da.Fill(dtBANGLUONG);
            return dtBANGLUONG;
        }
        public bool ThemBangLuong(DTO_BANGLUONG bangLuong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO BANGLUONG VALUES ('{0}', '{1}', '{2}', '{3}', N'{4}')"
                , bangLuong.Maluong, bangLuong.Lcb, bangLuong.Phucapchucvu, bangLuong.Phucapkhac, bangLuong.Ghichu);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
    MALUONG VARCHAR(8) PRIMARY KEY,
	LCB MONEY,
	PHUCAPCHUCVU MONEY,
	PHUCAPKHAC MONEY,
	GHICHU NVARCHAR(80)
 */
        public bool SuaBangLuong(DTO_BANGLUONG bangLuong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE BANGLUONG " +
                "SET LCB='{0}',PHUCAPCHUCVU='{1}',PHUCAPKHAC='{2}',GHICHU=N'{3}'" + "WHERE MALUONG = '{4}'",
            bangLuong.Lcb, bangLuong.Phucapchucvu, bangLuong.Phucapkhac, bangLuong.Ghichu, bangLuong.Maluong);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaBangLuong(string maluong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM BANGLUONG WHERE MALUONG = '{0}'", maluong);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public List<string> TongHopMaLuong()
        {
            List<string> listMaLuong = new List<string>();
            CheckConnection();
            string sql = string.Format("SELECT MALUONG FROM BANGLUONG");

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                listMaLuong.Add(sdr[0].ToString());
            }
            connection.Close();
            return listMaLuong;
        }

        public DTO_BANGLUONG GetChiTietLuong(string maluong)
        {
            DTO_BANGLUONG dtoBangLuong = new DTO_BANGLUONG();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM BANGLUONG WHERE MALUONG='{0}'", maluong);
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                dtoBangLuong.Maluong = maluong;
                dtoBangLuong.Lcb = Convert.ToDouble(reader[1].ToString());
                dtoBangLuong.Phucapchucvu = Convert.ToDouble(reader[2].ToString());
                dtoBangLuong.Phucapkhac = Convert.ToDouble(reader[3].ToString());
                if (!reader.IsClosed)
                    reader.Close();
                return dtoBangLuong;
            }
            else
            {
                if (!reader.IsClosed)
                    reader.Close();
                return dtoBangLuong;
            }
        }
    }
}
