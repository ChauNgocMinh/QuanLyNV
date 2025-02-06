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
using DTO;
using BUS;
using System.Data;
using QuanLyNhanVien.MVVM.View.SubView;

namespace QuanLyNhanVien.WindowView
{
    /// <summary>
    /// Interaction logic for LyDoNghiViec.xaml
    /// </summary>
    public partial class LyDoNghiViec : Window
    {
        public DTO_NVTHOIVIEC dtoNVThoiViec = new DTO_NVTHOIVIEC();
        public BUS_NVTHOIVIEC busNVThoiViec = new BUS_NVTHOIVIEC();

        public LyDoNghiViec()
        {
            InitializeComponent();
        }

        private void hoanThanhBtn_Click(object sender, RoutedEventArgs e)
        {
            dtoNVThoiViec.Lydo = lyDoNghiViecTbx.Text;
            dtoNVThoiViec.Ngaythoiviec = DateTime.Now;
            busNVThoiViec.ThemNVThoiViec(dtoNVThoiViec);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tenNVTbk.Text = dtoNVThoiViec.Hoten;
            maNVTbk.Text = dtoNVThoiViec.Manv.ToString();
        }
    }
}
