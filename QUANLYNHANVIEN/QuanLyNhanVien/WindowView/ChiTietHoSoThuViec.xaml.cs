using BUS;
using DTO;
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
using System.Windows.Shapes;

namespace QuanLyNhanVien.WindowView
{
    /// <summary>
    /// Interaction logic for ChiTietHoSoThuViec.xaml
    /// </summary>
    public partial class ChiTietHoSoThuViec : Window
    {
        public DTO_HOSOTHUVIEC chiTietHoSoThuViec = new DTO_HOSOTHUVIEC();

        public ChiTietHoSoThuViec()
        {
            InitializeComponent();
        }

        private void Textblocks_Loaded(object sender, RoutedEventArgs e)
        {
            maNVTbk.Text = chiTietHoSoThuViec.Manvtv.ToString();
            hotenTbk.Text = chiTietHoSoThuViec.Hoten.ToString();
            ngaySinhTbk.Text = chiTietHoSoThuViec.Ngaysinh.ToString("MM/dd/yyyy");
            gioiTinhTbk.Text = chiTietHoSoThuViec.Gioitinh.ToString();
            cccdTbk.Text = chiTietHoSoThuViec.Cmnd_cccd.ToString();
            noiCapTbk.Text = chiTietHoSoThuViec.Noicap.ToString();
            viTriTbk.Text = chiTietHoSoThuViec.Vitrithuviec.ToString();
            ngayThuViecTbk.Text = chiTietHoSoThuViec.Ngaytv.ToString("MM/dd/yyyy");
            soThangTbk.Text = chiTietHoSoThuViec.Sothangtv.ToString();
            sdtTbk.Text = chiTietHoSoThuViec.Sdt.ToString();
            hocVanTbk.Text = chiTietHoSoThuViec.Hocvan.ToString();
            ghiChuTbx.Text = chiTietHoSoThuViec.Ghichu.ToString();
        }

        private void huyBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
