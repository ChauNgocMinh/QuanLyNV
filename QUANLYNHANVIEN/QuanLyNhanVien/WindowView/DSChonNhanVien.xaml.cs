using BUS;
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
using System.Windows.Shapes;

namespace QuanLyNhanVien.WindowView
{
    /// <summary>
    /// Interaction logic for DSChonNhanVien.xaml
    /// </summary>
    public partial class DSChonNhanVien : Window
    {
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        private string maNV = "";

        public string MaNV { get => maNV; set => maNV = value; }

        public DSChonNhanVien(string maNV)
        {
            InitializeComponent();
            this.maNV = maNV;
            DataGridLoad();
        }

        public void DataGridLoad()
        {
            danhSachDtg.DataContext = busNhanVien.getNhanVien();
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

            maNVTbx.Text = row[0].ToString();
            hoTenTbx.Text = row[3].ToString();
            cccdTbx.Text = row[7].ToString();
            phongTbx.Text = row[1].ToString();
        }

        public void ClearBoxes()
        {
            maNVTbx.Text = "";
            hoTenTbx.Text = "";
            cccdTbx.Text = "";
            phongTbx.Text = "";
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void hoanThanhBtn_Click(object sender, RoutedEventArgs e)
        {
            maNV = maNVTbx.Text;
            this.Close();
        }
    }
}
