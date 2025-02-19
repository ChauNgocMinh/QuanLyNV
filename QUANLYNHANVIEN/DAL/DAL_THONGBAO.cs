using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;
namespace DAL
{
    public class DAL_THONGBAO : KetNoi
    {
        public DataTable getThongBao(int Ma)
        {
            string query = "SELECT tb.MATB AS 'Mã thông báo', tb.TIEUDE AS 'Tiêu đề', tb.NOIDUNG AS 'Nội dung', tb.TYPE AS 'Loại', tb.CreateDate AS 'Ngày gửi' " +
                           "FROM ThongBao tb " +
                           "JOIN ThongBaoNhanVien tbnv ON tb.MATB = tbnv.MATB " +
                           "WHERE tbnv.MA = @Ma " +
                           "ORDER BY tb.CreateDate DESC";
            using (SqlDataAdapter da = new SqlDataAdapter(query, connection))
            {
                da.SelectCommand.Parameters.AddWithValue("@Ma", Ma);
                DataTable dtTHONGBAO = new DataTable();
                da.Fill(dtTHONGBAO);
                return dtTHONGBAO;
            }
        }

        public bool ThemThongBao(DTO_BANGTHONGBAO tb, List<int> danhSachNguoiDung)
        {
            string queryInsertThongBao = "INSERT INTO ThongBao (TIEUDE, NOIDUNG, TYPE, CreateDate) OUTPUT INSERTED.MATB VALUES (@TieuDe, @NoiDung, @Type, @CreateDate)";

            using (SqlCommand cmd = new SqlCommand(queryInsertThongBao, connection))
            {
                cmd.Parameters.AddWithValue("@TieuDe", tb.Tieude);
                cmd.Parameters.AddWithValue("@NoiDung", tb.Noidung);
                cmd.Parameters.AddWithValue("@Type", tb.Type);
                cmd.Parameters.AddWithValue("@CreateDate", tb.Createdate);

                connection.Open();
                int matb = (int)cmd.ExecuteScalar(); 

                if (matb > 0)
                {
                    foreach (int idUser in danhSachNguoiDung)
                    {
                        string queryInsertNguoiDung = "INSERT INTO ThongBaoNguoiDung (MATB, MA) VALUES (@Matb, @Ma)";
                        using (SqlCommand cmdNguoiDung = new SqlCommand(queryInsertNguoiDung, connection))
                        {
                            cmdNguoiDung.Parameters.AddWithValue("@Matb", matb);
                            cmdNguoiDung.Parameters.AddWithValue("@Ma", idUser);
                            cmdNguoiDung.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                    return true;
                }

                connection.Close();
                return false;
            }
        }
    }
}
