using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DTO;
namespace DAL
{
    public class DAL_THAMSO : KetNoi
    {
        public DataTable getThamSo()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MATHAMSO 'Mã tham số', TENTHAMSO 'Tên tham số', GIATRI 'Giá trị' FROM THAMSO", connection);
            DataTable dtTHAMSO = new DataTable();
            da.Fill(dtTHAMSO);
            return dtTHAMSO;
        }


        public bool SuaThamSo(DTO_THAMSO ts)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            string sql = string.Format("UPDATE THAMSO SET  GIATRI='{0}' WHERE MATHAMSO= '{1}'", ts.Giatri,ts.Mats);
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
            connection.Close();
        }

        public double Get_soNgayNghiToiDa()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            double soNgayNghiToiDa = 0;
            string sql = string.Format("SELECT GIATRI FROM THAMSO WHERE MATHAMSO= 'TS01'");
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() == true)
            {
                soNgayNghiToiDa = Convert.ToDouble(reader[0].ToString());

            }
            if (!reader.IsClosed)
                reader.Close();
            connection.Close();
            return soNgayNghiToiDa;
        }
        public double Get_soNgayLamToiDa()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            double soNgayLamToiDa = 0;
            string sql = string.Format("SELECT GIATRI FROM THAMSO WHERE MATHAMSO= 'TS02'");
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() == true)
            {
                soNgayLamToiDa = Convert.ToDouble(reader[0].ToString());

            }
            if (!reader.IsClosed)
                reader.Close();
            connection.Close();
            return soNgayLamToiDa;
        }
        public double Get_tienLamthem()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            double tienLamThem = 0;
            string sql = string.Format("SELECT GIATRI FROM THAMSO WHERE MATHAMSO= 'TS03'");
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() == true)
            {
                tienLamThem = Convert.ToDouble(reader[0].ToString());

            }
            if (!reader.IsClosed)
                reader.Close();
            connection.Close();
            return tienLamThem;
        }
        public double Get_tiLePhat()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            double tiLePhat = 0;
            string sql = string.Format("SELECT GIATRI FROM THAMSO WHERE MATHAMSO= 'TS04'");
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() == true)
            {
                tiLePhat = Convert.ToDouble(reader[0].ToString());

            }
            if (!reader.IsClosed)
                reader.Close();
            connection.Close();
            return tiLePhat;
        }

        public double Get_soThangNghiSinh()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            double soThangNghiSinh = 0;
            string sql = string.Format("SELECT GIATRI FROM THAMSO WHERE MATHAMSO= 'TS05'");
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read() == true)
            {
                soThangNghiSinh = Convert.ToDouble(reader[0].ToString());

            }
            if (!reader.IsClosed)
                reader.Close();
            connection.Close();
            return soThangNghiSinh;
        }

    }
}
