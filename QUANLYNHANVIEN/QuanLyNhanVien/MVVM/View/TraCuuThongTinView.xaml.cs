using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using BUS;
using DTO;
using QuanLyNhanVien.MessageBox;

namespace QuanLyNhanVien.MVVM.View
{
    /// <summary>
    /// Interaction logic for TraCuuThongTinView.xaml
    /// </summary>
    public partial class TraCuuThongTinView : UserControl
    {
        BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();

        public TraCuuThongTinView()
        {
            InitializeComponent();
            DataGridLoad();
        }
        public void DataGridLoad()
        {
            dsTimKiemThongTinDtg.DataContext = busNhanVien.getNhanVien();
        }

        private void timkiemBtn_Click(object sender, RoutedEventArgs e)
        {
            Loc();
        }

        private void timkiemTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (manvRdBtn.IsChecked == true)
            {
                e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
            }
            if (hotenRdBtn.IsChecked == true)
            {
                e.Handled = new Regex("[0-9]+").IsMatch(e.Text);
            }
            if (sdtRdBtn.IsChecked == true)
            {
                e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
            }
        }

        private void lamMoiBtn_Click(object sender, RoutedEventArgs e)
        {
            timkiemTbx.Text = "";
            DataGridLoad();
        }

        private void timkiemTbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Loc();
            }    
        }

        public void Loc()
        {
            if (timkiemTbx.Text == String.Empty)
            {
                bool? result = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                DataGridLoad();
                return;
            }
            if (manvRdBtn.IsChecked == true)
            {
                dsTimKiemThongTinDtg.DataContext = busNhanVien.TimKiemNhanVienTheoMa(timkiemTbx.Text);
            }
            if (hotenRdBtn.IsChecked == true)
            {
                dsTimKiemThongTinDtg.DataContext = busNhanVien.TimKiemNhanVienTheoTen(timkiemTbx.Text);
            }
            if (sdtRdBtn.IsChecked == true)
            {
                dsTimKiemThongTinDtg.DataContext = busNhanVien.TimKiemNhanVienTheoSDT(timkiemTbx.Text);
            }
        }
    }
}
