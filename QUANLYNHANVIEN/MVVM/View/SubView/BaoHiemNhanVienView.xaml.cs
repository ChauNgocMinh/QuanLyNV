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

namespace QuanLyNhanVien.MVVM.View.SubView
{
    /// <summary>
    /// Interaction logic for BaoHiemNhanVien.xaml
    /// </summary>
    public partial class BaoHiemNhanVienView : UserControl
    {
        public BaoHiemNhanVienView()
        {
            InitializeComponent();
        }

        private void btnThemBaoHiem_Click(object sender, RoutedEventArgs e)
        {
            ThemBaoHiem themBaoHiem = new ThemBaoHiem();
            themBaoHiem.ShowDialog();
        }

        private void btnThemThaiSan_Click(object sender, RoutedEventArgs e)
        {
            ThemThaiSan thaiSan = new ThemThaiSan();
            thaiSan.ShowDialog();
        }
    }
}
