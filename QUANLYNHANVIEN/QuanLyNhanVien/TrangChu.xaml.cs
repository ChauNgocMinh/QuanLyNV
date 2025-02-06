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
using System.Windows.Threading;
using QuanLyNhanVien.Properties;
using BUS;

namespace QuanLyNhanVien
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TrangChu : Window
    {
        public BUS_PHANLOAITK busPhanLoaiTK = new BUS_PHANLOAITK();
        private string maNV = string.Empty;

        public string MaNV { get => maNV; set => maNV = value; }

        public TrangChu(DTO_TAIKHOAN dtoTaiKhoan)
        {
            MaNV = dtoTaiKhoan._TENDANGNHAP;
            InitializeComponent();
            AccountButton.Content = dtoTaiKhoan._TENCHUTAIKHOAN;
            if (dtoTaiKhoan._MALOAITK == 1)
            {
                thongTinCaNhanRbn.Visibility = Visibility.Collapsed;
                loaiTaiKhoanTbk.Text = "Quản trị hệ thống";
                //thongTinCaNhanRbn.Visibility = Visibility.Collapsed;
                //Settings.Visibility = Visibility.Collapsed;
                //Report.Visibility = Visibility.Collapsed;
            }
            else if (dtoTaiKhoan._MALOAITK == 2)
            {
                heThongRbn.Visibility = Visibility.Collapsed;
                thongTinCaNhanRbn.Visibility = Visibility.Collapsed;
                loaiTaiKhoanTbk.Text = "Quản lý nhân viên";
            }
            else if (dtoTaiKhoan._MALOAITK == 3)
            {
                bangLuongRbn.Visibility = Visibility.Collapsed;
                baoCaoRbn.Visibility = Visibility.Collapsed;
                boPhanRbn.Visibility = Visibility.Collapsed;
                chamCongRbn.Visibility = Visibility.Collapsed;
                heThongRbn.Visibility = Visibility.Collapsed;
                nhanVienRbn.Visibility = Visibility.Collapsed;
                traCuuRbn.Visibility= Visibility.Collapsed;
                loaiTaiKhoanTbk.Text = "Nhân viên";
            }

            tenNVTbk.Text = dtoTaiKhoan._TENCHUTAIKHOAN;
            
            Timer.Text = DateTime.Now.ToString("MM/dd/yyyy");
            //loaiTaiKhoanTbk.Text = dtoTaiKhoan._MALOAITK;
            //StartClock();
        }

        private void MinimizedButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                MaximizeButton_Image.Source = new BitmapImage(new Uri("/Images/DoubleQuadButton.png", UriKind.Relative));
                WindowState = WindowState.Maximized;

                MainBorder.CornerRadius = new CornerRadius(0);
                MainBorder.BorderThickness = new Thickness(0);
            }
            else
            {
                MaximizeButton_Image.Source = new BitmapImage(new Uri(@"/Images/QuadButton.png", UriKind.Relative));
                WindowState = WindowState.Normal;

                MainBorder.CornerRadius = new CornerRadius(20);
                MainBorder.BorderThickness = new Thickness(5);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            DangNhap dangNhap = new DangNhap();
            dangNhap.Show();
            this.Close();
        }

        private void btnDoiMK_Click(object sender, RoutedEventArgs e)
        {
            DoiMatKhau doiMatKhau = new DoiMatKhau();
            doiMatKhau.ShowDialog();
        }

        //private void StartClock()
        //{
        //    DispatcherTimer timer = new DispatcherTimer();
        //    timer.Interval = TimeSpan.FromSeconds(1);
        //    timer.Tick += TickEvent;
        //    timer.Start();
        //}
    }
}
