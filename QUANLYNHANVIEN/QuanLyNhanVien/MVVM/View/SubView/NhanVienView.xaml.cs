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
using System.Data;
using QuanLyNhanVien.MessageBox;
using Microsoft.Win32;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.IO;

namespace QuanLyNhanVien.MVVM.View.SubView
{
    /// <summary>
    /// Interaction logic for NhanVienView.xaml
    /// </summary>
    public partial class NhanVienView : UserControl
    {
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        public DTO_NHANVIEN dtoNhanVien = new DTO_NHANVIEN();
        public BUS_TAIKHOAN busTaiKhoan = new BUS_TAIKHOAN();
        public BUS_PHONGBAN busPhongBan = new BUS_PHONGBAN();
        public BUS_LSCHINHSUA busLSChinhSua = new BUS_LSCHINHSUA();
        public BUS_BANGCHAMCONG busBangChamCong = new BUS_BANGCHAMCONG();
        public BUS_LICHSUVANGMAT busLichSuVangMat = new BUS_LICHSUVANGMAT();
        public BUS_LICHSUCHAMCONG busLSChamCong = new BUS_LICHSUCHAMCONG();
        public BUS_BANGTINHLUONG busBangTinhLuong = new BUS_BANGTINHLUONG();
        public BUS_SOTHAISAN busSoThaiSan = new BUS_SOTHAISAN();
        public BUS_THAYDOIBANGLUONG busThayDoiBangLuong = new BUS_THAYDOIBANGLUONG();
        public BUS_SOBH busSoBH = new BUS_SOBH();
        public BUS_BOPHAN busBoPhan = new BUS_BOPHAN();

        public NhanVienView()
        {
            InitializeComponent();
            DataGridLoad();
            ComboBoxesLoad();
        }

        private void themBtn_Click(object sender, RoutedEventArgs e)
        {
            ThemNhanVienForm themNhanVienForm = new ThemNhanVienForm(1);
            themNhanVienForm.ShowDialog();
            
            DataGridLoad();
            ClearBoxes();
        }

        private void suaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dsNhanVienDtg.SelectedItems.Count == 0)
            {
                bool? result = new MessageBoxCustom("Vui lòng chọn nhân viên cần sửa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            DTO_NHANVIEN suaNhanVien = new DTO_NHANVIEN();
            DataRowView row = dsNhanVienDtg.SelectedItem as DataRowView;
            ThemNhanVienForm themNhanVienForm = new ThemNhanVienForm(2);

            suaNhanVien.Manv = int.Parse(row[0].ToString());
            suaNhanVien.Maphong = row[1].ToString();
            suaNhanVien.Maluong = row[2].ToString();
            suaNhanVien.Hoten = row[3].ToString();
            suaNhanVien.Ngaysinh = DateTime.Parse(row[4].ToString());
            suaNhanVien.Gioitinh = row[5].ToString();
            suaNhanVien.Dantoc = row[6].ToString();
            suaNhanVien.Cmnd_cccd = row[7].ToString();
            suaNhanVien.Noicap = row[8].ToString();
            suaNhanVien.Chucvu = row[9].ToString();
            suaNhanVien.Maloainv = row[10].ToString();
            suaNhanVien.Loaihd = row[11].ToString();
            suaNhanVien.Thoigian = int.Parse(row[12].ToString());
            suaNhanVien.Ngaydangki = DateTime.Parse(row[13].ToString());
            suaNhanVien.Ngayhethan = DateTime.Parse(row[14].ToString());
            suaNhanVien.Sdt = row[15].ToString();
            suaNhanVien.Hocvan = row[16].ToString();
            suaNhanVien.Ghichu = row[17].ToString();

            themNhanVienForm.suaNhanVien = suaNhanVien;
            themNhanVienForm.ShowDialog();
            DataGridLoad();
            ClearBoxes();
        }

        private void xoaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dsNhanVienDtg.SelectedItems.Count == 0)
            {
                bool? Result1 = new MessageBoxCustom("Vui lòng chọn nhân viên cần xóa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            bool? result = new MessageBoxCustom("Xác nhận cho nhân viên nghỉ việc?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (!result.Value)
                return;

            DataRowView row = dsNhanVienDtg.SelectedItem as DataRowView;
            DTO_NVTHOIVIEC dtoNVThoiViec = new DTO_NVTHOIVIEC();
            dtoNVThoiViec.Manv = int.Parse(row[0].ToString());
            dtoNVThoiViec.Hoten = row[3].ToString();
            dtoNVThoiViec.Cmnd_cccd = row[7].ToString();

            LyDoNghiViec lyDoNghiViec = new LyDoNghiViec();
            lyDoNghiViec.dtoNVThoiViec = dtoNVThoiViec;
            lyDoNghiViec.ShowDialog();

            busNhanVien.XoaNhanVien(dtoNhanVien.Manv);

            if (busLSChinhSua.KiemTraTonTaiNhanVien(dtoNhanVien.Manv.ToString()))
            {
                busLSChinhSua.SuaGhiChu("Đã nghỉ việc" ,dtoNhanVien.Manv.ToString());
            }

            if (busBangChamCong.KiemTraTonTaiNhanVien(dtoNhanVien.Manv.ToString()))
            {
                busBangChamCong.SuaGhiChu("Đã nghỉ việc", dtoNhanVien.Manv.ToString());
            }

            if (busLichSuVangMat.KiemTraTonTaiNhanVien(dtoNhanVien.Manv.ToString()))
            {
                busLichSuVangMat.XoaLichSuVangMat(dtoNhanVien.Manv);
            }

            if (busLSChamCong.KiemTraTonTai(dtoNhanVien.Manv.ToString()))
            {
                busLSChamCong.XoaLichSuChamCong(dtoNhanVien.Manv);
            }

            if (busBangTinhLuong.KiemTraTonTaiNhanVien(dtoNhanVien.Manv.ToString()))
            {
                busBangTinhLuong.SuaGhiChu("Đã nghỉ việc", dtoNhanVien.Manv.ToString());
            }

            if (busSoThaiSan.KiemTraTonTai(dtoNhanVien.Manv.ToString()))
            {
                busSoThaiSan.SuaGhiChu("Đã nghỉ việc", dtoNhanVien.Manv.ToString());
            }

            if (busSoBH.KiemTraTonTaiNhanVien(dtoNhanVien.Manv.ToString()))
            {
                busSoBH.SuaGhiChu("Đã nghỉ việc", dtoNhanVien.Manv.ToString());
            }

            if (busThayDoiBangLuong.KiemTraTonTaiThayDoiBangLuongTheoNhanVien(dtoNhanVien.Manv.ToString()))
            {
                busThayDoiBangLuong.XoaThayDoiBangLuongCuaNhanVien(dtoNhanVien.Manv);
            }

            if (busTaiKhoan.KiemTraTonTai(dtoNhanVien.Manv.ToString()))
            {
                busTaiKhoan.XoaTaiKhoan(dtoNhanVien.Manv);
            }

            DataGridLoad();
            bool? Result = new MessageBoxCustom("Xóa nhân viên thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            ClearBoxes();
        }

        private void dsNhanVienDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dsNhanVienDtg.SelectedItems.Count == 0) return;
            DataRowView row = dsNhanVienDtg.SelectedItem as DataRowView;

            if (row == null)
            {
                ClearBoxes();
                return;
            }

            dtoNhanVien.Manv = int.Parse(row[0].ToString());
            boPhanCbx.SelectedItem = busBoPhan.TimKiemTheoMaBoPhan(busPhongBan.TimKiemBoPhanTheoPhong(row[1].ToString()));
            phongCbx.SelectedItem = busPhongBan.TimKiemTenPhongBanTheoMa(row[1].ToString());
            tenNVTbx.Text = row[3].ToString();
        }

        private void lamMoiBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            DataGridLoad();
        }

        private void boPhanCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            phongCbx.Items.Clear();

            if (boPhanCbx.SelectedIndex == -1)
            {
                DataGridLoad();
                foreach (var tenPhong in busPhongBan.TongHopPhongBan(""))
                {
                    phongCbx.Items.Add(tenPhong);
                }
                return;
            }

            foreach (var tenPhong in busPhongBan.TongHopPhongBan(busBoPhan.TimKiemTheoTenBoPhan(boPhanCbx.SelectedItem.ToString())))
            {
                phongCbx.Items.Add(tenPhong);
            }
        }

        private void xuatDSBtn_Click(object sender, RoutedEventArgs e)
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
                    p.Workbook.Properties.Title = "Danh sách nhân viên";
                    p.Workbook.Worksheets.Add("Sheet 1");

                    ExcelWorksheet ws = p.Workbook.Worksheets[0];
                    ws.Name = "Test sheet";
                    ws.Cells.Style.Font.Size = 14;
                    ws.Cells.Style.Font.Name = "Consolas";

                    string[] arrColumnHeader = { "Mã nhân viên", "Mã phòng", "Mã lương", "Họ tên", "Ngày sinh",
                                                "Giới tính", "Dân tộc", "CMND/CCCD", "Nơi cấp", "Chức vụ",
                                                "Mã loại nhân viên", "Loại hợp đồng", "Thời gian (năm)", "Ngày ký",
                                                "Ngày hết hạn", "Số điện thoại", "Học vấn", "Ghi chú" };

                    var countColHeader = arrColumnHeader.Count();

                    ws.Cells[1, 1].Value = "Danh sách nhân viên";
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
                    dt = busNhanVien.xuatNhanVien();

                    foreach (DataRow dr in dt.Rows)
                    {
                        colIndex = 1;
                        rowIndex++;

                        ws.Cells[rowIndex, colIndex++].Value = dr["MANV"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["MAPHONG"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["MALUONG"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["HOTEN"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["NGAYSINH"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["GIOITINH"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["DANTOC"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["CMND_CCCD"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["NOICAP"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["CHUCVU"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["MALOAINV"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["LOAIHD"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["THOIGIAN"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["NGAYKY"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["NGAYHETHAN"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["SDT"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = dr["HOCVAN"].ToString();
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

        private void locBtn_Click(object sender, RoutedEventArgs e)
        {
            LocNhanVien();
        }

        private void tenNVTbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LocNhanVien();
            }
        }

        private void dsNhanVienDtg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dsNhanVienDtg.SelectedItems.Count == 0)
            {
                return;
            }
            XemChiTiet();
        }

        public void DataGridLoad()
        {
            dsNhanVienDtg.DataContext = busNhanVien.getNhanVien();
        }

        public void ClearBoxes()
        {
            phongCbx.Text = "";
            boPhanCbx.Text = "";
            tenNVTbx.Text = "";
        }

        public void ComboBoxesLoad()
        {
            boPhanCbx.Text = "";
            phongCbx.Text = "";
            foreach (var tenPhong in busPhongBan.TongHopPhongBan(""))
            {
                phongCbx.Items.Add(tenPhong);
            }

            foreach (var tenBoPhan in busBoPhan.TongHopTenBoPhan())
            {
                boPhanCbx.Items.Add(tenBoPhan);
            }
        }

        public void LocNhanVien()
        {
            if (phongCbx.Text == string.Empty && tenNVTbx.Text == string.Empty)
            {
                DataGridLoad();
                return;
            }

            if (phongCbx.Text != string.Empty && tenNVTbx.Text == string.Empty)
            {
                dsNhanVienDtg.DataContext = busNhanVien.TongHopNhanVienTheoPhong(busPhongBan.TimKiemMaPhongBan(phongCbx.SelectedItem.ToString()), "");
            }

            if (phongCbx.Text == string.Empty && tenNVTbx.Text != string.Empty)
            {
                dsNhanVienDtg.DataContext = busNhanVien.TongHopNhanVienTheoPhong("", tenNVTbx.Text);
            }

            if (phongCbx.Text != string.Empty && tenNVTbx.Text != string.Empty)
            {
                dsNhanVienDtg.DataContext = busNhanVien.TongHopNhanVienTheoPhong(busPhongBan.TimKiemMaPhongBan(phongCbx.SelectedItem.ToString()), tenNVTbx.Text);
            }
        }

        public void XemChiTiet()
        {
            DTO_NHANVIEN ctNhanVien = new DTO_NHANVIEN();
            DataRowView row = dsNhanVienDtg.SelectedItem as DataRowView;
            ChiTietNhanVienForm chiTietNhanVienForm = new ChiTietNhanVienForm();

            ctNhanVien.Manv = int.Parse(row[0].ToString());
            ctNhanVien.Maphong = row[1].ToString();
            ctNhanVien.Maluong = row[2].ToString();
            ctNhanVien.Hoten = row[3].ToString();
            ctNhanVien.Ngaysinh = DateTime.Parse(row[4].ToString());
            ctNhanVien.Gioitinh = row[5].ToString();
            ctNhanVien.Dantoc = row[6].ToString();
            ctNhanVien.Cmnd_cccd = row[7].ToString();
            ctNhanVien.Noicap = row[8].ToString();
            ctNhanVien.Chucvu = row[9].ToString();
            ctNhanVien.Maloainv = row[10].ToString();
            ctNhanVien.Loaihd = row[11].ToString();
            ctNhanVien.Thoigian = int.Parse(row[12].ToString());
            ctNhanVien.Ngaydangki = DateTime.Parse(row[13].ToString());
            ctNhanVien.Ngayhethan = DateTime.Parse(row[14].ToString());
            ctNhanVien.Sdt = row[15].ToString();
            ctNhanVien.Hocvan = row[16].ToString();
            ctNhanVien.Ghichu = row[17].ToString();

            chiTietNhanVienForm.ctNhanVien = ctNhanVien;
            chiTietNhanVienForm.ShowDialog();
            DataGridLoad();
        }

        private void quanLyNhanVien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LocNhanVien();
            }
        }
    }
}
