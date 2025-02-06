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
    public class DAL_PHONGBAN : KetNoi
    {

        public DataTable getPhongBan()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MAPHONG 'Mã phòng', MABP 'Mã bộ phận', TENPHONG 'Tên phòng', FORMAT(NGAYTHANHLAP, 'MM/dd/yyyy') 'Ngày thành lập', GHICHU 'Ghi chú' FROM PHONGBAN", connection);
            DataTable dtPHONGBAN = new DataTable();
            da.Fill(dtPHONGBAN);
            return dtPHONGBAN;
        }
        public bool ThemPhongBan(DTO_PHONGBAN phongBan)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO PHONGBAN VALUES ('{0}', '{1}',N'{2}','{3}',N'{4}')"
                , phongBan.Maphong, phongBan.Mabp, phongBan.Tenphong, phongBan.Ngaythanhlap,phongBan.Ghichu);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
	MAPHONG VARCHAR(6) PRIMARY KEY,
	MABP VARCHAR(8),
	TENPHONG NVARCHAR(20),
	NGAYTHANHLAP DATETIME,
	GHICHU NVARCHAR(70)
 */
        public bool SuaPhongBan(DTO_PHONGBAN phongBan)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE PHONGBAN " +
                "SET MABP='{0}' ,TENPHONG=N'{1}', NGAYTHANHLAP='{2}',GHICHU=N'{3}'" + "WHERE MAPHONG = '{4}'",
            phongBan.Mabp,phongBan.Tenphong, phongBan.Ngaythanhlap, phongBan.Ghichu, phongBan.Maphong);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaPhongBan(string maphong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM PHONGBAN WHERE MAPHONG = '{0}'", maphong);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public string TimKiemMaPhongBan(string tenPhong)
        {
            string maPhong = string.Empty;
            CheckConnection();
            string sql = string.Format("SELECT * FROM PHONGBAN WHERE TENPHONG = N'{0}'", tenPhong);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                maPhong = sdr["MAPHONG"].ToString();
            }
            connection.Close();
            return maPhong;
        }

        public string TimKiemTenPhongBanTheoMa(string maPhong)
        {
            string tenPhong = string.Empty;
            CheckConnection();
            string sql = string.Format("SELECT * FROM PHONGBAN WHERE MAPHONG = N'{0}'", maPhong);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                tenPhong = sdr["TENPHONG"].ToString();
            }
            connection.Close();
            return tenPhong;
        }

        public string TimKiemBoPhanTheoPhong(string maPhong)
        {
            string tenPhong = string.Empty;
            CheckConnection();
            string sql = string.Format("SELECT * FROM PHONGBAN WHERE MAPHONG = N'{0}'", maPhong);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                tenPhong = sdr["MABP"].ToString();
            }
            connection.Close();
            return tenPhong;
        }

        public List<string> TongHopPhongBan(string maBP)
        {
            List<string> listPhongBan = new List<string>();
            CheckConnection();
            string sql;
            if (maBP == "")
                sql = string.Format("SELECT TENPHONG FROM PHONGBAN");
            else
                sql = string.Format("SELECT TENPHONG FROM PHONGBAN WHERE MABP = N'{0}'", maBP);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                listPhongBan.Add(sdr[0].ToString());
            }
            connection.Close();
            return listPhongBan;
        }
        public List<string> TongHopMaPhongBan()
        {
            List<string> listMaPhongBan = new List<string>();
            CheckConnection();
            string sql = string.Format("SELECT MAPHONG FROM PHONGBAN");

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                listMaPhongBan.Add(sdr[0].ToString());
            }
            connection.Close();
            return listMaPhongBan;
        }
    }
}
