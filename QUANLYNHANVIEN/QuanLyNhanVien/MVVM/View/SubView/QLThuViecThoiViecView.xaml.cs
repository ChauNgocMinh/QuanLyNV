using BUS;
using DTO;
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

namespace QuanLyNhanVien.MVVM.View.SubView
{
    /// <summary>
    /// Interaction logic for QLThuViecThoiViecView.xaml
    /// </summary>
    public partial class QLThuViecThoiViecView : UserControl
    {
        BUS_NVTHOIVIEC busNhanVienThoiViec = new BUS_NVTHOIVIEC();
        BUS_HOSOTHUVIEC busHoSoThuViec = new BUS_HOSOTHUVIEC();

        public QLThuViecThoiViecView()
        {
            InitializeComponent();
            DataGridLoad();
        }

        private void suaNhanVienBtn_Click(object sender, RoutedEventArgs e)
        {
            if (thuViecDtg.SelectedItems.Count == 0)
            {
                bool? result = new MessageBoxCustom("Vui lòng chọn nhân viên cần sửa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            DTO_HOSOTHUVIEC suaHoSoThuViec = new DTO_HOSOTHUVIEC();
            DataRowView row = thuViecDtg.SelectedItem as DataRowView;

            HoSoThuViec hoSoThuViec = new HoSoThuViec(false);

            suaHoSoThuViec.Manvtv = int.Parse(row[0].ToString());
            suaHoSoThuViec.Hoten = row[1].ToString();
            suaHoSoThuViec.Ngaysinh = DateTime.Parse(row[2].ToString());
            suaHoSoThuViec.Gioitinh = row[3].ToString();
            suaHoSoThuViec.Cmnd_cccd = row[4].ToString();
            suaHoSoThuViec.Noicap = row[5].ToString();
            suaHoSoThuViec.Vitrithuviec = row[6].ToString();
            suaHoSoThuViec.Ngaytv = DateTime.Parse(row[7].ToString());
            suaHoSoThuViec.Sothangtv = int.Parse(row[8].ToString());
            suaHoSoThuViec.Sdt = row[9].ToString();
            suaHoSoThuViec.Hocvan = row[10].ToString();
            suaHoSoThuViec.Ghichu = row[11].ToString();

            hoSoThuViec.suaHoSoThuViec = suaHoSoThuViec;

            hoSoThuViec.ShowDialog();
            DataGridLoad();
        }

        private void themNhanVienBtn_Click(object sender, RoutedEventArgs e)
        {
            HoSoThuViec hoSoThuViec = new HoSoThuViec(true);
            hoSoThuViec.ShowDialog();
            DataGridLoad();
        }

        private void xoaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (thuViecDtg.SelectedItems.Count == 0)
            {
                bool? Result1 = new MessageBoxCustom("Vui lòng chọn nhân viên cần xóa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            bool? result = new MessageBoxCustom("Xác nhận cho nhân viên thôi thực tập?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (!result.Value)
                return;

            DataRowView row = thuViecDtg.SelectedItem as DataRowView;
            busHoSoThuViec.XoaHoSoThuViec(int.Parse(row[0].ToString()));
            DataGridLoad();
            bool? Result = new MessageBoxCustom("Xóa nhân viên thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();

        }

        private void chiTietBtn_Click(object sender, RoutedEventArgs e)
        {
            XemChiTiet();
        }

        private void thuViecDtg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            XemChiTiet();
        }

        public void DataGridLoad()
        {
            nvThoiViecDtg.DataContext = busNhanVienThoiViec.getNVThoiViec();
            thuViecDtg.DataContext = busHoSoThuViec.getHoSoThuViec();
        }

        public void XemChiTiet()
        {
            if (thuViecDtg.SelectedItems.Count == 0)
            {
                bool? result = new MessageBoxCustom("Vui lòng chọn nhân viên cần xem!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            DTO_HOSOTHUVIEC chiTietHoSoThuViec = new DTO_HOSOTHUVIEC();
            DataRowView row = thuViecDtg.SelectedItem as DataRowView;

            ChiTietHoSoThuViec chiTietHoSo = new ChiTietHoSoThuViec();

            chiTietHoSoThuViec.Manvtv = int.Parse(row[0].ToString());
            chiTietHoSoThuViec.Hoten = row[1].ToString();
            chiTietHoSoThuViec.Ngaysinh = DateTime.Parse(row[2].ToString());
            chiTietHoSoThuViec.Gioitinh = row[3].ToString();
            chiTietHoSoThuViec.Cmnd_cccd = row[4].ToString();
            chiTietHoSoThuViec.Noicap = row[5].ToString();
            chiTietHoSoThuViec.Vitrithuviec = row[6].ToString();
            chiTietHoSoThuViec.Ngaytv = DateTime.Parse(row[7].ToString());
            chiTietHoSoThuViec.Sothangtv = int.Parse(row[8].ToString());
            chiTietHoSoThuViec.Sdt = row[9].ToString();
            chiTietHoSoThuViec.Hocvan = row[10].ToString();
            chiTietHoSoThuViec.Ghichu = row[11].ToString();

            chiTietHoSo.chiTietHoSoThuViec = chiTietHoSoThuViec;

            chiTietHoSo.ShowDialog();
        }
    }
}
