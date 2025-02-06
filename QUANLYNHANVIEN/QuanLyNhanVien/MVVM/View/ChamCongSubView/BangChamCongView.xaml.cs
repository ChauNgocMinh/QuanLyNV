using BUS;
using DTO;
using Microsoft.Win32;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using QuanLyNhanVien.MessageBox;
using QuanLyNhanVien.WindowView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace QuanLyNhanVien.MVVM.View.ChamCongSubView
{
    /// <summary>
    /// Interaction logic for BangChamCongView.xaml
    /// </summary>
    public partial class BangChamCongView : UserControl
    {
        public BUS_BANGLUONG busBangLuong = new BUS_BANGLUONG();
        public BUS_BANGCHAMCONG busBangChamCong = new BUS_BANGCHAMCONG();
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        public BUS_BANGTINHLUONG busBangTinhLuong = new BUS_BANGTINHLUONG();
        public BUS_KHENTHUONG busKhenThuong = new BUS_KHENTHUONG();
        public BUS_KYLUAT busKyLuat = new BUS_KYLUAT();
        public BUS_LICHSUVANGMAT busLichSuVangMat = new BUS_LICHSUVANGMAT();
        public BUS_THAMSO busThamSo = new BUS_THAMSO();
        public double tienKT_KL = 0;
        public string maNV = "";

        public BangChamCongView()
        {
            InitializeComponent();
            DataGridLoad();
        }

        private void maNVTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (maNVTbx.Text == "")
            {
                maLuongTbx.Text = "";
                return;
            }
            maLuongTbx.Text = busNhanVien.GetMaLuong(maNVTbx.Text);

            loadNgayTbxes();
        }

        private void maLuongTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (maLuongTbx.Text == "")
            {
                luongCBTbx.Text = "";
                phuCapTbx.Text = "";
                phuCapKhacTbx.Text = "";
                return;
            }

            DTO_BANGLUONG dtoBangLuong = new DTO_BANGLUONG();
            dtoBangLuong = busBangLuong.GetChiTietLuong(maLuongTbx.Text);
            luongCBTbx.Text = dtoBangLuong.Lcb.ToString();
            phuCapTbx.Text = dtoBangLuong.Phucapchucvu.ToString();
            phuCapKhacTbx.Text = dtoBangLuong.Phucapkhac.ToString();
        }

        private void thoiGianDpk_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            loadNgayTbxes();
        }

        private void tinhLuongBtn_Click(object sender, RoutedEventArgs e)
        {
            CheckVaTinhLuong();
        }

        private void maNVBtn_Click(object sender, RoutedEventArgs e)
        {
            DSChonNhanVien dsChonNhanVien = new DSChonNhanVien(maNV);
            dsChonNhanVien.ShowDialog();
            maNVTbx.Text = dsChonNhanVien.MaNV;
        }

        private void tinhTienKhenThuongBtn_Click(object sender, RoutedEventArgs e)
        {
            if (khenThuongTbx.Text == "")
            {
                khenThuongTbx.Text = "0";
            }
                
            DanhSachKhenThuong dsKhenThuong = new DanhSachKhenThuong("khen thưởng", tienKT_KL, khenThuongTbx.Text);
            dsKhenThuong.ShowDialog();
            khenThuongTbx.Text = dsKhenThuong.TongTien.ToString();
        }

        private void tinhTienKyLuatBtn_Click(object sender, RoutedEventArgs e)
        {
            if (kyLuatTbx.Text == "")
            {
                kyLuatTbx.Text = "0";
            }

            DanhSachKhenThuong dsKhenThuong = new DanhSachKhenThuong("kỷ luật", tienKT_KL, kyLuatTbx.Text);
            dsKhenThuong.ShowDialog();
            kyLuatTbx.Text = dsKhenThuong.TongTien.ToString();
        }

        private void luongDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (luongDtg.SelectedItems.Count == 0)
            {
                return;
            }

            DataRowView row = luongDtg.SelectedItem as DataRowView;

            if (row == null)
            {
                maNVTbx.Text = "";
                thoiGianDpk.Text = "";
                return;
            }

            maNVTbx.Text = row[0].ToString();
            string selectedMonth = row[2].ToString() + "/" + row[3].ToString();
            thoiGianDpk.SelectedDate = DateTime.ParseExact(selectedMonth, "MM/yyyy", null);
            khenThuongTbx.Text = row[5].ToString();
            kyLuatTbx.Text = row[6].ToString();
            CheckVaTinhLuong();
        }

        private void themBtn_Click(object sender, RoutedEventArgs e)
        {
            bool? result;

            if (maNVTbx.Text == "" || thoiGianDpk.Text == "")
            {
                result = new MessageBoxCustom("Vui chọn đầy đủ 2 mục mã nhân viên và thời gian.", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }

            if (busBangChamCong.KiemTraTonTai(maNVTbx.Text, thoiGianDpk.SelectedDate.Value.Month.ToString(), thoiGianDpk.SelectedDate.Value.Year.ToString()))
            {
                DTO_BANGCHAMCONG dtoBangChamCong = new DTO_BANGCHAMCONG();
                dtoBangChamCong.Manv = int.Parse(maNVTbx.Text);
                dtoBangChamCong.Thang = thoiGianDpk.SelectedDate.Value.Month;
                dtoBangChamCong.Nam = thoiGianDpk.SelectedDate.Value.Year;
                dtoBangChamCong.Maluong = maLuongTbx.Text;
                dtoBangChamCong.Tienkhenthuong = double.Parse(khenThuongTbx.Text);
                dtoBangChamCong.Tienkyluat = double.Parse(kyLuatTbx.Text);
                dtoBangChamCong.Songaycong = int.Parse(soNgayCongTbx.Text);
                dtoBangChamCong.Songaynghi = int.Parse(soNgayNghiTbx.Text);
                dtoBangChamCong.Sogiolamthem = int.Parse(soGioLamThemTbx.Text);

                busBangChamCong.SuaBangChamCong(dtoBangChamCong);

                DTO_BANGTINHLUONG dtoBangTinhLuong = new DTO_BANGTINHLUONG();
                dtoBangTinhLuong.Manv = int.Parse(maNVTbx.Text);
                dtoBangTinhLuong.Thang = thoiGianDpk.SelectedDate.Value.Month;
                dtoBangTinhLuong.Nam = thoiGianDpk.SelectedDate.Value.Year;
                dtoBangTinhLuong.Luong = double.Parse(tongTienTbk.Text);

                busBangTinhLuong.SuaBangTinhLuong(dtoBangTinhLuong);

                result = new MessageBoxCustom("Sửa thành công", MessageType.Success, MessageButtons.Ok).ShowDialog();

            }
            else
            {
                DTO_BANGCHAMCONG dtoBangChamCong = new DTO_BANGCHAMCONG();
                dtoBangChamCong.Manv = int.Parse(maNVTbx.Text);
                dtoBangChamCong.Thang = thoiGianDpk.SelectedDate.Value.Month;
                dtoBangChamCong.Nam = thoiGianDpk.SelectedDate.Value.Year;
                dtoBangChamCong.Maluong = maLuongTbx.Text;
                dtoBangChamCong.Tienkhenthuong = double.Parse(khenThuongTbx.Text);
                dtoBangChamCong.Tienkyluat = double.Parse(kyLuatTbx.Text);
                dtoBangChamCong.Songaycong = int.Parse(soNgayCongTbx.Text);
                dtoBangChamCong.Songaynghi = int.Parse(soNgayNghiTbx.Text);
                dtoBangChamCong.Sogiolamthem = int.Parse(soGioLamThemTbx.Text);

                busBangChamCong.ThemBangChamCong(dtoBangChamCong);

                DTO_BANGTINHLUONG dtoBangTinhLuong = new DTO_BANGTINHLUONG();
                dtoBangTinhLuong.Manv = int.Parse(maNVTbx.Text);
                dtoBangTinhLuong.Thang = thoiGianDpk.SelectedDate.Value.Month;
                dtoBangTinhLuong.Nam = thoiGianDpk.SelectedDate.Value.Year;
                dtoBangTinhLuong.Luong = double.Parse(tongTienTbk.Text);

                busBangTinhLuong.ThemBangTinhLuong(dtoBangTinhLuong);

                result = new MessageBoxCustom("Thêm thành công", MessageType.Success, MessageButtons.Ok).ShowDialog();
                
            }

            maNVTbx.Text = "";
            thoiGianDpk.Text = "";
            ClearTextBoxes();
            loadNgayTbxes();
            DataGridLoad();
        }

        private void lamMoiBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }

        private void xoaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (luongDtg.SelectedItems.Count == 0)
            {
                bool? Result1 = new MessageBoxCustom("Vui lòng chọn chi tiết lương cần xóa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            bool? result = new MessageBoxCustom("Xác nhận xóa?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (!result.Value)
                return;

            DataRowView row = luongDtg.SelectedItem as DataRowView;
            busBangChamCong.XoaBangChamCong(int.Parse(row[0].ToString()), int.Parse(row[2].ToString()), int.Parse(row[3].ToString()));
            DataGridLoad();
            busBangTinhLuong.XoaBangTinhLuong(int.Parse(row[0].ToString()), int.Parse(row[2].ToString()), int.Parse(row[3].ToString()));
            bool? Result = new MessageBoxCustom("Xóa chi tiết lương thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            ClearTextBoxes();
        }

        private void xuatExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "";

            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "Excel |*.xlsx";

            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }

            if (string.IsNullOrEmpty(filePath))
            {
                bool? result = new MessageBoxCustom("Đường dẫn không hợp lệ!", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    p.Workbook.Properties.Author = "Nhóm13_QLNV";
                    p.Workbook.Properties.Title = "Danh sách lương nhân viên";
                    p.Workbook.Worksheets.Add("Sheet 1");

                    ExcelWorksheet ws = p.Workbook.Worksheets[0];
                    ws.Name = "Test sheet";
                    ws.Cells.Style.Font.Size = 14;
                    ws.Cells.Style.Font.Name = "Consolas";

                    string[] arrColumnHeader = { "Mã nhân viên", "Họ tên", "Tháng", "Năm", "Mã lương", "Tiền khen thưởng",
                                                "Tiền kỷ luật", "Số ngày công", "Số ngày nghỉ", "Số giờ làm thêm", "Ghi chú" };

                    var countColHeader = arrColumnHeader.Count();

                    ws.Cells[1, 1].Value = "Danh sách lương nhân viên";
                    ws.Cells[1, 1, 1, countColHeader].Merge = true;
                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    int colIndex = 1;
                    int rowIndex = 2;

                    foreach (var item in arrColumnHeader)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];

                        var fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                        var border = cell.Style.Border;
                        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                        cell.Value = item;
                        colIndex++;
                    }
                    DataTable dt = new DataTable();
                    dt = busBangChamCong.xuatBangChamCong();

                    foreach (DataRow dr in dt.Rows)
                    {
                        colIndex = 1;
                        rowIndex++;

                        ws.Cells[rowIndex, colIndex++].Value = dr["MANV"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["HOTEN"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["THANG"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["NAM"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["MALUONG"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["TIENKHENTHUONG"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["TIENKYLUAT"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["SONGAYCONG"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["SONGAYNGHI"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["SOGIOLAMTHEM"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["GHICHU"].ToString();

                    }

                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);

                }
                bool? result = new MessageBoxCustom("Xuất excel thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu file!", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        public void CheckVaTinhLuong()
        {
            try
            {
                if (soNgayCongTbx.Text == "" && soGioLamThemTbx.Text == "" && phuCapTbx.Text == "" && phuCapKhacTbx.Text == "" && luongCBTbx.Text == "" && khenThuongTbx.Text == "" && kyLuatTbx.Text == "" )
                {
                    tongTienTbk.Text = "0";
                }
                else
                {
                    SetDefaultValue(soNgayCongTbx);
                    SetDefaultValue(soGioLamThemTbx);
                    SetDefaultValue(phuCapTbx);
                    SetDefaultValue(phuCapKhacTbx);
                    SetDefaultValue(luongCBTbx);
                    SetDefaultValue(khenThuongTbx);
                    SetDefaultValue(kyLuatTbx);
                    SetDefaultValue(soNgayNghiTbx);

                    if (soNgayCongTbx.Text == "0")
                    {
                        tongTienTbk.Text = (double.Parse(phuCapTbx.Text) + double.Parse(phuCapKhacTbx.Text) + double.Parse(luongCBTbx.Text) + double.Parse(khenThuongTbx.Text) - double.Parse(kyLuatTbx.Text)).ToString();
                        return;
                    }

                    double soNgayNghiKoLuong = busThamSo.Get_soNgayLamToiDa() - double.Parse(soNgayCongTbx.Text);
                    double tienPhat = ((busThamSo.Get_tiLePhat() * double.Parse(luongCBTbx.Text)) * soNgayNghiKoLuong) + double.Parse(kyLuatTbx.Text);

                    double luongTheoSoNgayCong = ((double.Parse(luongCBTbx.Text) / busThamSo.Get_soNgayLamToiDa()) * int.Parse(soNgayCongTbx.Text));
                    double luongLamThem = (int.Parse(soGioLamThemTbx.Text) * busThamSo.Get_tienLamthem());
                    double luongPhuCap = double.Parse(phuCapTbx.Text) + double.Parse(phuCapKhacTbx.Text);
                    double tienThuong = luongTheoSoNgayCong + luongPhuCap + luongLamThem + double.Parse(khenThuongTbx.Text);

                    //bool? result = new MessageBoxCustom(tienThuong.ToString(), MessageType.Error, MessageButtons.Ok).ShowDialog();

                    tongTienTbk.Text = (tienThuong - tienPhat).ToString("#.##");
                }
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi tính! Vui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            
        }

        public void SetDefaultValue(TextBox tbx)
        {
            if (tbx.Text == "")
            {
                tbx.Text = "0";
            }
        }

        public void DataGridLoad()
        {
            luongDtg.DataContext = busBangChamCong.getBangChamCong();
        }

        public void loadNgayTbxes()
        {
            if (maNVTbx.Text == "")
            {
                return;
            }

            if (thoiGianDpk.Text == "")
            {
                soNgayCongTbx.Text = "";
                soNgayNghiTbx.Text = "";
                soGioLamThemTbx.Text = "";
                return;
            }

            int thang = thoiGianDpk.SelectedDate.Value.Month;
            int nam = thoiGianDpk.SelectedDate.Value.Year;
            //DTO_BANGCHAMCONG dtoBangChamCong = busBangChamCong.getBangChamCongTheoNhanVien(maNVTbx.Text, thang, nam);
            soNgayNghiTbx.Text = busLichSuVangMat.DemSoNgayNghiTrongThang(int.Parse(maNVTbx.Text), thang, nam).ToString();
            if (double.Parse(soNgayNghiTbx.Text) <= busThamSo.Get_soNgayNghiToiDa())
            {
                soNgayCongTbx.Text = busThamSo.Get_soNgayLamToiDa().ToString();
            }
            else
            {
                soNgayCongTbx.Text = (busThamSo.Get_soNgayLamToiDa() - double.Parse(soNgayNghiTbx.Text) + busThamSo.Get_soNgayNghiToiDa()).ToString();
            }

            //soGioLamThemTbx.Text = dtoBangChamCong.Sogiolamthem.ToString();
        }

        public void ClearTextBoxes()
        {
            maNVTbx.Text = "";
            thoiGianDpk.Text = "";
            soNgayCongTbx.Text = "";
            soNgayNghiTbx.Text = "";
            soGioLamThemTbx.Text = "";
            khenThuongTbx.Text = "";
            kyLuatTbx.Text = "";
            tongTienTbk.Text = "";
        }
    }
}
