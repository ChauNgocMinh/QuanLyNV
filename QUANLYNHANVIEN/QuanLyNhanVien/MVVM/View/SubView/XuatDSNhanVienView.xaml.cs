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
using Microsoft.Win32;
using System.ComponentModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using LicenseContext = OfficeOpenXml.LicenseContext;
using QuanLyNhanVien.MessageBox;
using QuanLyNhanVien.WindowView;

namespace QuanLyNhanVien.MVVM.View.SubView
{
    /// <summary>
    /// Interaction logic for NhanVienView.xaml
    /// </summary>
    public partial class XuatDSNhanVienView : UserControl
    {
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();

        public XuatDSNhanVienView()
        {
            InitializeComponent();
            DataGridLoad();
        }

        public void DataGridLoad()
        {
            dsNhanVienDtg.DataContext = busNhanVien.getNhanVien();
        }

        private void chiTietBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dsNhanVienDtg.SelectedItems.Count == 0)
            {
                bool? result = new MessageBoxCustom("Vui lòng chọn nhân viên cần xem!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }
            XemChiTiet();
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

        private void dsNhanVienDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
