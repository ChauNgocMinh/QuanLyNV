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
    /// Interaction logic for ChiTietNhanVienForm.xaml
    /// </summary>
    public partial class ChiTietNhanVienForm : Window
    {
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        public BUS_PHONGBAN busPhongBan = new BUS_PHONGBAN();
        public BUS_LOAINHANVIEN busLoaiNV = new BUS_LOAINHANVIEN();
        public BUS_BANGLUONG busBangLuong = new BUS_BANGLUONG();
        public DTO_NHANVIEN ctNhanVien;

        public ChiTietNhanVienForm()
        {
            InitializeComponent();
        }

        private void huyBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Textblocks_Loaded(object sender, RoutedEventArgs e)
        {
            maNVTbk.Text = ctNhanVien.Manv.ToString();
            tenTbk.Text = ctNhanVien.Hoten.ToString();
            ngaySinhTbk.Text = ctNhanVien.Ngaysinh.ToString("MM/dd/yyyy");
            gioiTinhTbk.Text = ctNhanVien.Gioitinh.ToString();
            cccdTbk.Text = ctNhanVien.Cmnd_cccd.ToString();
            noiCapTbk.Text = ctNhanVien.Noicap.ToString();
            chucVuTbk.Text = ctNhanVien.Chucvu.ToString();
            loaiHopDongTbk.Text = ctNhanVien.Loaihd.ToString();
            thoiGianTbk.Text = ctNhanVien.Thoigian.ToString();
            ngayKyTbk.Text = ctNhanVien.Ngaydangki.ToString("MM/dd/yyyy");
            ngayHetHanTbk.Text = ctNhanVien.Ngayhethan.ToString("MM/dd/yyyy");
            soDienThoaiTbk.Text = ctNhanVien.Sdt.ToString();
            hocVanTbk.Text = ctNhanVien.Hocvan.ToString();
            ghiChuTbx.Text = ctNhanVien.Ghichu.ToString();

            danTocTbk.Text = ctNhanVien.Dantoc.ToString();
            loaiNVTbk.Text = busLoaiNV.TimKiemTheoMaLoaiNhanVien(ctNhanVien.Maloainv.ToString());
            phongTbk.Text = busPhongBan.TimKiemTenPhongBanTheoMa(ctNhanVien.Maphong.ToString());
            maLuongTbk.Text = ctNhanVien.Maluong.ToString();
        }
    }
}
