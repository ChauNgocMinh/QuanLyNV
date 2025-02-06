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
    public class DAL_LSCHINHSUA : KetNoi
    {

        public DataTable getLSChinhSua()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MACS 'Mã chỉnh sửa', MANV 'Mã nhân viên', LANCS 'Lần chỉnh sửa', MAPHONG 'Mã phòng', MALUONG 'Mã lương', HOTEN 'Họ tên', FORMAT(NGAYSINH, 'MM/dd/yyyy') 'Ngày sinh', GIOITINH 'Giới tính', DANTOC 'Dân tộc', CMND_CCCD 'CMND-CCCD', NOICAP 'Nơi cấp', CHUCVU 'Chức vụ', MALOAINV 'Mã loại nhân viên', LOAIHD 'Loại hợp đồng', THOIGIAN 'Thời gian', FORMAT(NGAYKY, 'MM/dd/yyyy') 'Ngày ký',  FORMAT(NGAYHETHAN, 'MM/dd/yyyy') 'Ngày hết hạn', SDT 'Số điện thoại', HOCVAN 'Học vấn', GHICHU 'Ghi chú', NGAYCHINHSUA 'Ngày chỉnh sửa' FROM LSCHINHSUA", connection);
            DataTable dtLSCHINHSUA = new DataTable();
            da.Fill(dtLSCHINHSUA);
            return dtLSCHINHSUA;
        }

        public bool ThemLSChinhSua(DTO_LSCHINHSUA ls)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO LSCHINHSUA VALUES ('{0}','{1}', '{2}', '{3}'" +
                ", N'{4}', '{5}', N'{6}', N'{7}', " +
                "'{8}', N'{9}', N'{10}', '{11}', N'{12}', '{13}', '{14}', '{15}', '{16}', N'{17}', N'{18}', '{19}')"
                , ls.Manv, ls.Lancs, ls.Maphong, ls.Maluong, ls.Hoten, ls.Ngaysinh,
                ls.Gioitinh, ls.Dantoc, ls.Cmnd_cccd, ls.Noicap, ls.Chucvu, ls.Maloainv,
                ls.Loaihd, ls.Thoigian, ls.Ngaydangki, ls.Ngayhethan, ls.Sdt, ls.Hocvan, ls.Ghichu,ls.Ngaychinhsua);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
    MANV INT IDENTITY(1,1) PRIMARY KEY,
	MAPHONG VARCHAR(6),
	MALUONG VARCHAR(8),
	HOTEN NVARCHAR(70),
	NGAYSINH DATETIME,
	GIOITINH NVARCHAR(3),
	DANTOC NVARCHAR(12),
	CMND_CCCD VARCHAR(12),
	NOICAP NVARCHAR(20),
	CHUCVU NVARCHAR(25),
	MALOAINV VARCHAR(10),
	LOAIHD NVARCHAR(20),
	THOIGIAN INT,
	NGAYKY DATETIME,
	NGAYHETHAN DATETIME,
	SDT VARCHAR(10),
	HOCVAN NVARCHAR(20),
	GHICHU NVARCHAR(60)
 */
        //public bool SuaLSChinhSua(DTO_LSCHINHSUA ls)
        //{
        //    if (connection.State != ConnectionState.Open)
        //        connection.Open();
        //    string sql = string.Format("UPDATE LSCHINHSUA " +
        //        "SET MAPHONG='{0}, MALUONG='{1}',HOTEN=N'{2}',NGAYSINH='{3}',GIOITINH=N'{4}',DANTOC='{5}',CMND_CCCD='{6}'" +
        //        "NOICAP=N'{7}',CHUCVU=N'{8}',MALOAINV='{9}',LOAIHD=N'{10}',THOIGIAN='{11}','NGAYKY='{12}',NGAYHETHAN='{13}'" +
        //        "SDT='{14}',HOCVAN=N'{15}',GHICHU='{16}',NGAYCHINHSUA='{17}'" + "WHERE MANV = '{18}' AND MACS='{19}'",
        //    ls.Maphong, ls.Maluong, ls.Hoten, ls.Ngaysinh,
        //        ls.Gioitinh, ls.Dantoc, ls.Cmnd_cccd, ls.Noicap, ls.Chucvu, ls.Maloainv,
        //        ls.Loaihd, ls.Thoigian, ls.Ngaydangki, ls.Ngayhethan, ls.Sdt, ls.Hocvan, ls.Ghichu,ls.Ngaychinhsua, ls.Manv,ls.Macs);
        //    SqlCommand cmd = new SqlCommand(sql, connection);
        //    if (cmd.ExecuteNonQuery() > 0)
        //        return true;
        //    else return false;
        //    connection.Close();
        //}

        public bool XoaLSChinhSua(int maCS)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM LSCHINHSUA WHERE MACS = '{0}'", maCS);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaLSChinhSuaNhanVien(int macs)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM LSCHINHSUA WHERE MANV = '{0}'", macs);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public DataTable TongHopLSChinhSuaNhanVienTheoPhong(string maPhong, string maNV)
        {
            DataTable dtNHANVIEN = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            if (maPhong == "")
            {
                da = new SqlDataAdapter("SELECT * FROM LSCHINHSUA WHERE MANV = N'" + maNV + "'", connection);
            }

            if (maNV == "")
            {
                da = new SqlDataAdapter("SELECT * FROM LSCHINHSUA WHERE MAPHONG = N'" + maPhong + "'", connection);
            }

            if (maNV != "" && maPhong != "")
            {
                da = new SqlDataAdapter("SELECT * FROM LSCHINHSUA WHERE MAPHONG = N'" + maPhong + "' AND MANV = N'" + maNV + "'", connection);

            }

            da.Fill(dtNHANVIEN);
            return dtNHANVIEN;
        }

        public int TimLanChinhSuaGanNhat(string maNV)
        {
            int lanCS = 0;
            CheckConnection();
            string sql = string.Format("SELECT (CASE WHEN MAX(LANCS) >= 1 THEN MAX(LANCS) ELSE 0 END) 'LANCSGANNHAT' FROM LSCHINHSUA WHERE MANV = '{0}'", maNV);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lanCS = int.Parse(sdr["LANCSGANNHAT"].ToString());
            }
            connection.Close();
            return lanCS;
        }

        public DataTable getLSChinhSuaCuaTungNhanVien(string maNV)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MACS, MANV, LANCS, MAPHONG, MALUONG, HOTEN, FORMAT(NGAYSINH, 'MM/dd/yyyy') 'NGAYSINH', GIOITINH, DANTOC, CMND_CCCD, NOICAP, CHUCVU, MALOAINV, LOAIHD, THOIGIAN, FORMAT(NGAYKY, 'MM/dd/yyyy') 'NGAYKY',  FORMAT(NGAYHETHAN, 'MM/dd/yyyy') 'NGAYHETHAN', SDT, HOCVAN, GHICHU, NGAYCHINHSUA FROM LSCHINHSUA WHERE MANV = '" + maNV + "'", connection);
            DataTable dtLSCHINHSUA = new DataTable();
            da.Fill(dtLSCHINHSUA);
            return dtLSCHINHSUA;
        }

        public bool KiemTraTonTaiNhanVien(string maNV)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM LSCHINHSUA WHERE MANV='{0}'", maNV);
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
            string sql = string.Format("UPDATE LSCHINHSUA " +
                "SET GHICHU=N'{0}' WHERE MANV = '{1}'", ghiChu, maNV);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
    }
}
