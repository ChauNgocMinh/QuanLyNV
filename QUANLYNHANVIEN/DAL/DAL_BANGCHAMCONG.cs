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
    public class DAL_BANGCHAMCONG : KetNoi
    {

        public DataTable getBangChamCong()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT BANGCHAMCONG.MANV 'Mã nhân viên', HOTEN 'Họ tên', THANG 'Tháng', NAM 'Năm', BANGCHAMCONG.MALUONG 'Mã lương', TIENKHENTHUONG 'Tiền khen thưởng', TIENKYLUAT 'Tiền kỷ luật', SONGAYCONG 'Số ngày công', SONGAYNGHI 'Số ngày nghỉ', SOGIOLAMTHEM 'Số giờ làm thêm', BANGCHAMCONG.GHICHU 'Ghi chú' FROM BANGCHAMCONG, NHANVIEN WHERE BANGCHAMCONG.MANV = NHANVIEN.MANV", connection);
            DataTable dtBANGCHAMCONG = new DataTable();
            da.Fill(dtBANGCHAMCONG);
            return dtBANGCHAMCONG;
        }

        public DataTable xuatBangChamCong()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM BANGCHAMCONG, NHANVIEN WHERE BANGCHAMCONG.MANV = NHANVIEN.MANV", connection);
            DataTable dtBANGCHAMCONG = new DataTable();
            da.Fill(dtBANGCHAMCONG);
            return dtBANGCHAMCONG;
        }

        public bool ThemBangChamCong(DTO_BANGCHAMCONG bangChamCong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO BANGCHAMCONG VALUES " +
                "('{0}', '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',N'{9}')"
                , bangChamCong.Manv, bangChamCong.Thang, bangChamCong.Nam, 
                bangChamCong.Maluong, bangChamCong.Tienkhenthuong, bangChamCong.Tienkyluat,
                bangChamCong.Songaycong,bangChamCong.Songaynghi,bangChamCong.Sogiolamthem,bangChamCong.Ghichu);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
	MANV INT,
	THANG INT,
	NAM INT,
	PRIMARY KEY (MANV, THANG, NAM),
	MALUONG VARCHAR(8),
	MAKT INT,
	MAKL INT,
	SONGAYCONG INT,
	SONGAYNGHI INT,
	SOGIOLAMTHEM INT,
	GHICHU NVARCHAR(60)
 */
        public bool SuaBangChamCong(DTO_BANGCHAMCONG bangChamCong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE BANGCHAMCONG " +
                "SET MALUONG='{0}', TIENKHENTHUONG='{1}',TIENKYLUAT='{2}',SONGAYCONG='{3}'" +
               ",SONGAYNGHI='{4}',SOGIOLAMTHEM='{5}',GHICHU=N'{6}' " + "WHERE MANV = '{7}' AND THANG ='{8}' AND NAM ='{9}'",
               bangChamCong.Maluong, bangChamCong.Tienkhenthuong, bangChamCong.Tienkyluat,
                bangChamCong.Songaycong, bangChamCong.Songaynghi, bangChamCong.Sogiolamthem, bangChamCong.Ghichu, bangChamCong.Manv, bangChamCong.Thang, bangChamCong.Nam);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaBangChamCong(int manv,int thang,int nam)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM BANGCHAMCONG WHERE MANV = '{0}' AND THANG ='{1}' AND NAM ='{2}'", manv,thang,nam);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public DTO_BANGCHAMCONG getBangChamCongTheoNhanVien(string maNV, int thang, int nam)
        {
            DTO_BANGCHAMCONG dtoBangChamCong = new DTO_BANGCHAMCONG();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT SONGAYCONG, SONGAYNGHI, SOGIOLAMTHEM FROM BANGCHAMCONG"+
                " WHERE MANV='{0}' AND THANG='{1}' AND NAM ='{2}'", maNV, thang, nam);
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                dtoBangChamCong.Songaycong = Convert.ToInt32(reader[0].ToString());
                dtoBangChamCong.Songaynghi = Convert.ToInt32(reader[1].ToString());
                dtoBangChamCong.Sogiolamthem = Convert.ToInt32(reader[2].ToString());

                if (!reader.IsClosed)
                    reader.Close();
                return dtoBangChamCong;
            }
            else
            {
                if (!reader.IsClosed)
                    reader.Close();
                return dtoBangChamCong;
            }
        }

        public DataTable getBangChiTietChamCongTheoNhanVien(string maNV, string thang, string nam)
        {
            DataTable dtBANGCHAMCONG = new DataTable();
            if (thang == "" && nam == "")
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT LUONG 'Lương', BANGCHAMCONG.THANG 'Tháng', BANGCHAMCONG.NAM 'Năm', BANGCHAMCONG.MALUONG 'Mã lương', TIENKHENTHUONG 'Tiền khen thưởng', TIENKYLUAT 'Tiền kỷ luật', SONGAYCONG 'Số ngày công', SONGAYNGHI 'Số ngày nghỉ', SOGIOLAMTHEM 'Số giờ làm thêm', BANGCHAMCONG.GHICHU 'Ghi chú' FROM BANGCHAMCONG, BANGTINHLUONG "
                + "WHERE BANGCHAMCONG.MANV = '" + maNV + "' AND BANGTINHLUONG.MANV = '" + maNV + "'", connection);
                //DataTable dtBANGCHAMCONG = new DataTable();
                da.Fill(dtBANGCHAMCONG);
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT LUONG 'Lương', BANGCHAMCONG.THANG 'Tháng', BANGCHAMCONG.NAM 'Năm', BANGCHAMCONG.MALUONG 'Mã lương', TIENKHENTHUONG 'Tiền khen thưởng', TIENKYLUAT 'Tiền kỷ luật', SONGAYCONG 'Số ngày công', SONGAYNGHI 'Số ngày nghỉ', SOGIOLAMTHEM 'Số giờ làm thêm', BANGCHAMCONG.GHICHU 'Ghi chú' FROM BANGCHAMCONG, BANGTINHLUONG "
                + "WHERE BANGCHAMCONG.MANV = '" + maNV + "' AND BANGCHAMCONG.THANG = '" + thang + "' AND BANGCHAMCONG.NAM = '" + nam + "'AND BANGTINHLUONG.MANV = '" + maNV + "' AND BANGTINHLUONG.THANG = '" + thang + "' AND BANGTINHLUONG.NAM = '" + nam + "'", connection);
                //DataTable dtBANGCHAMCONG = new DataTable();
                da.Fill(dtBANGCHAMCONG);
            }
            return dtBANGCHAMCONG;
        }

        public DTO_BANGCHAMCONG getBangChamCongNhanVienTheoThang(string maNV, int thang, int nam)
        {
            DTO_BANGCHAMCONG dtoBangChamCong = new DTO_BANGCHAMCONG();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT LUONG 'Lương', BANGCHAMCONG.THANG 'Tháng', BANGCHAMCONG.NAM 'Năm', BANGCHAMCONG.MALUONG 'Mã lương', TIENKHENTHUONG 'Tiền khen thưởng', TIENKYLUAT 'Tiền kỷ luật', SONGAYCONG 'Số ngày công', SONGAYNGHI 'Số ngày nghỉ', SOGIOLAMTHEM 'Số giờ làm thêm' FROM BANGCHAMCONG, BANGTINHLUONG "
                + "WHERE BANGCHAMCONG.MANV = '" + maNV + "' AND BANGCHAMCONG.THANG = '" + thang + "' AND BANGCHAMCONG.NAM = '" + nam + "' AND BANGTINHLUONG.MANV = '" + maNV + "' AND BANGTINHLUONG.THANG = '" + thang + "' AND BANGTINHLUONG.NAM = '" + nam + "'");
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                dtoBangChamCong.Thang = Convert.ToInt32(reader[1].ToString());
                dtoBangChamCong.Nam = Convert.ToInt32(reader[2].ToString());
                dtoBangChamCong.Maluong = reader[3].ToString();
                dtoBangChamCong.Tienkhenthuong = double.Parse(reader[4].ToString());
                dtoBangChamCong.Tienkyluat = double.Parse(reader[5].ToString());
                dtoBangChamCong.Songaycong = int.Parse(reader[6].ToString());
                dtoBangChamCong.Songaynghi = int.Parse(reader[7].ToString());
                dtoBangChamCong.Sogiolamthem = int.Parse(reader[8].ToString());

                if (!reader.IsClosed)
                    reader.Close();
                return dtoBangChamCong;
            }
            else
            {
                if (!reader.IsClosed)
                    reader.Close();
                return dtoBangChamCong;
            }
        }

        public string GetMaLuongTheoThang(string maNV, string thang, string nam)
        {
            string maLuong = string.Empty;
            CheckConnection();
            string sql = string.Format("SELECT MALUONG FROM BANGCHAMCONG WHERE MANV = '{0}' AND THANG= '{1}' AND NAM= '{2}'", maNV, thang, nam);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                maLuong = sdr["MALUONG"].ToString();
            }
            connection.Close();
            return maLuong;
        }

        public bool KiemTraTonTai(string maNV, string thang, string nam)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM BANGCHAMCONG WHERE MANV='{0}' AND THANG='{1}' AND NAM='{2}' ", maNV, thang,nam);
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

        public bool KiemTraTonTaiNhanVien(string maNV)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM BANGCHAMCONG WHERE MANV='{0}'", maNV);
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

        public bool SuaGhiChu(string ghiChu, string maNV)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE BANGCHAMCONG " +
                "SET GHICHU=N'{0}' WHERE MANV = '{1}'", ghiChu, maNV);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
    }
}
