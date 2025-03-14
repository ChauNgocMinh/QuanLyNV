﻿using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DAL
{
    public class DAL_NHANVIEN : KetNoi
    {

        public DataTable getNhanVien()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MANV 'Mã nhân viên', MAPHONG 'Mã phòng', MALUONG 'Mã lương', HOTEN 'Họ tên', FORMAT(NGAYSINH, 'MM/dd/yyyy') 'Ngày sinh', GIOITINH 'Giới tính', DANTOC 'Dân tộc', CMND_CCCD 'CMND-CCCD', NOICAP 'Nơi cấp', CHUCVU 'Chức vụ', MALOAINV 'Mã loại nhân viên', LOAIHD 'Loại hợp đồng', THOIGIAN 'Thời gian hợp đồng', FORMAT(NGAYKY, 'MM/dd/yyyy') 'Ngày ký hợp đồng',  FORMAT(NGAYHETHAN, 'MM/dd/yyyy') 'Ngày hết hạn', SDT 'Số điện thoại', HOCVAN 'Học vấn', GHICHU 'Ghi chú ' FROM NHANVIEN", connection);
            DataTable dtNHANVIEN = new DataTable();
            da.Fill(dtNHANVIEN);
            dtNHANVIEN.Columns.Add("Chọn", typeof(bool));
            foreach (DataRow row in dtNHANVIEN.Rows)
            {
                row["Chọn"] = false;
            }

            return dtNHANVIEN;
        }

        public DataTable getNhanVienByFilter(string urlImage)
        {
            SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM NHANVIEN where ANHDAIDIEN = '{urlImage}'", connection);
            DataTable dtNHANVIEN = new DataTable();
            da.Fill(dtNHANVIEN);
            if (dtNHANVIEN.Rows.Count > 0)
            {
                return dtNHANVIEN;
            }
            return null;
        }


        public DataTable xuatNhanVien()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MANV, MAPHONG, MALUONG, HOTEN, FORMAT(NGAYSINH, 'MM/dd/yyyy') NGAYSINH, GIOITINH, DANTOC , CMND_CCCD, NOICAP, CHUCVU, MALOAINV, LOAIHD, THOIGIAN, FORMAT(NGAYKY, 'MM/dd/yyyy') NGAYKY,  FORMAT(NGAYHETHAN, 'MM/dd/yyyy') NGAYHETHAN, SDT, HOCVAN, GHICHU FROM NHANVIEN", connection);
            DataTable dtNHANVIEN = new DataTable();
            da.Fill(dtNHANVIEN);
            return dtNHANVIEN;
        }

        public bool ThemNhanVien(DTO_NHANVIEN nhanVien)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    string sqlNhanVien = @"
                        INSERT INTO NHANVIEN (MAPHONG, MALUONG, HOTEN, NGAYSINH, GIOITINH, DANTOC, CMND_CCCD, NOICAP, CHUCVU, MALOAINV, 
                                              LOAIHD, THOIGIAN, NGAYKY, NGAYHETHAN, SDT, HOCVAN, GHICHU, ANHDAIDIEN)
                        OUTPUT INSERTED.MANV
                        VALUES (@Maphong, @Maluong, @Hoten, @Ngaysinh, @Gioitinh, @Dantoc, @Cmnd, @Noicap, @Chucvu, @Maloainv, 
                                @Loaihd, @Thoigian, @Ngaydangki, @Ngayhethan, @Sdt, @Hocvan, @Ghichu, @AnhDaiDien)";

                    using (SqlCommand cmdNhanVien = new SqlCommand(sqlNhanVien, connection, transaction))
                    {
                        cmdNhanVien.Parameters.AddWithValue("@Maphong", nhanVien.Maphong);
                        cmdNhanVien.Parameters.AddWithValue("@Maluong", nhanVien.Maluong);
                        cmdNhanVien.Parameters.AddWithValue("@Hoten", nhanVien.Hoten);
                        cmdNhanVien.Parameters.AddWithValue("@Ngaysinh", nhanVien.Ngaysinh);
                        cmdNhanVien.Parameters.AddWithValue("@Gioitinh", nhanVien.Gioitinh);
                        cmdNhanVien.Parameters.AddWithValue("@Dantoc", nhanVien.Dantoc);
                        cmdNhanVien.Parameters.AddWithValue("@Cmnd", nhanVien.Cmnd_cccd);
                        cmdNhanVien.Parameters.AddWithValue("@Noicap", nhanVien.Noicap);
                        cmdNhanVien.Parameters.AddWithValue("@Chucvu", nhanVien.Chucvu);
                        cmdNhanVien.Parameters.AddWithValue("@Maloainv", nhanVien.Maloainv);
                        cmdNhanVien.Parameters.AddWithValue("@Loaihd", nhanVien.Loaihd);
                        cmdNhanVien.Parameters.AddWithValue("@Thoigian", nhanVien.Thoigian);
                        cmdNhanVien.Parameters.AddWithValue("@Ngaydangki", nhanVien.Ngaydangki);
                        cmdNhanVien.Parameters.AddWithValue("@Ngayhethan", nhanVien.Ngayhethan);
                        cmdNhanVien.Parameters.AddWithValue("@Sdt", nhanVien.Sdt);
                        cmdNhanVien.Parameters.AddWithValue("@Hocvan", nhanVien.Hocvan);
                        cmdNhanVien.Parameters.AddWithValue("@Ghichu", nhanVien.Ghichu);
                        cmdNhanVien.Parameters.AddWithValue("@AnhDaiDien", nhanVien.AnhDaiDien);

                        int newMaNV = (int)cmdNhanVien.ExecuteScalar();

                        string sqlTaiKhoan = @"
                            INSERT INTO TAIKHOAN (MALOAITK, TENCHUTAIKHOAN, TENDANGNHAP, MATKHAU) 
                            VALUES (@Maloaitk, @TenChuTaiKhoan, @TenDangNhap, @MatKhau)";
                        string hashedPassword = HashPassword("123456");
                        using (SqlCommand cmdTaiKhoan = new SqlCommand(sqlTaiKhoan, connection, transaction))
                        {
                            cmdTaiKhoan.Parameters.AddWithValue("@Maloaitk", 3); 
                            cmdTaiKhoan.Parameters.AddWithValue("@TenChuTaiKhoan", nhanVien.Hoten);
                            cmdTaiKhoan.Parameters.AddWithValue("@TenDangNhap", newMaNV.ToString()); 
                            cmdTaiKhoan.Parameters.AddWithValue("@MatKhau", hashedPassword);

                            cmdTaiKhoan.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
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
        public bool SuaNhanVien(DTO_NHANVIEN nhanVien)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE NHANVIEN " +
                "SET MAPHONG='{0}', MALUONG='{1}',HOTEN=N'{2}',NGAYSINH='{3}',GIOITINH=N'{4}',DANTOC=N'{5}',CMND_CCCD='{6}', " +
                "NOICAP=N'{7}',CHUCVU=N'{8}',MALOAINV='{9}',LOAIHD=N'{10}',THOIGIAN='{11}',NGAYHETHAN='{12}', " +
                "SDT='{13}',HOCVAN=N'{14}',GHICHU=N'{15}',ANHDAIDIEN=N'{16}'" + "WHERE MANV = '{17}'",
                nhanVien.Maphong, nhanVien.Maluong, nhanVien.Hoten, nhanVien.Ngaysinh,
                nhanVien.Gioitinh, nhanVien.Dantoc, nhanVien.Cmnd_cccd, nhanVien.Noicap, nhanVien.Chucvu, nhanVien.Maloainv,
                nhanVien.Loaihd, nhanVien.Thoigian, nhanVien.Ngayhethan, nhanVien.Sdt, nhanVien.Hocvan, nhanVien.Ghichu, nhanVien.AnhDaiDien, nhanVien.Manv);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaNhanVien(int manv)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM NHANVIEN WHERE MANV = '{0}'", manv);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public DataTable TongHopNhanVienTheoPhong(string maPhong, string ten)
        {
            DataTable dtNHANVIEN = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            if (maPhong == "")
            {
                da = new SqlDataAdapter("SELECT * FROM NHANVIEN WHERE dbo.fChuyenCoDauThanhKhongDau(HOTEN) LIKE N'%" + ten + "%'", connection);
            }

            if (ten == "")
            {
                da = new SqlDataAdapter("SELECT * FROM NHANVIEN WHERE MAPHONG = N'" + maPhong + "'", connection);
            }

            if (ten != "" && maPhong != "")
            {
                da = new SqlDataAdapter("SELECT * FROM NHANVIEN WHERE MAPHONG = N'" + maPhong + "' and dbo.fChuyenCoDauThanhKhongDau(HOTEN) LIKE N'%" + ten + "%'", connection);
                
            }

            da.Fill(dtNHANVIEN);
            return dtNHANVIEN;
        }

        public List<string> TongHopMaNhanVienTheoGioiTinh(string gioiTinh)
        {
            List<string> listMaNhanVien = new List<string>();
            CheckConnection();
            string sql = string.Format("SELECT MANV FROM NHANVIEN WHERE GIOITINH = N'" + gioiTinh + "'");

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                listMaNhanVien.Add(sdr[0].ToString());
            }
            connection.Close();
            return listMaNhanVien;
        }

        public List<string> TongHopMaNhanVien()
        {
            List<string> listMaNhanVien = new List<string>();
            CheckConnection();
            string sql = string.Format("SELECT MANV FROM NHANVIEN");

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                listMaNhanVien.Add(sdr[0].ToString());
            }
            connection.Close();
            return listMaNhanVien;
        }

        public string TimTenNVTheoMa(string maNV)
        {
            string tenNV = string.Empty;
            CheckConnection();
            string sql = string.Format("SELECT HOTEN FROM NHANVIEN WHERE MANV = '{0}'", maNV);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                tenNV = sdr["HOTEN"].ToString();
            }
            connection.Close();
            return tenNV;
        }

        public int TimNamDauTienNVVaoLam()
        {
            int nam = 1990;
            CheckConnection();
            string sql = string.Format("SELECT TOP 1 YEAR(NGAYKY) 'NGAYKYSOMNHAT' FROM NHANVIEN ORDER BY NGAYKY ASC");

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                nam = int.Parse(sdr["NGAYKYSOMNHAT"].ToString());
            }
            connection.Close();
            return nam;
        }

        public int TimNamGanNhatNVVaoLam()
        {
            int nam = 1990;
            CheckConnection();
            string sql = string.Format("SELECT TOP 1 YEAR(NGAYKY) 'NGAYKYGANNHAT' FROM NHANVIEN ORDER BY NGAYKY DESC");

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                nam = int.Parse(sdr["NGAYKYGANNHAT"].ToString());
            }
            connection.Close();
            return nam;
        }

        public int TimMaNVTheoTen(string tenNV)
        {
            int maNV = 0;
            CheckConnection();
            string sql = string.Format("SELECT MANV FROM NHANVIEN WHERE HOTEN = N'{0}'", tenNV);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                maNV = int.Parse(sdr["MANV"].ToString());
            }
            connection.Close();
            return maNV;
        }

        public string GetMaLuong(string maNV)
        {
            string maLuong = string.Empty;
            CheckConnection();
            string sql = string.Format("SELECT MALUONG FROM NHANVIEN WHERE MANV = N'{0}'", maNV);

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                maLuong = sdr["MALUONG"].ToString();
            }
            connection.Close();
            return maLuong;
        }

        public bool SuaMaLuongNhanVien(string maNV, string maLuong)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE NHANVIEN SET MALUONG = '{0}' WHERE MANV = '{1}'", maLuong, maNV);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public int SoLuongNhanVienVaoLam (int thang,int nam)
        {
            int n = 0;
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("select * from NHANVIEN Where month(NGAYKY)='{0}' AND year (NGAYKY) ='{1}'", thang, nam);
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() == true)
            {
                n++;
            }
            if (!reader.IsClosed)
                reader.Close();
            connection.Close();
            return n;
        }

        public DTO_NHANVIEN GetChiTietNhanVienTheoMa(string maNV)
        {
            DTO_NHANVIEN dtoNhanVien = new DTO_NHANVIEN();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("SELECT * FROM NHANVIEN WHERE MANV='" + maNV + "'");
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                dtoNhanVien.Manv = int.Parse(maNV);
                dtoNhanVien.Maphong = reader[1].ToString();
                dtoNhanVien.Maluong = reader[2].ToString();
                dtoNhanVien.Hoten = reader[3].ToString();
                dtoNhanVien.Ngaysinh = DateTime.Parse(reader[4].ToString());
                dtoNhanVien.Gioitinh = reader[5].ToString();
                dtoNhanVien.Dantoc = reader[6].ToString();
                dtoNhanVien.Cmnd_cccd = reader[7].ToString();
                dtoNhanVien.Noicap = reader[8].ToString();
                dtoNhanVien.Chucvu = reader[9].ToString();
                dtoNhanVien.Maloainv = reader[10].ToString();
                dtoNhanVien.Loaihd = reader[11].ToString();
                dtoNhanVien.Thoigian = int.Parse(reader[12].ToString());
                dtoNhanVien.Ngayhethan = DateTime.Parse(reader[14].ToString());
                dtoNhanVien.Sdt = reader[15].ToString();
                dtoNhanVien.Hocvan = reader[16].ToString();
                dtoNhanVien.Ghichu = reader[17].ToString();

                if (!reader.IsClosed)
                    reader.Close();
                return dtoNhanVien;
            }
            else
            {
                if (!reader.IsClosed)
                    reader.Close();
                return dtoNhanVien;
            }
        }

        public DataTable TimKiemNVTheoMa(string manv)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM NHANVIEN WHERE MANV LIKE '%" + manv + "%'", connection);
            DataTable dtNHANVIEN = new DataTable();
            da.Fill(dtNHANVIEN);
            return dtNHANVIEN;
        }
        public DataTable TimKiemNVTheoTen(string ten)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM NHANVIEN WHERE dbo.fChuyenCoDauThanhKhongDau(HOTEN) LIKE N'%" + ten + "%'", connection);
            DataTable dtNHANVIEN = new DataTable();
            da.Fill(dtNHANVIEN);
            return dtNHANVIEN;
        }
        public DataTable TimKiemNVTheoSDT(string sdt)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM NHANVIEN WHERE SDT LIKE '%" + sdt + "%'", connection);
            DataTable dtNHANVIEN = new DataTable();
            da.Fill(dtNHANVIEN);
            return dtNHANVIEN;
        }
    }
}
