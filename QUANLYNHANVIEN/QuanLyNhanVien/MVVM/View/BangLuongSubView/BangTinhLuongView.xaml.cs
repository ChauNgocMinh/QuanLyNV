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
using System.Text.RegularExpressions;

namespace QuanLyNhanVien.MVVM.View.BangLuongSubView
{
    /// <summary>
    /// Interaction logic for BangTinhLuongView.xaml
    /// </summary>
    public partial class BangTinhLuongView : UserControl
    {
        BUS_BANGTINHLUONG busBangTinhLuong = new BUS_BANGTINHLUONG();
        BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        public BangTinhLuongView()
        {
            InitializeComponent();
            CbxesLoaded();
            bangLuongDtg.DataContext = busBangTinhLuong.getBangTinhLuongTheoThang(thangCbx.SelectedValue.ToString(), namCbx.Text); 
        }

        private void bangTinhLuong_KeyDown(object sender, KeyEventArgs e)
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
                    bangLuongDtg.DataContext = busBangTinhLuong.getBangTinhLuongTheoThang("", namCbx.Text);
                    return;

                }
                bangLuongDtg.DataContext = busBangTinhLuong.getBangTinhLuongTheoThang(thangCbx.SelectedValue.ToString(), namCbx.Text);
            }
        }

        private void lamMoiBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetData();
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
                    p.Workbook.Properties.Title = "Bảng lương nhân viên";
                    p.Workbook.Worksheets.Add("Sheet 1");

                    ExcelWorksheet ws = p.Workbook.Worksheets[0];
                    ws.Name = "Test sheet";
                    ws.Cells.Style.Font.Size = 14;
                    ws.Cells.Style.Font.Name = "Consolas";

                    string[] arrColumnHeader = { "Mã nhân viên", "Lương", "Tháng", "Năm", "Ghi chú" };

                    var countColHeader = arrColumnHeader.Count();

                    ws.Cells[1, 1].Value = "Bảng lương nhân viên";
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
                        dt = busBangTinhLuong.getBangTinhLuongTheoThang(thangCbx.SelectedValue.ToString(), namCbx.Text);
                    }
                    else
                    {
                        dt = busBangTinhLuong.getBangTinhLuong();
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        colIndex = 1;
                        rowIndex++;

                        ws.Cells[rowIndex, colIndex++].Value = dr["Mã nhân viên"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["Lương"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["Tháng"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["Năm"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["Ghi chú"].ToString();

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

        private void numberBoxes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        public void DataGridLoad()
        {
            bangLuongDtg.DataContext = busBangTinhLuong.getBangTinhLuong();
        }

        public void ResetData()
        {
            thangCbx.Text = "";
            namCbx.Text = "";
            DataGridLoad();
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
