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
    public partial class ChiTietChinhSua : Window
    {
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        public BUS_PHONGBAN busPhongBan = new BUS_PHONGBAN();
        public BUS_LOAINHANVIEN busLoaiNV = new BUS_LOAINHANVIEN();
        public BUS_BANGLUONG busBangLuong = new BUS_BANGLUONG();
        public DTO_LSCHINHSUA ctChinhSua;
        public bool checkAdd;

        public ChiTietChinhSua()
        {
            InitializeComponent();
        }

        private void huyBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Textblocks_Loaded(object sender, RoutedEventArgs e)
        {
            //if (checkAdd)
            //    return;
            tgChinhSuaTbk.Text = ctChinhSua.Ngaychinhsua.ToString("MM/dd/yyyy hh:mm tt");
            lanCSTbk.Text = ctChinhSua.Lancs.ToString();
            maNVTbk.Text = ctChinhSua.Manv.ToString();
            tenTbk.Text = ctChinhSua.Hoten.ToString();
            ngaySinhTbk.Text = ctChinhSua.Ngaysinh.ToString("MM/dd/yyyy");
            gioiTinhTbk.Text = ctChinhSua.Gioitinh.ToString();
            cccdTbk.Text = ctChinhSua.Cmnd_cccd.ToString();
            noiCapTbk.Text = ctChinhSua.Noicap.ToString();
            chucVuTbk.Text = ctChinhSua.Chucvu.ToString();
            loaiHopDongTbk.Text = ctChinhSua.Loaihd.ToString();
            thoiGianTbk.Text = ctChinhSua.Thoigian.ToString();
            ngayKyTbk.Text = ctChinhSua.Ngaydangki.ToString("MM/dd/yyyy");
            ngayHetHanTbk.Text = ctChinhSua.Ngayhethan.ToString("MM/dd/yyyy");
            soDienThoaiTbk.Text = ctChinhSua.Sdt.ToString();
            hocVanTbk.Text = ctChinhSua.Hocvan.ToString();
            ghiChuTbx.Text = ctChinhSua.Ghichu.ToString();

            danTocTbk.Text = ctChinhSua.Dantoc.ToString();
            loaiNVTbk.Text = busLoaiNV.TimKiemTheoMaLoaiNhanVien(ctChinhSua.Maloainv.ToString());
            phongTbk.Text = busPhongBan.TimKiemTenPhongBanTheoMa(ctChinhSua.Maphong.ToString());
            maLuongTbk.Text = ctChinhSua.Maluong.ToString();
        }
    }
}
