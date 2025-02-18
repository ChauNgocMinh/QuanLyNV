using DTO;
using BUS;
using System.Windows;
using System.Windows.Documents;
using System.Collections.Generic;

namespace QuanLyNhanVien.WindowView
{
    /// <summary>
    /// Interaction logic for ThemBaoHiem.xaml
    /// </summary>
    public partial class FormGuiThongBao : Window
    {
        //public BUS_SOBH busBaoHiem = new BUS_SOBH();
        //public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        //public DTO_SOBH suaBaoHiem;     
        //public DTO_SOBH ctBaoHiem;
        public bool checkAdd;
        public string listId {  get; set; }
        public FormGuiThongBao(string _listId)
        {
            InitializeComponent();
            listId = _listId;
        }

        private void btnGui_Click(object sender, RoutedEventArgs e)
        {
            string noiDung = txtNoiDung.Text.Trim();

            if (string.IsNullOrEmpty(noiDung))
            {
                //MessageBox.Show("Vui lòng nhập nội dung thông báo!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //MessageBox.Show($"Thông báo đã được gửi:\n\n{noiDung}", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        // Xử lý nút Hủy
        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Đóng cửa sổ
        }
    }
}
