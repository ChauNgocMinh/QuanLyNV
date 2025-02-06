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
using System.Text.RegularExpressions;
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
    /// Interaction logic for BangChamCongThuViecView.xaml
    /// </summary>
    public partial class BangChamCongThuViecView : UserControl
    {
        BUS_BANGCHAMCONGTHUVIEC busBangChamCongThuViec = new BUS_BANGCHAMCONGTHUVIEC();
        BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();

        public BangChamCongThuViecView()
        {
            InitializeComponent();
            CbxesLoaded();
            bangCongThuViecDtg.DataContext = busBangChamCongThuViec.getBangChamCongThuViecTheoThang(thangCbx.SelectedValue.ToString(), namCbx.Text);

        }

        private void numberBoxes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void lamMoiBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetData();
        }

        private void xoaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bangCongThuViecDtg.SelectedItems.Count == 0)
            {
                bool? Result1 = new MessageBoxCustom("Vui lòng chọn bảng lương cần xóa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            bool? result = new MessageBoxCustom("Xác nhận xóa?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (!result.Value)
                return;
            DataRowView row = bangCongThuViecDtg.SelectedItem as DataRowView;

            busBangChamCongThuViec.XoaBangChamCongThuViec(int.Parse(row[0].ToString()), int.Parse(row[1].ToString()), int.Parse(row[2].ToString()));
            bool? Result = new MessageBoxCustom("Xóa bản lương thành công", MessageType.Success, MessageButtons.Ok).ShowDialog();
            ResetData();
        }

        private void suaBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (bangCongThuViecDtg.SelectedItems.Count == 0)
            {
                bool? result = new MessageBoxCustom("Vui lòng chọn bảng lương cần sửa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            DTO_BANGCHAMCONGTHUVIEC dtoBangCongThuViec = new DTO_BANGCHAMCONGTHUVIEC();
            DataRowView row = bangCongThuViecDtg.SelectedItem as DataRowView;
            ChamCongThuViec chamCong = new ChamCongThuViec(false);

            dtoBangCongThuViec.Manvtv = int.Parse(row[0].ToString());
            dtoBangCongThuViec.Thang = int.Parse(row[1].ToString());
            dtoBangCongThuViec.Nam = int.Parse(row[2].ToString());
            dtoBangCongThuViec.Luongtv = double.Parse(row[6].ToString());
            dtoBangCongThuViec.Songaycong = int.Parse(row[3].ToString());
            dtoBangCongThuViec.Songaynghi = int.Parse(row[4].ToString());
            dtoBangCongThuViec.Sogiolamthem = int.Parse(row[5].ToString());
            dtoBangCongThuViec.Ghichu = row[7].ToString();

            chamCong.suaChamCongThuViec = dtoBangCongThuViec;
            chamCong.ShowDialog();
            ResetData();
        }

        private void xuatExcel_Click(object sender, RoutedEventArgs e)
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
                    p.Workbook.Properties.Title = "Bảng lương nhân viên thử việc";
                    p.Workbook.Worksheets.Add("Sheet 1");

                    ExcelWorksheet ws = p.Workbook.Worksheets[0];
                    ws.Name = "Test sheet";
                    ws.Cells.Style.Font.Size = 14;
                    ws.Cells.Style.Font.Name = "Consolas";

                    string[] arrColumnHeader = { "Mã nhân viên", "Năm", "Tháng", "Số ngày công", "Số ngày nghỉ",
                                                "Số giờ làm thêm", "Lương thử việc", "Ghi chú" };

                    var countColHeader = arrColumnHeader.Count();

                    ws.Cells[1, 1].Value = "Bảng lương nhân viên thử việc";
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
                    if (thangCbx.Text != "" && namCbx.Text != "")
                    {
                        dt = busBangChamCongThuViec.xuatBangChamCongThuViecTheoThang(thangCbx.SelectedValue.ToString(), namCbx.Text);
                    }
                    else
                    {
                        dt = busBangChamCongThuViec.xuatBangChamCongThuViec();
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        colIndex = 1;
                        rowIndex++;

                        ws.Cells[rowIndex, colIndex++].Value = dr["MANVTV"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["THANG"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["NAM"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["SONGAYCONG"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["SONGAYNGHI"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["SOGIOLAMTHEM"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["LUONGTV"].ToString();
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

        private void themBtn_Click(object sender, RoutedEventArgs e)
        {
            ChamCongThuViec chamCong = new ChamCongThuViec(true);
            chamCong.ShowDialog();
            ResetData();
        }

        public void DataGridLoad()
        {
            bangCongThuViecDtg.DataContext = busBangChamCongThuViec.getBangChamCongThuViec();
        }

        public void ResetData()
        {
            thangCbx.Text = "";
            namCbx.Text = "";
            DataGridLoad();
        }

        private void bangCongThuViec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (namCbx.Text == "")
                {
                    bool? result = new MessageBoxCustom("Vui lòng nhập năm!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }
                if (thangCbx.Text == "")
                {
                    bangCongThuViecDtg.DataContext = busBangChamCongThuViec.getBangChamCongThuViecTheoThang("", namCbx.Text);

                    return;

                }
                bangCongThuViecDtg.DataContext = busBangChamCongThuViec.getBangChamCongThuViecTheoThang(thangCbx.SelectedValue.ToString(), namCbx.Text);
            }
        }

        public void CbxesLoaded()
        {
            for (int i = 1; i <= 12; i++)
            {
                thangCbx.Items.Add(i);
            }

            if (busNhanVien.TimNamDauTienNVVaoLam() < 2000)
            {
                for (int i = busNhanVien.TimNamDauTienNVVaoLam(); i <= (busNhanVien.TimNamGanNhatNVVaoLam() + 20); i++)
                {
                    namCbx.Items.Add(i);
                }
            }
            else
            {
                for (int i = 2000; i <= (busNhanVien.TimNamGanNhatNVVaoLam() + 20); i++)
                {
                    namCbx.Items.Add(i);
                }
            }
            thangCbx.Text = DateTime.Now.Month.ToString();
            namCbx.Text = DateTime.Now.Year.ToString();
        }
    }
}
