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
    public class DAL_BANGCHAMCONGTHUVIEC : KetNoi
    {

        public DataTable getBangChamCongThuViec()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MANVTV 'Mã nhân viên thử việc',THANG 'Tháng',NAM 'Năm',SONGAYCONG 'Số ngày công',SONGAYNGHI 'Số ngày nghỉ',SOGIOLAMTHEM 'Số giờ làm thêm',LUONGTV 'Lương thử việc',GHICHU 'Ghi chú' FROM BANGCHAMCONGTHUVIEC", connection);
            DataTable dtBANGCHAMCONGTHUVIEC = new DataTable();
            da.Fill(dtBANGCHAMCONGTHUVIEC);
            return dtBANGCHAMCONGTHUVIEC;
        }

        public DataTable xuatBangChamCongThuViec()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM BANGCHAMCONGTHUVIEC", connection);
            DataTable dtBANGCHAMCONGTHUVIEC = new DataTable();
            da.Fill(dtBANGCHAMCONGTHUVIEC);
            return dtBANGCHAMCONGTHUVIEC;
        }

        public bool ThemBangChamCongThuViec(DTO_BANGCHAMCONGTHUVIEC bangChamCongThuViec)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO BANGCHAMCONGTHUVIEC VALUES " +
                "('{0}', '{1}','{2}','{3}','{4}','{5}','{6}',N'{7}')"
                , bangChamCongThuViec.Manvtv, bangChamCongThuViec.Thang, bangChamCongThuViec.Nam,
                bangChamCongThuViec.Songaycong, bangChamCongThuViec.Songaynghi, bangChamCongThuViec.Sogiolamthem,
                bangChamCongThuViec.Luongtv, bangChamCongThuViec.Ghichu);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
	MANVTV INT,
	THANG INT,
	NAM INT,
	PRIMARY KEY (MANVTV, THANG, NAM),
	SONGAYCONG INT,
	SONGAYNGHI INT,
	SOGIOLAMTHEM INT,
	LUONGTV MONEY,
	GHICHU NVARCHAR(60)
 */
        public bool SuaBangChamCongThuViec(DTO_BANGCHAMCONGTHUVIEC bangChamCongThuViec)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE BANGCHAMCONGTHUVIEC " +
                "SET SONGAYCONG='{0}',SONGAYNGHI='{1}',SOGIOLAMTHEM='{2}',LUONGTV='{3}',GHICHU=N'{4}' " + 
                "WHERE MANVTV = '{5}' AND THANG ='{6}' AND NAM ='{7}'",
                bangChamCongThuViec.Songaycong, bangChamCongThuViec.Songaynghi, 
                bangChamCongThuViec.Sogiolamthem,bangChamCongThuViec.Luongtv, bangChamCongThuViec.Ghichu, 
                bangChamCongThuViec.Manvtv, bangChamCongThuViec.Thang, bangChamCongThuViec.Nam);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaBangChamCongThuViec(int manvtv, int thang, int nam)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM BANGCHAMCONGTHUVIEC WHERE MANVTV = '{0}' AND THANG ='{1}' AND NAM ='{2}'", manvtv, thang, nam);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public DataTable getBangChamCongThuViecTheoThang(string thang, string nam)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            if (thang == "")
            {
                da = new SqlDataAdapter("SELECT MANVTV 'Mã nhân viên thử việc',THANG 'Tháng',NAM 'Năm',SONGAYCONG 'Số ngày công',SONGAYNGHI 'Số ngày nghỉ',SOGIOLAMTHEM 'Số giờ làm thêm',LUONGTV 'Lương thử việc',GHICHU 'Ghi chú' FROM BANGCHAMCONGTHUVIEC WHERE NAM ='" + nam + "'", connection);
            }
            else
            {
                da = new SqlDataAdapter("SELECT MANVTV 'Mã nhân viên thử việc',THANG 'Tháng',NAM 'Năm',SONGAYCONG 'Số ngày công',SONGAYNGHI 'Số ngày nghỉ',SOGIOLAMTHEM 'Số giờ làm thêm',LUONGTV 'Lương thử việc',GHICHU 'Ghi chú' FROM BANGCHAMCONGTHUVIEC WHERE THANG ='" + thang + "' AND NAM ='" + nam + "'", connection);
            }
            DataTable dtBANGCHAMCONGTHUVIEC = new DataTable();
            da.Fill(dtBANGCHAMCONGTHUVIEC);
            return dtBANGCHAMCONGTHUVIEC;
            
        }

        public DataTable xuatBangChamCongThuViecTheoThang(string thang, string nam)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            if (thang == "")
            {
                da = new SqlDataAdapter("SELECT * FROM BANGCHAMCONGTHUVIEC WHERE NAM ='" + nam + "'", connection);
            }
            else
            {
                da = new SqlDataAdapter("SELECT * FROM BANGCHAMCONGTHUVIEC WHERE THANG ='" + thang + "' AND NAM ='" + nam + "'", connection);
            }
            DataTable dtBANGCHAMCONGTHUVIEC = new DataTable();
            da.Fill(dtBANGCHAMCONGTHUVIEC);
            return dtBANGCHAMCONGTHUVIEC;

        }
    }
}
