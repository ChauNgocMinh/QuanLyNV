using BUS;
using DTO;
using QuanLyNhanVien.MessageBox;
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

namespace QuanLyNhanVien.MVVM.View.HeThongSubView
{
    /// <summary>
    /// Interaction logic for TaiKhoanView.xaml
    /// </summary>
    public partial class TaiKhoanView : UserControl
    {
        public BUS_PHANLOAITK busPLTK = new BUS_PHANLOAITK();
        public BUS_TAIKHOAN busTaiKhoan = new BUS_TAIKHOAN();
        public DTO_TAIKHOAN dtoTaiKhoan = new DTO_TAIKHOAN();
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        public TaiKhoanView()
        {
            InitializeComponent();
            DataGridLoad();
        }

        public void DataGridLoad()
        {
            taiKhoanDtg.DataContext = busTaiKhoan.getTaiKhoan();
        }

        private void taiKhoanDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (taiKhoanDtg.SelectedItems.Count == 0) return;
            DataRowView row = taiKhoanDtg.SelectedItem as DataRowView;

            if (row == null)
            {
                ClearBoxes();
                return;
            }

            dtoTaiKhoan._MATK = Convert.ToInt32(row[0].ToString());
            busTaiKhoan.LayMatKhau(dtoTaiKhoan);
            maNVCbx.Text = row[3].ToString();
            tenChuTaiKhoanTbx.Text = row[2].ToString();
            matKhauTbx.Text = row[4].ToString();
        }

        public void ClearBoxes()
        {
            maNVCbx.Text = tenChuTaiKhoanTbx.Text = xacNhanMKTbx.Text = matKhauTbx.Text = "";
        }

        private void nhapLaiBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
        }

        private void themBtn_Click(object sender, RoutedEventArgs e)
        {
            bool? show;
            if (matKhauTbx.Text != xacNhanMKTbx.Text)
            {
                show = new MessageBoxCustom("Vui lòng xác nhận lại mật khẩu!", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }
            
            dtoTaiKhoan._TENCHUTAIKHOAN = tenChuTaiKhoanTbx.Text;
            dtoTaiKhoan._TENDANGNHAP = maNVCbx.Text;
            dtoTaiKhoan._MATKHAU = matKhauTbx.Text;
            dtoTaiKhoan._MALOAITK = 3;

            if (busTaiKhoan.KiemTraTonTai(dtoTaiKhoan))
            {
                show  = new MessageBoxCustom("Tài khoản đã tồn tại!", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }

            busTaiKhoan.ThemTaiKhoan(dtoTaiKhoan);
            show = new MessageBoxCustom("Thêm thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            DataGridLoad();
            ClearBoxes();
        }

        private void suaBtn_Click(object sender, RoutedEventArgs e)
        {
            bool? show;

            if (matKhauTbx.Text != xacNhanMKTbx.Text)
            {
                show = new MessageBoxCustom("Vui lòng xác nhận lại mật khẩu!", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }

            dtoTaiKhoan._TENCHUTAIKHOAN = tenChuTaiKhoanTbx.Text;
            dtoTaiKhoan._TENDANGNHAP = maNVCbx.Text;
            dtoTaiKhoan._MATKHAU = matKhauTbx.Text;

            if (!busTaiKhoan.KiemTraTonTai(dtoTaiKhoan))
            {
                show = new MessageBoxCustom("Tài khoản không tồn tại!", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }

            busTaiKhoan.SuaTaiKhoan(dtoTaiKhoan);
            show = new MessageBoxCustom("Sửa thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            DataGridLoad();
            ClearBoxes();
        }

        private void xoaBtn_Click(object sender, RoutedEventArgs e)
        {
            bool? show;
            dtoTaiKhoan._TENCHUTAIKHOAN = tenChuTaiKhoanTbx.Text;
            dtoTaiKhoan._TENDANGNHAP = maNVCbx.Text;
            dtoTaiKhoan._MATKHAU = matKhauTbx.Text;
            dtoTaiKhoan._MALOAITK = 3;

            if (taiKhoanDtg.SelectedItems.Count == 0)
            {
                show = new MessageBoxCustom("Chọn tài khoản muốn xóa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            DataRowView row = taiKhoanDtg.SelectedItem as DataRowView;
            busTaiKhoan.XoaTaiKhoan(int.Parse(row[0].ToString()));
            show = new MessageBoxCustom("Xóa thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            DataGridLoad();
            ClearBoxes();
        }

        private void maNVCbx_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var maNV in busNhanVien.TongHopMaNhanVien())
            {
                maNVCbx.Items.Add(maNV);
            }
        }

        private void maNVCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (maNVCbx.SelectedIndex == -1)
                return;
            tenChuTaiKhoanTbx.Text = busNhanVien.TimTenNVTheoMa(maNVCbx.SelectedValue.ToString());
        }
    }
}
