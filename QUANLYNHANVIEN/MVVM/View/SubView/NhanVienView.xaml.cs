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
    /// Interaction logic for NhanVienView.xaml
    /// </summary>
    public partial class NhanVienView : UserControl
    {
        public NhanVienView()
        {
            InitializeComponent();
        }

        private void btnThemNhanVien_Click(object sender, RoutedEventArgs e)
        {
            ThemNhanVienForm themNhanVienForm = new ThemNhanVienForm();
            themNhanVienForm.ShowDialog();
        }

        private void btnSuaNhanSu_Click(object sender, RoutedEventArgs e)
        {
            ThemNhanVienForm themNhanVienForm = new ThemNhanVienForm();
            themNhanVienForm.ShowDialog();
        }
    }
}
