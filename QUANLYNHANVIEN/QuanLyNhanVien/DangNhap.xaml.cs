using BUS;
using DTO;
using QuanLyNhanVien.MessageBox;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Windows.Media;
using OpenCvSharp;
using System.Threading;
using System.Threading.Tasks;
using OpenCvSharp.WpfExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp.Face;
using System.Data;

namespace QuanLyNhanVien
{
    /// <summary>
    /// Interaction logic for DangNhap.xaml
    /// </summary>
    public partial class DangNhap : System.Windows.Window
    {
        private BUS_TAIKHOAN tk = new BUS_TAIKHOAN();
        private BUS_NHANVIENHIENTAI busNhanVienHienTai = new BUS_NHANVIENHIENTAI();
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        private VideoCapture capture;
        private CascadeClassifier faceCascade;
        private bool isCameraRunning = false;
        private CancellationTokenSource cts;
        BUS_LICHSUCHAMCONG busLichSuChamCong = new BUS_LICHSUCHAMCONG();
        BUS_LICHSUVANGMAT busLichSuVangMat = new BUS_LICHSUVANGMAT();
        BUS_BANGCHAMCONG busBangChamCong = new BUS_BANGCHAMCONG();
        BUS_BANGTINHLUONG busBangTinhLuong = new BUS_BANGTINHLUONG();
        private readonly string folderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "CapturedImages");
        public DangNhap()
        {
            InitializeComponent();
            StartCamera();
            LoadSavedFaces();
            taiKhoanTbx.Focus();
        }
        private void StartCamera()
        {
            capture = new VideoCapture(0);
            if (!capture.IsOpened())
            {
                bool? result = new MessageBoxCustom("Không tìm thấy camera", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }

            faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");

            isCameraRunning = true;
            cts = new CancellationTokenSource();

            Task.Run(() => CaptureCamera(cts.Token));
        }

        private void CaptureCamera(CancellationToken token)
        {
            try
            {
                while (isCameraRunning && !token.IsCancellationRequested)
                {
                    using (var frame = new Mat())
                    {
                        capture.Read(frame);
                        if (frame.Empty()) continue;

                        DetectFace(frame);
                        var bitmap = frame.ToBitmapSource();
                        bitmap.Freeze();
                        Dispatcher.Invoke(() =>
                        {
                            if (!token.IsCancellationRequested)
                                cameraFeed.Source = bitmap;
                        });
                    }
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Task CaptureCamera đã bị hủy.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong CaptureCamera: " + ex.Message);
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isCameraRunning = false;
            cts?.Cancel();

            if (capture != null)
            {
                capture.Release();
                capture.Dispose();
            }
        }

        private List<(string fileName, Mat faceMat)> savedFaces = new List<(string, Mat)>();
        private void LoadSavedFaces()
        {
            string[] imageFiles = Directory.GetFiles(folderPath, "*.jpg");
            savedFaces = imageFiles.Select(file => (Path.GetFileName(file), Cv2.ImRead(file, ImreadModes.Grayscale))).ToList();
        }

        private void DetectFace(Mat frame)
        {
            var gray = new Mat();
            Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
            var faces = faceCascade.DetectMultiScale(gray, 1.1, 6, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(30, 30));

            foreach (var rect in faces)
            {
                Cv2.Rectangle(frame, rect, Scalar.Red, 2);
                var faceMat = new Mat(frame, rect);
                string matchedFile = GetMatchingFaceFile(faceMat);
                if (!string.IsNullOrEmpty(matchedFile))
                {
                    Dispatcher.Invoke(() =>
                    {
                        DataTable dtNhanVien = busNhanVien.getNhanVienByFilter(matchedFile);

                        string maNV = dtNhanVien.Rows[0]["MANV"].ToString(); 
                        if (maNV == null)
                        {
                            FaceMatchNotification.Text = "Nhân viên không tồn tại";
                            FaceMatchNotification.Visibility = Visibility.Visible;
                            return;
                        }

                        if(busLichSuChamCong.CheckChamCong(maNV) == true)
                        {
                            FaceMatchNotification.Text = $"Nhân viên đã chấm công trong hôm nay";
                            FaceMatchNotification.Visibility = Visibility.Visible;
                            return;
                        }
                        DTO_LICHSUCHAMCONG dtoLichSuChamCong = new DTO_LICHSUCHAMCONG();
                        double SoGioLam = 0;
                        double SoGioLamThem = 0;

                        DateTime thoiGianHienTai = DateTime.Now;
                        DateTime gioBatDau = thoiGianHienTai.Date.AddHours(8); 
                        DateTime gioKetThuc = thoiGianHienTai.Date.AddHours(17);
                        DateTime? gioCheckInCuoi = busLichSuChamCong.TimLanCuoiChamCongTheoMa(maNV);

                        if (gioCheckInCuoi == null || gioCheckInCuoi.Value.Date != thoiGianHienTai.Date)
                        {
                            dtoLichSuChamCong.Manv = int.Parse(maNV);
                            dtoLichSuChamCong.Ngaychamconggannhat = thoiGianHienTai;

                            busLichSuChamCong.ThemLichSuChamCong(dtoLichSuChamCong);
                            FaceMatchNotification.Text = $"Nhân viên {dtNhanVien.Rows[0]["HOTEN"].ToString()} check-in lúc {thoiGianHienTai:HH:mm}";
                            FaceMatchNotification.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            TimeSpan thoiGianLam = thoiGianHienTai - gioCheckInCuoi.Value;

                            if (gioCheckInCuoi < gioBatDau)
                            {
                                SoGioLam = (thoiGianHienTai - gioBatDau).TotalHours;
                            }
                            else if (gioCheckInCuoi < gioKetThuc)
                            {
                                if (thoiGianHienTai <= gioKetThuc)
                                {
                                    SoGioLam = thoiGianLam.TotalHours;
                                }
                                else
                                {
                                    SoGioLam = (gioKetThuc - gioCheckInCuoi.Value).TotalHours;
                                    SoGioLamThem = (thoiGianHienTai - gioKetThuc).TotalHours;
                                }
                            }
                            else
                            {
                                SoGioLamThem = thoiGianLam.TotalHours;
                            }

                            SoGioLam = Math.Round(SoGioLam, 2);
                            SoGioLamThem = Math.Round(SoGioLamThem, 2);
                            dtoLichSuChamCong.Manv = int.Parse(maNV);
                            dtoLichSuChamCong.Ngaychamconggannhat = thoiGianHienTai;
                            dtoLichSuChamCong.Ghichu = "Đã chốt";
                            busLichSuChamCong.SuaLichSuChamCong(dtoLichSuChamCong);

                            FaceMatchNotification.Text = $"Nhân viên {dtNhanVien.Rows[0]["HOTEN"].ToString()} check-in lúc {thoiGianHienTai:HH:mm}, Giờ làm: {SoGioLam}, OT: {SoGioLamThem}";
                            FaceMatchNotification.Visibility = Visibility.Visible;

                            if (busBangChamCong.KiemTraTonTai(maNV, DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString()))
                            {
                                DTO_BANGCHAMCONG dtoBangChamCong = new DTO_BANGCHAMCONG();
                                dtoBangChamCong.Manv = int.Parse(maNV);
                                dtoBangChamCong.Thang = DateTime.Now.Month;
                                dtoBangChamCong.Nam = DateTime.Now.Year;
                                dtoBangChamCong.Maluong = dtNhanVien.Rows[0]["MALUONG"].ToString();
                                dtoBangChamCong.Tienkhenthuong = 0;
                                dtoBangChamCong.Tienkyluat = 0;
                                double ngayCong = SoGioLam / 8.0;
                                ngayCong = Math.Round(ngayCong, 2); 
                                dtoBangChamCong.Songaycong += ngayCong; dtoBangChamCong.Songaynghi = 0;
                                dtoBangChamCong.Sogiolamthem = SoGioLamThem;

                                busBangChamCong.SuaBangChamCong(dtoBangChamCong);
                            }
                            else
                            {
                                DTO_BANGCHAMCONG dtoBangChamCong = new DTO_BANGCHAMCONG();
                                dtoBangChamCong.Manv = int.Parse(maNV);
                                dtoBangChamCong.Thang = DateTime.Now.Month;
                                dtoBangChamCong.Nam = DateTime.Now.Year;
                                dtoBangChamCong.Maluong = dtNhanVien.Rows[0]["MALUONG"].ToString();
                                dtoBangChamCong.Tienkhenthuong = 0;
                                dtoBangChamCong.Tienkyluat = 0;
                                double ngayCong = SoGioLam / 8.0;
                                ngayCong = Math.Round(ngayCong, 2); 
                                dtoBangChamCong.Songaycong += ngayCong; 
                                dtoBangChamCong.Songaynghi = 0;
                                dtoBangChamCong.Sogiolamthem = SoGioLamThem;

                                busBangChamCong.ThemBangChamCong(dtoBangChamCong);
                            }
                        }

                    });
                }
            }
        }

        private string GetMatchingFaceFile(Mat faceMat)
        {
            foreach (var (fileName, savedFace) in savedFaces)
            {
                if (CompareFaces(faceMat, savedFace))
                {
                    return fileName;
                }
            }
            return string.Empty;
        }

        private bool CompareFaces(Mat img1, Mat img2)
        {
            if (img1.Empty() || img2.Empty())
                return false;

            var orb = ORB.Create();
            var keypoints1 = new KeyPoint[] { };
            var keypoints2 = new KeyPoint[] { };
            var descriptors1 = new Mat();
            var descriptors2 = new Mat();

            orb.DetectAndCompute(img1, null, out keypoints1, descriptors1);
            orb.DetectAndCompute(img2, null, out keypoints2, descriptors2);

            if (descriptors1.Empty() || descriptors2.Empty())
                return false;

            var bf = new BFMatcher(NormTypes.Hamming, crossCheck: true);
            var matches = bf.Match(descriptors1, descriptors2);

            if (matches.Length < 50)
                return false;
            double matchScore = matches.Average(m => m.Distance);
            const double maxScore = 53; 
            Console.WriteLine($"Matches: {matches.Length}, Score: {matchScore}");
            return matchScore < maxScore;
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
