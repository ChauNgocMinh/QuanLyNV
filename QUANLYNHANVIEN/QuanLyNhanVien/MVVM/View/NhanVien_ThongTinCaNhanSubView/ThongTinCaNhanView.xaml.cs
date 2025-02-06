using DTO;
using QuanLyNhanVien.WindowView;
using System;
using System.Collections.Generic;
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
using DTO;
using BUS;
using QuanLyNhanVien.MessageBox;
using System.Data;
using Microsoft.Win32;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Diagnostics;
using System.IO;

namespace QuanLyNhanVien.MVVM.View.NhanVien_ThongTinCaNhanSubView
{
    /// <summary>
    /// Interaction logic for ThongTinCaNhanView.xaml
    /// </summary>
    public partial class ThongTinCaNhanView : UserControl
    {
        DTO_NHANVIENHIENTAI dtoNhanVienHienTai = new DTO_NHANVIENHIENTAI();
        BUS_NHANVIENHIENTAI busNhanVienHienTai = new BUS_NHANVIENHIENTAI();
        BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        DTO_NHANVIEN dtoNhanVien = new DTO_NHANVIEN();

        public ThongTinCaNhanView()
        {
            InitializeComponent();
            GetMaNV();            
        }

        public void GetMaNV()
        {            
            busNhanVienHienTai.getNhanVienHienTai();
            maNVTbk.Text = busNhanVienHienTai.getNhanVienHienTai().ToString();
        }

        public void GetDuLieu()
        {
            dtoNhanVien = busNhanVien.GetChiTietNhanVienTheoMa(maNVTbk.Text);
            phongTbk.Text = dtoNhanVien.Maphong;
            tenTbk.Text = dtoNhanVien.Hoten;
            ngaySinhTbk.Text = dtoNhanVien.Ngaysinh.ToString("MM/dd/yyyy");
            cccdTbk.Text = dtoNhanVien.Cmnd_cccd;
            noiCapTbk.Text = dtoNhanVien.Noicap;
            ngayKyTbk.Text = dtoNhanVien.Ngaydangki.ToString("MM/dd/yyyy");
            ngayHetHanTbk.Text = dtoNhanVien.Ngayhethan.ToString("MM/dd/yyyy");
            maLuongTbk.Text = dtoNhanVien.Maluong;
            chucVuTbk.Text = dtoNhanVien.Chucvu;
            loaiNVCbk.Text = dtoNhanVien.Maloainv;
            gioiTinhTbk.Text = dtoNhanVien.Gioitinh;
            danTocTbk.Text = dtoNhanVien.Dantoc;
            soDienThoaiTbk.Text = dtoNhanVien.Sdt;
            hocVanTbk.Text = dtoNhanVien.Hocvan;
            loaiHopDongTbk.Text = dtoNhanVien.Loaihd;
            thoiGianTbk.Text = dtoNhanVien.Thoigian.ToString();
            ghiChuTbx.Text = dtoNhanVien.Ghichu;
        }

        private void ExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            DTO_NHANVIEN nv = busNhanVien.GetChiTietNhanVienTheoMa(maNVTbk.Text);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Chọn nơi lưu file Excel",
                FileName = "Thông tin nhân viên.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excel = new ExcelPackage())
                {
                    ExcelWorksheet ws = excel.Workbook.Worksheets.Add("Nhân viên");
                    string[] columnNames = { "Mã NV", "Mã phòng", "Mã lương", "Họ tên", "Ngày sinh", "Giới tính", "Dân tộc", "CMND/CCCD", "Nơi cấp", "Chức vụ", "Mã loại NV", "Loại HĐ", "Thời gian", "Ngày đăng ký", "Ngày hết hạn", "SĐT", "Học vấn", "Ghi chú" };
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        ws.Cells[1, i + 1].Value = columnNames[i];
                        ws.Cells[1, i + 1].Style.Font.Bold = true;
                        ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    }
                    int rowIndex = 2;
                    ws.Cells[rowIndex, 1].Value = nv.manv;
                    ws.Cells[rowIndex, 2].Value = nv.maphong;
                    ws.Cells[rowIndex, 3].Value = nv.maluong;
                    ws.Cells[rowIndex, 4].Value = nv.hoten;
                    ws.Cells[rowIndex, 5].Value = nv.ngaysinh.ToString("dd/MM/yyyy");
                    ws.Cells[rowIndex, 6].Value = nv.gioitinh;
                    ws.Cells[rowIndex, 7].Value = nv.dantoc;
                    ws.Cells[rowIndex, 8].Value = nv.cmnd_cccd;
                    ws.Cells[rowIndex, 9].Value = nv.noicap;
                    ws.Cells[rowIndex, 10].Value = nv.chucvu;
                    ws.Cells[rowIndex, 11].Value = nv.maloainv;
                    ws.Cells[rowIndex, 12].Value = nv.loaihd;
                    ws.Cells[rowIndex, 13].Value = nv.thoigian;
                    ws.Cells[rowIndex, 14].Value = nv.ngaydangki.ToString("dd/MM/yyyy");
                    ws.Cells[rowIndex, 15].Value = nv.ngayhethan.ToString("dd/MM/yyyy");
                    ws.Cells[rowIndex, 16].Value = nv.sdt;
                    ws.Cells[rowIndex, 17].Value = nv.hocvan;
                    ws.Cells[rowIndex, 18].Value = nv.ghichu;
                    ws.Cells[ws.Dimension.Address].AutoFitColumns();
                    File.WriteAllBytes(filePath, excel.GetAsByteArray());
                    new MessageBoxCustom("Xuất Excel thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    Process.Start("explorer.exe", "/select," + filePath);
                }
            }
        }

        private void lichSuBtn_Click(object sender, RoutedEventArgs e)
        {
            XemLichSuChinhSua xemLichSuChinhSua = new XemLichSuChinhSua();
            xemLichSuChinhSua.MaNV = maNVTbk.Text;
            xemLichSuChinhSua.ShowDialog();
        }

        private void maNVTbk_Loaded(object sender, RoutedEventArgs e)
        {
            GetDuLieu();
        }

        private void chinhSuaBtn_Click(object sender, RoutedEventArgs e)
        {
            DTO_NHANVIEN suaNhanVien = new DTO_NHANVIEN();
            ThemNhanVienForm themNhanVienForm = new ThemNhanVienForm(3);

            suaNhanVien.Manv = int.Parse(maNVTbk.Text);
            suaNhanVien.Maphong = phongTbk.Text;
            suaNhanVien.Maluong = maLuongTbk.Text;
            suaNhanVien.Hoten = tenTbk.Text;
            suaNhanVien.Ngaysinh = DateTime.Parse(ngaySinhTbk.Text);
            suaNhanVien.Gioitinh = gioiTinhTbk.Text;
            suaNhanVien.Dantoc = danTocTbk.Text;
            suaNhanVien.Cmnd_cccd = cccdTbk.Text;
            suaNhanVien.Noicap = noiCapTbk.Text;
            suaNhanVien.Chucvu = chucVuTbk.Text;
            suaNhanVien.Maloainv = loaiNVCbk.Text;
            suaNhanVien.Loaihd = loaiHopDongTbk.Text;
            suaNhanVien.Thoigian = int.Parse(thoiGianTbk.Text);
            suaNhanVien.Ngaydangki = DateTime.Parse(ngayKyTbk.Text);
            suaNhanVien.Ngayhethan = DateTime.Parse(ngayHetHanTbk.Text);
            suaNhanVien.Sdt = soDienThoaiTbk.Text;
            suaNhanVien.Hocvan = hocVanTbk.Text;
            suaNhanVien.Ghichu = ghiChuTbx.Text;

            themNhanVienForm.suaNhanVien = suaNhanVien;
            themNhanVienForm.ShowDialog();
            GetDuLieu();
        }
    }
}
