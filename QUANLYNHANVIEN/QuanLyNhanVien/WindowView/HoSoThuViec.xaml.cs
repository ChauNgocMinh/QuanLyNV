using BUS;
using DTO;
using QuanLyNhanVien.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for HoSoThuViec.xaml
    /// </summary>
    public partial class HoSoThuViec : Window
    {
        public BUS_HOSOTHUVIEC busHoSoThuViec = new BUS_HOSOTHUVIEC();
        public DTO_HOSOTHUVIEC suaHoSoThuViec = new DTO_HOSOTHUVIEC();
        public bool checkAdd;

        public HoSoThuViec(bool CheckAdd)
        {
            InitializeComponent();
            checkAdd = CheckAdd;
            LoadData();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }    

        private void maNVTbx_Loaded(object sender, RoutedEventArgs e)
        {
            if (checkAdd)
                return;
            maNVTbx.Text = suaHoSoThuViec.Manvtv.ToString();
            tenTbx.Text = suaHoSoThuViec.Hoten.ToString();
            ngaySinhDpk.Text = suaHoSoThuViec.Ngaysinh.ToString("MM/dd/yyyy");
            gioiTinhCbx.Text = suaHoSoThuViec.Gioitinh.ToString();
            cccdTbx.Text = suaHoSoThuViec.Cmnd_cccd.ToString();
            noiCapTbx.Text = suaHoSoThuViec.Noicap.ToString();
            ngayBatDauDpk.Text = suaHoSoThuViec.Ngaytv.ToString("MM/dd/yyyy");
            viTriTbx.Text = suaHoSoThuViec.Vitrithuviec.ToString();
            soThangTbx.Text = suaHoSoThuViec.Sothangtv.ToString();
            sdtTbx.Text = suaHoSoThuViec.Sdt.ToString();
            hocVanTbx.Text = suaHoSoThuViec.Hocvan.ToString();
            ghiChuTbx.Text = suaHoSoThuViec.Ghichu.ToString();
        }  

        private void themSuaBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckData())
                    return;

                DTO_HOSOTHUVIEC dtoHoSoThuViec = new DTO_HOSOTHUVIEC();

                dtoHoSoThuViec.Hoten = tenTbx.Text;
                dtoHoSoThuViec.Ngaysinh = DateTime.Parse(ngaySinhDpk.Text);
                dtoHoSoThuViec.Gioitinh = gioiTinhCbx.Text;
                dtoHoSoThuViec.Cmnd_cccd = cccdTbx.Text;
                dtoHoSoThuViec.Noicap = noiCapTbx.Text;
                dtoHoSoThuViec.Vitrithuviec = viTriTbx.Text;
                dtoHoSoThuViec.Ngaytv = DateTime.Parse(ngayBatDauDpk.Text);
                dtoHoSoThuViec.Sothangtv = int.Parse(soThangTbx.Text);
                dtoHoSoThuViec.Sdt = sdtTbx.Text;
                dtoHoSoThuViec.Hocvan = hocVanTbx.Text;
                dtoHoSoThuViec.Ghichu = ghiChuTbx.Text;

                if (maNVTbx.Text == string.Empty)
                {
                    busHoSoThuViec.ThemHoSoThuViec(dtoHoSoThuViec);
                    bool? Result = new MessageBoxCustom("Thêm nhân viên thử việc thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                else
                {
                    dtoHoSoThuViec.Manvtv = int.Parse(maNVTbx.Text);
                    busHoSoThuViec.SuaHoSoThuViec(dtoHoSoThuViec);
                    bool? Result = new MessageBoxCustom("Sửa nhân viên thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                this.Close();
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu!\nVui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            
        }

        public bool CheckData()
        {
            if (tenTbx.Text == String.Empty || ngaySinhDpk.Text == String.Empty
                || gioiTinhCbx.Text == String.Empty || cccdTbx.Text == String.Empty
                || ngayBatDauDpk.Text == String.Empty || viTriTbx.Text == String.Empty
                || noiCapTbx.Text == String.Empty || hocVanTbx.Text == String.Empty
                || soThangTbx.Text == String.Empty || sdtTbx.Text == String.Empty)
            {
                bool? Result = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return false;
            }

            if (cccdTbx.Text.Length != 9 && cccdTbx.Text.Length != 12)
            {
                bool? result = new MessageBoxCustom("Vui lòng nhập đúng định dạng\n(CMND: 9 chữ số/CCCD: 12 chữ số)", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return false;
            }

            if (sdtTbx.Text.Length != 10)
            {
                bool? result = new MessageBoxCustom("Vui lòng nhập đúng định dạng số điện thoại 10 số", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return false;
            }
            return true;
        }

        public void LoadData()
        {
            if (checkAdd)
            {
                themSuaBtn.Content = "Thêm";
            }
            else
            {
                themSuaBtn.Content = "Sửa";
                tenTbx.IsEnabled = false;
            }

            string[] listGioiTinh = new string[] { "Nam", "Nữ", "Khác" };

            foreach (string gioiTinh in listGioiTinh)
            {
                gioiTinhCbx.Items.Add(gioiTinh);
            }
        }

        private void numberTextBoxes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void ngaySinhDpk_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ngaySinhDpk.SelectedDate > DateTime.Now.Date.AddYears(-18))
            {
                bool? Result = new MessageBoxCustom("Chưa đủ tuổi vào làm (ít nhất 18 tuổi).", MessageType.Error, MessageButtons.Ok).ShowDialog();
                ngaySinhDpk.Text = "";
                return;
            }
        }

        private void ngayBatDauDpk_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ngayBatDauDpk.SelectedDate < DateTime.Now)
            {
                bool? Result = new MessageBoxCustom("Không thể chọn ngày trong quá khứ.", MessageType.Error, MessageButtons.Ok).ShowDialog();
                ngayBatDauDpk.Text = "";
                return;
            }
        }
    }
}
