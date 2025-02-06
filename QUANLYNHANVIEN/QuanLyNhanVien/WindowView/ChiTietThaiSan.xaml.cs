using System;
using System.Data;
using BUS;
using DTO;
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
    /// Interaction logic for ThemThaiSan.xaml
    /// </summary>
    public partial class ChiTietThaiSan : Window
    {
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        public BUS_SOTHAISAN busSoThaiSan = new BUS_SOTHAISAN();
        public DTO_SOTHAISAN ctThaiSan;
        
        public bool checkAdd;
        public ChiTietThaiSan()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void maTSTbx_Loaded_1(object sender, RoutedEventArgs e)
        {
            //if (checkAdd)
              //  return;
            maTSTbx.Text = ctThaiSan.Mats.ToString();
            maNVCbx.Text = ctThaiSan.Manv.ToString();
            ngayNghiSinhTbx.Text = ctThaiSan.Ngaynghisinh.ToString();
            ngayVeSomTbx.Text = ctThaiSan.Ngayvesom.ToString();
            ngayLamTLTbx.Text = ctThaiSan.Ngaylamtrolai.ToString();
            troCapTbx.Text = ctThaiSan.Trocapcty.ToString();
            ghiChuTbx.Text = ctThaiSan.Ghichu.ToString();
        }
    }
}
