using DTO;
using BUS;
using System.Windows;
using System.Windows.Documents;
using System.Collections.Generic;
using System;
using QuanLyNhanVien.MessageBox;

namespace QuanLyNhanVien.WindowView
{
    /// <summary>
    /// Interaction logic for ThemThongBao.xaml
    /// </summary>
    public partial class FormGuiThongBao : Window
    {
        private readonly BUS_THONGBAO busThongBao;
        private readonly List<int> danhSachNguoiDung;

        public FormGuiThongBao(List<int> _danhSachNguoiDung)
        {
            InitializeComponent();
            LoadLoaiThongBao();
            busThongBao = new BUS_THONGBAO();
            danhSachNguoiDung = _danhSachNguoiDung;
        }

        private void LoadLoaiThongBao()
        {
            List<string> loaiThongBaoList = new List<string>
            {
                "Thông báo",
                "Tin tức",
                "Cảnh báo",
                "Khác"
            };
            cbbLoaiThongBao.ItemsSource = loaiThongBaoList;
            cbbLoaiThongBao.SelectedIndex = 0;
        }

        private void btnGui_Click(object sender, RoutedEventArgs e)
        {
            string loaiThongBao = cbbLoaiThongBao.SelectedItem.ToString();
            string tieuDe = txtTieuDe.Text.Trim();
            string noiDung = txtNoiDung.Text.Trim();

            if (string.IsNullOrEmpty(tieuDe))
            {
                new MessageBoxCustom("Vui lòng nhập tiêu đề!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            if (string.IsNullOrEmpty(noiDung))
            {
                new MessageBoxCustom("Vui lòng nhập nội dung!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                return;
            }

            DTO_BANGTHONGBAO thongBao = new DTO_BANGTHONGBAO
            {
                Tieude = tieuDe,
                Noidung = noiDung,
                Type = loaiThongBao,
                Createdate = DateTime.Now
            };

            bool ketQua = busThongBao.themThongBao(thongBao, danhSachNguoiDung);

            if (ketQua)
            {
                new MessageBoxCustom("Thông báo đã được gửi thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                this.Close();
            }
            else
            {
                new MessageBoxCustom("Gửi thông báo thất bại!", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
