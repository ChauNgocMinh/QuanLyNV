using BUS;
using DTO;
using QuanLyNhanVien.MessageBox;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;

namespace QuanLyNhanVien
{
    /// <summary>
    /// Interaction logic for DangNhap.xaml
    /// </summary>
    public partial class DangNhap : Window
    {
        BUS_TAIKHOAN tk = new BUS_TAIKHOAN();
        BUS_NHANVIENHIENTAI busNhanVienHienTai = new BUS_NHANVIENHIENTAI();
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

        public DangNhap()
        {
            InitializeComponent();
            StartCamera();
            taiKhoanTbx.Focus();
        }
        private void StartCamera()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                bool? result = new MessageBoxCustom("Không tìm thấy camera", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }

            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(Video_NewFrame);
            videoSource.Start();
        }

        private void Video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                eventArgs.Frame.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                bitmapImage.Freeze();
                Dispatcher.Invoke(() => cameraFeed.Source = bitmapImage);
            }
        }

        private void btnDangNhap_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void MinimizedButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnQuenMK_Click(object sender, RoutedEventArgs e)
        {
            QuenMK quenMK = new QuenMK();
            quenMK.ShowDialog();   
        }

        public void Login()
        {
            DTO_TAIKHOAN dTO_TaiKhoan = new DTO_TAIKHOAN();
            dTO_TaiKhoan._TENDANGNHAP = taiKhoanTbx.Text.ToString().Replace(" ","");
            dTO_TaiKhoan._MATKHAU = matKhauPwb.Password.ToString();

            if (tk.KiemTraTonTai(dTO_TaiKhoan._TENDANGNHAP))
            {
                if (tk.KiemTraTaiKhoan(dTO_TaiKhoan))
                {
                    
                    TrangChu trangChu = new TrangChu(dTO_TaiKhoan);
                    //bool? result = new MessageBoxCustom("Đăng nhập thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    trangChu.Show();
                    this.Hide();

                    if (dTO_TaiKhoan._TENDANGNHAP.ToLower() == "admin" || dTO_TaiKhoan._TENDANGNHAP.ToLower() == "manager")
                    {
                        return;
                    }

                    DTO_NHANVIENHIENTAI dtoNhanVienHienTai = new DTO_NHANVIENHIENTAI();

                    dtoNhanVienHienTai.Manv = int.Parse(dTO_TaiKhoan._TENDANGNHAP);

                    busNhanVienHienTai.XoaNhanVienHienTai();
                    busNhanVienHienTai.ThemNhanVienHienTai(dtoNhanVienHienTai);
                }
                else
                {
                    bool? result = new MessageBoxCustom("Sai mật khẩu, vui lòng thử lại.", MessageType.Error, MessageButtons.Ok).ShowDialog();
                    return;
                }
            }
            else
            {
                bool? result = new MessageBoxCustom("Tài khoản không tồn tại.", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }
            
        }
    }
}
