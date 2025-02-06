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
    public class DAL_NVTHOIVIEC : KetNoi
    {

        public DataTable getNVThoiViec()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MANV 'Mã nhân viên',HOTEN 'Họ tên',CMND_CCCD 'CMND-CCCD',FORMAT(NGAYTHOIVIEC, 'MM/dd/yyyy') 'Ngày thôi việc',LYDO 'Lý do' FROM NVTHOIVIEC", connection);
            DataTable dtNVTHOIVIEC = new DataTable();
            da.Fill(dtNVTHOIVIEC);
            return dtNVTHOIVIEC;
        }
        public bool ThemNVThoiViec(DTO_NVTHOIVIEC nvThoiViec)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("INSERT INTO NVTHOIVIEC VALUES ('{0}', N'{1}','{2}','{3}',N'{4}')"
                , nvThoiViec.Manv, nvThoiViec.Hoten,nvThoiViec.Cmnd_cccd,nvThoiViec.Ngaythoiviec,nvThoiViec.Lydo);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }
        /*
MANV INT PRIMARY KEY,
HOTEN NVARCHAR(70),
CMND_CCCD VARCHAR(12),
NGAYTHOIVIEC DATETIME,
LYDO NVARCHAR(50)
 */
        public bool SuaNVThoiViec(DTO_NVTHOIVIEC nvThoiViec)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE NVTHOIVIEC " +
                "SET HOTEN=N'{0}', CMND_CCCD='{1}',NGAYTHOIVIEC='{2}',LYDO='{3}' " +
                "WHERE MANV = '{4}'", nvThoiViec.Hoten, nvThoiViec.Cmnd_cccd, nvThoiViec.Ngaythoiviec,nvThoiViec.Lydo,nvThoiViec.Manv);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public bool XoaNVThoiViec(int manv)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("DELETE FROM NVTHOIVIEC WHERE MANV = '{0}'", manv);
            SqlCommand cmd = new SqlCommand(sql, connection);
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
            connection.Close();
        }

        public int SoLuongNhanVienNghiViec(int thang, int nam)
        {
            int n = 0;
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("select * from NVTHOIVIEC Where month(NGAYTHOIVIEC)='{0}' AND year (NGAYTHOIVIEC) ='{1}'", thang, nam);
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
    }
}
