﻿using BUS;
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

namespace QuanLyNhanVien
{
    /// <summary>
    /// Interaction logic for DangNhap.xaml
    /// </summary>
    public partial class DangNhap : System.Windows.Window
    {
        private BUS_TAIKHOAN tk = new BUS_TAIKHOAN();
        private BUS_NHANVIENHIENTAI busNhanVienHienTai = new BUS_NHANVIENHIENTAI();
        private VideoCapture capture;
        private CascadeClassifier faceCascade;
        private bool isCameraRunning = false;
        private CancellationTokenSource cts;

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

        // Load ảnh đã lưu vào bộ nhớ khi khởi động
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

                // Cắt ảnh khuôn mặt ra từ frame
                var faceMat = new Mat(frame, rect);

                // Kiểm tra khuôn mặt với ảnh đã lưu
                string matchedFile = GetMatchingFaceFile(faceMat);
                if (!string.IsNullOrEmpty(matchedFile))
                {
                    Dispatcher.Invoke(() =>
                    {
                        new MessageBoxCustom($"Khuôn mặt trùng khớp với ảnh: {matchedFile}", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    });
                }
            }
        }

        // Kiểm tra khuôn mặt trùng với file nào
        private string GetMatchingFaceFile(Mat faceMat)
        {
            foreach (var (fileName, savedFace) in savedFaces)
            {
                if (CompareFaces(faceMat, savedFace))
                {
                    return fileName; // Trả về tên file nếu khớp
                }
            }
            return string.Empty;
        }

        // So sánh hai khuôn mặt dùng ORB + BFMatcher
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

            if (matches.Length < 35) // Số điểm match tối thiểu để tránh nhận diện sai
                return false;

            double matchScore = matches.Average(m => m.Distance);
            return matchScore < 39; // Điều chỉnh ngưỡng tùy theo độ chính xác mong muốn
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
