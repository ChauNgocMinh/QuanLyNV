using BUS;
using QuanLyNhanVien.MessageBox;
using QuanLyNhanVien.MVVM.View.BangLuongSubView;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace QuanLyNhanVien.WindowView
{
    /// <summary>
    /// Interaction logic for DanhSachKhenThuong.xaml
    /// </summary>
    public partial class DanhSachKhenThuong : Window
    {
        public BUS_KHENTHUONG busKhenThuong = new BUS_KHENTHUONG();
        public BUS_KYLUAT busKyLuat = new BUS_KYLUAT();
        private double tongTien = 0;

        public double TongTien { get => tongTien; set => tongTien = value; }

        public DanhSachKhenThuong(string type, double tongTien, string tienSanCo)
        {
            InitializeComponent();
            DataGridLoad(type);
            this.TongTien = tongTien;
            tienSanCoTbk.Text = tienSanCo;
            soLuongTbx.Text = "0";
        }

        public void DataGridLoad(string type)
        {
            if (type == "khen thưởng")
            {
                loaiFormTbk.Text = "Danh sách khen thưởng";
                danhSachDtg.DataContext = busKhenThuong.getKhenThuong();
            }
            if (type == "kỷ luật")
            {
                loaiFormTbk.Text = "Danh sách kỷ luật";
                danhSachDtg.DataContext = busKyLuat.getKyLuat();
            }
        }

        private void soLuongTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (soLuongTbx.Text == "")
            {
                tongTienTbk.Text = "0";
                return;
            }
            if (tienTbx.Text == "")
            {
                tongTienTbk.Text = "0";
                return;
            }
            tongTienTbk.Text = (double.Parse(tienTbx.Text) * double.Parse(soLuongTbx.Text)).ToString();
        }

        private void soLuongTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
            if (soLuongTbx.Text == "")
                return;
            if (int.Parse(soLuongTbx.Text) >= 10000)
            {
                bool? result = new MessageBoxCustom("Số lần quá lớn!", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        private void danhSachDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (danhSachDtg.SelectedItems.Count == 0)
            {
                return;
            }

            DataRowView row = danhSachDtg.SelectedItem as DataRowView;

            if (row == null)
            {
                ClearBoxes();
                return;
            }

            tienTbx.Text = row[1].ToString();
            soLuongTbx.Text = "";
        }

        public void ClearBoxes()
        {
            tienTbx.Text = "";
            soLuongTbx.Text = "";
            tongTienTbk.Text = "";
        }

        private void truBotBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.Parse(tienSanCoTbk.Text) < double.Parse(tongTienTbk.Text))
            {
                bool? result = new MessageBoxCustom("Không thể trừ bớt số tiền lớn hơn tiền sẵn có", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }
            if (tongTienTbk.Text == "")
            {
                TongTien = double.Parse(tienSanCoTbk.Text);
            }
            if (tienTbx.Text != "" && soLuongTbx.Text == "")
            {
                tongTienTbk.Text = tienTbx.Text;
            }
            TongTien = double.Parse(tienSanCoTbk.Text) - double.Parse(tongTienTbk.Text);
            this.Close();
        }

        private void congThemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tongTienTbk.Text == "")
            {
                TongTien = double.Parse(tienSanCoTbk.Text);
            }
            if (tienTbx.Text != "" && soLuongTbx.Text == "")
            {
                tongTienTbk.Text = tienTbx.Text;
            }
            TongTien = double.Parse(tienSanCoTbk.Text) + double.Parse(tongTienTbk.Text);
            this.Close();
        }
    }
}
