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
using BUS;
using DTO;
using QuanLyNhanVien.MessageBox;

namespace QuanLyNhanVien.MVVM.View.NhanVien_ThongTinCaNhanSubView
{
    /// <summary>
    /// Interaction logic for BangLuongCaNhanView.xaml
    /// </summary>
    public partial class BangLuongCaNhanView : UserControl
    {
        DTO_NHANVIENHIENTAI dtoNhanVienHienTai = new DTO_NHANVIENHIENTAI();
        BUS_NHANVIENHIENTAI busNhanVienHienTai = new BUS_NHANVIENHIENTAI();
        BUS_THAYDOIBANGLUONG busThayDoiBangLuong = new BUS_THAYDOIBANGLUONG();
        BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        DTO_NHANVIEN dtoNhanVien = new DTO_NHANVIEN();
        BUS_BANGLUONG busBangLuong = new BUS_BANGLUONG();
        DTO_BANGLUONG dtoBangLuong = new DTO_BANGLUONG();

        public BangLuongCaNhanView()
        {
            InitializeComponent();
            DataGridLoad();
            Get_DuLieu();
        }

        public void Get_DuLieu()
        {
            busNhanVienHienTai.getNhanVienHienTai();
            string manv = busNhanVienHienTai.getNhanVienHienTai();
            string maluongcanhan = busNhanVien.GetMaLuong(manv);
            dtoBangLuong = busBangLuong.GetChiTietLuong(maluongcanhan);
            maLuongTbx.Text = dtoBangLuong.Maluong;
            luongCBTbx.Text = dtoBangLuong.Lcb.ToString();
            phuCapTbx.Text  = dtoBangLuong.Phucapchucvu.ToString();
            phuCapKhacTbx.Text = dtoBangLuong.Phucapkhac.ToString();
            ghiChuTbx.Text = dtoBangLuong.Ghichu;
            tongLuongTbx.Text = (double.Parse(luongCBTbx.Text) + double.Parse(phuCapTbx.Text) + double.Parse(phuCapKhacTbx.Text)).ToString();
        }

        public void DataGridLoad()
        {
            dsBangLuongCaNhanDtg.DataContext = busThayDoiBangLuong.getThayDoiBangLuongCaNhan(busNhanVienHienTai.getNhanVienHienTai());
        }

        //private void dsBangLuongCaNhanDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        private void chiTietBtn_Click(object sender, RoutedEventArgs e)
        {
            bool? result = new MessageBoxCustom("Đây chỉ là mức lương cơ bản chưa tính theo\nsố ngày công, số ngày nghỉ, khen thưởng và kỷ luật.", MessageType.Info, MessageButtons.Ok).ShowDialog();
        }
    }
}
