using BUS;
using DTO;
using QuanLyNhanVien.MessageBox;
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

namespace QuanLyNhanVien.WindowView
{
    /// <summary>
    /// Interaction logic for DoiMatKhau.xaml
    /// </summary>
    public partial class DoiMatKhau : Window
    {
        BUS_TAIKHOAN tk = new BUS_TAIKHOAN();
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void MinimizedButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void xacNhanBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tenDangNhapTbx.Text != "" && matKhauTbx.Text != "" && tenTaiKhoanTbx.Text != "" && matKhauMoiTbx.Text != "")
            {
                DTO_TAIKHOAN dTO_TAIKHOAN = new DTO_TAIKHOAN();
                dTO_TAIKHOAN._TENDANGNHAP = tenDangNhapTbx.Text.ToString();
                dTO_TAIKHOAN._TENCHUTAIKHOAN = tenTaiKhoanTbx.Text.ToString();
                dTO_TAIKHOAN._MATKHAU = matKhauTbx.Text.ToString();
                if (tk.KiemTraTonTai(dTO_TAIKHOAN))
                {
                    if(tk.KiemTraTaiKhoan(dTO_TAIKHOAN))
                    {
                        dTO_TAIKHOAN._MATKHAU = matKhauMoiTbx.Text.ToString();
                        tk.SuaTaiKhoan(dTO_TAIKHOAN);
                        bool? result = new MessageBoxCustom("Đổi mật khẩu thành công", MessageType.Success, MessageButtons.Ok).ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        bool? result = new MessageBoxCustom("Sai mật khẩu", MessageType.Error, MessageButtons.Ok).ShowDialog();
                    }
                }
                else
                {
                    bool? result = new MessageBoxCustom("Tài khoản của bạn không có trong dữ liệu!", MessageType.Error, MessageButtons.Ok).ShowDialog();
                }
            }
            else
            {
                bool? result = new MessageBoxCustom("Hãy nhập đầy đủ thông tin!", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }
    }
}
