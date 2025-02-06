using System;
using DTO;
using BUS;
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
    /// Interaction logic for ThemBaoHiem.xaml
    /// </summary>
    public partial class ChiTietBaoHiem : Window
    {
        public BUS_SOBH busBaoHiem = new BUS_SOBH();
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        public DTO_SOBH suaBaoHiem;
        public DTO_SOBH ctBaoHiem;
        public bool checkAdd;
        public ChiTietBaoHiem()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void maBHTbx_Loaded(object sender, RoutedEventArgs e)
        {
            if (checkAdd)
                return;
            maBHTbx.Text = ctBaoHiem.Mabh.ToString();
            maNVCbx.Text = ctBaoHiem.Manv.ToString();
            ngayCapTbx.Text = ctBaoHiem.Ngaycapso.ToString();
            noiCapTbx.Text = ctBaoHiem.Noicapso.ToString();
            ghiChuTbx.Text = ctBaoHiem.Ghichu.ToString();
        }
    }
}
