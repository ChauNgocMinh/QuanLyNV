using BUS;
using DTO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyNhanVien.MVVM.View.NhanVien_ThongTinCaNhanSubView
{
    /// <summary>
    /// Interaction logic for BangChamCongCaNhanView.xaml
    /// </summary>
    public partial class BangChamCongCaNhanView : UserControl
    {
        public BUS_NHANVIENHIENTAI busNhanVienHienTai = new BUS_NHANVIENHIENTAI();
        public BUS_BANGCHAMCONG busBangChamCong = new BUS_BANGCHAMCONG();
        public BUS_BANGLUONG busBangLuong = new BUS_BANGLUONG();

        public BangChamCongCaNhanView()
        {
            InitializeComponent();
            maNVTbx.Text = busNhanVienHienTai.getNhanVienHienTai();
            DataGridLoad("", "");
        }

        private void thoiGianDpk_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridLoad(thoiGianDpk.SelectedDate.Value.Month.ToString(), thoiGianDpk.SelectedDate.Value.Year.ToString());
            maLuongTbx.Text = busBangChamCong.GetMaLuongTheoThang(maNVTbx.Text, thoiGianDpk.SelectedDate.Value.Month.ToString(), thoiGianDpk.SelectedDate.Value.Year.ToString());
            DTO_BANGLUONG dtoBangLuong = busBangLuong.GetChiTietLuong(maLuongTbx.Text);
            DTO_BANGCHAMCONG dtoBangChamCong = busBangChamCong.getBangChamCongNhanVienTheoThang(maNVTbx.Text, thoiGianDpk.SelectedDate.Value.Month, thoiGianDpk.SelectedDate.Value.Year);

            luongCBTbx.Text = dtoBangLuong.Lcb.ToString();
            phuCapTbx.Text = dtoBangLuong.Phucapchucvu.ToString();
            phuCapKhacTbx.Text = dtoBangLuong.Phucapkhac.ToString();
            khenThuongTbx.Text = dtoBangChamCong.Tienkhenthuong.ToString();
            kyLuatTbx.Text = dtoBangChamCong.Tienkyluat.ToString();
            soNgayNghiTbx.Text = dtoBangChamCong.Songaynghi.ToString();
            soNgayCongTbx.Text =dtoBangChamCong.Songaycong.ToString();
            soGioLamThemTbx.Text = dtoBangChamCong.Sogiolamthem.ToString();

            tongTienTbk.Text = (((double.Parse(luongCBTbx.Text) / 26) * int.Parse(soNgayCongTbx.Text)) + (int.Parse(soGioLamThemTbx.Text) * 40000)
                                + double.Parse(phuCapTbx.Text) + double.Parse(phuCapKhacTbx.Text)
                                + double.Parse(khenThuongTbx.Text) - double.Parse(kyLuatTbx.Text)).ToString("#.##");
        }

        private void luongDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bangChamCongCaNhanDtg.SelectedItems.Count == 0)
            {
                return;
            }

            DataRowView row = bangChamCongCaNhanDtg.SelectedItem as DataRowView;

            DTO_BANGLUONG dtoBangLuong = busBangLuong.GetChiTietLuong(row[3].ToString());

            double tongTien = double.Parse(row[0].ToString());
            string selectedMonth = row[1].ToString() + "/" + row[2].ToString();
            
            maLuongTbx.Text = row[3].ToString();
            luongCBTbx.Text = dtoBangLuong.Lcb.ToString();
            phuCapTbx.Text = dtoBangLuong.Phucapchucvu.ToString();
            phuCapKhacTbx.Text = dtoBangLuong.Phucapkhac.ToString();
            khenThuongTbx.Text = row[4].ToString();
            kyLuatTbx.Text = row[5].ToString();
            thoiGianDpk.SelectedDate = DateTime.ParseExact(selectedMonth, "MM/yyyy", null);
            soNgayCongTbx.Text = row[6].ToString();
            soNgayNghiTbx.Text = row[7].ToString();
            soGioLamThemTbx.Text = row[8].ToString();           
            tongTienTbk.Text = tongTien.ToString("#.##");
        }

        public void DataGridLoad(string thang, string nam)
        {
            bangChamCongCaNhanDtg.DataContext = busBangChamCong.getBangChiTietChamCongTheoNhanVien(maNVTbx.Text, thang, nam);
        }
    }
}
