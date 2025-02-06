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
    /// Interaction logic for ChamCongThuViec.xaml
    /// </summary>
    public partial class ChamCongThuViec : Window
    {
        public BUS_BANGCHAMCONGTHUVIEC busChamCongThuViec = new BUS_BANGCHAMCONGTHUVIEC();
        public DTO_BANGCHAMCONGTHUVIEC suaChamCongThuViec = new DTO_BANGCHAMCONGTHUVIEC();
        public BUS_HOSOTHUVIEC busHoSoThuViec = new BUS_HOSOTHUVIEC();
        public bool checkAdd;
        public ChamCongThuViec(bool CheckAdd)
        {
            InitializeComponent();
            checkAdd = CheckAdd;
            LoadData();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void maNVCbx_Loaded(object sender, RoutedEventArgs e)
        {
            if (checkAdd)
                return;
            maNVCbx.Text = suaChamCongThuViec.Manvtv.ToString();
            thangCbx.Text = suaChamCongThuViec.Thang.ToString();
            namTbx.Text = suaChamCongThuViec.Nam.ToString();
            luongTVTbx.Text = suaChamCongThuViec.Luongtv.ToString();
            soNgayCongTbx.Text = suaChamCongThuViec.Songaycong.ToString();
            soNgayNghiTbx.Text = suaChamCongThuViec.Songaynghi.ToString();
            soGioLamThemTbx.Text = suaChamCongThuViec.Sogiolamthem.ToString();
            ghiChuTbx.Text = suaChamCongThuViec.Ghichu.ToString();
        }

        private void themSuaBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckData())
                    return;

                DTO_BANGCHAMCONGTHUVIEC dtoChamCongThuViec = new DTO_BANGCHAMCONGTHUVIEC();

                dtoChamCongThuViec.Manvtv = int.Parse(maNVCbx.Text);
                dtoChamCongThuViec.Thang = int.Parse(thangCbx.Text);
                dtoChamCongThuViec.Nam = int.Parse(namTbx.Text);
                dtoChamCongThuViec.Luongtv = double.Parse(luongTVTbx.Text);
                dtoChamCongThuViec.Songaycong = int.Parse(soNgayCongTbx.Text);
                dtoChamCongThuViec.Songaynghi = int.Parse(soNgayNghiTbx.Text);
                dtoChamCongThuViec.Sogiolamthem = int.Parse(soGioLamThemTbx.Text);
                dtoChamCongThuViec.Ghichu = ghiChuTbx.Text;

                if (maNVCbx.IsEnabled == true)
                {
                    busChamCongThuViec.ThemBangChamCongThuViec(dtoChamCongThuViec);
                    bool? Result = new MessageBoxCustom("Thêm bảng chấm công thử việc thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                else
                {
                    busChamCongThuViec.SuaBangChamCongThuViec(dtoChamCongThuViec);
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
            if (maNVCbx.Text == String.Empty || thangCbx.Text == String.Empty
                || namTbx.Text == String.Empty || luongTVTbx.Text == String.Empty
                || soNgayCongTbx.Text == String.Empty || soNgayNghiTbx.Text == String.Empty
                || soGioLamThemTbx.Text == String.Empty)
            {
                bool? Result = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
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
                maNVCbx.IsEnabled = false;
                thangCbx.IsEnabled = false;
                namTbx.IsEnabled = false;
            }

            foreach (string maNV in busHoSoThuViec.TongHopMaNhanVien())
            {
                maNVCbx.Items.Add(maNV);
            }

            for (int i = 1; i <= 12; i++)
            {
                thangCbx.Items.Add(i.ToString());
            }
        }

        private void numberTextBoxes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void huyBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
