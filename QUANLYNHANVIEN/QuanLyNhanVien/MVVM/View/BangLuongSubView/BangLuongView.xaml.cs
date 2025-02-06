using BUS;
using DTO;
using QuanLyNhanVien.MessageBox;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyNhanVien.MVVM.View.BangLuongSubView
{
    /// <summary>
    /// Interaction logic for BangLuongView.xaml
    /// </summary>
    public partial class BangLuongView : UserControl
    {
        BUS_BANGLUONG busBangLuong = new BUS_BANGLUONG();
        DTO_BANGLUONG dtoBangLuong = new DTO_BANGLUONG();

        public BangLuongView()
        {
            InitializeComponent();
            DataGridLoad();
        }

        private void luongDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (luongDtg.SelectedItems.Count == 0) return;
            DataRowView row = luongDtg.SelectedItem as DataRowView;

            if (row == null)
            {
                ClearBoxes();
                return;
            }

            dtoBangLuong.Maluong = maLuongTbx.Text = row[0].ToString();
            luongCBTbx.Text = row[1].ToString();
            phuCapTbx.Text = row[2].ToString();
            phuCapKhacTbx.Text = row[3].ToString();
            ghiChuTbx.Text = row[4].ToString(); 
        }

        private void lamMoiBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
        }

        private void themBtn_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                if (maLuongTbx.Text == String.Empty || luongCBTbx.Text == String.Empty || phuCapTbx.Text == String.Empty || phuCapKhacTbx.Text == String.Empty)
                {
                    bool? result = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }
                bool checkExist = false;
                List<string> list = busBangLuong.TongHopMaLuong();

                foreach (string s in list)
                {
                    if (s == maLuongTbx.Text)
                    {
                        checkExist = true;
                        break;
                    }
                }

                if (!checkExist)
                {
                    dtoBangLuong.Maluong = maLuongTbx.Text;
                    dtoBangLuong.Lcb = double.Parse(luongCBTbx.Text);
                    dtoBangLuong.Phucapchucvu = double.Parse(phuCapTbx.Text);
                    dtoBangLuong.Phucapkhac = double.Parse(phuCapKhacTbx.Text);
                    dtoBangLuong.Ghichu = ghiChuTbx.Text;
                    busBangLuong.ThemBangLuong(dtoBangLuong);
                    bool? result = new MessageBoxCustom("Thêm lương thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    DataGridLoad();
                    ClearBoxes();
                }
                else
                {
                    bool? result = new MessageBoxCustom("Mã lương đã tồn tại!", MessageType.Error, MessageButtons.Ok).ShowDialog();
                }
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu!\nVui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }

        }

        private void xoaBtn_Click(object sender, RoutedEventArgs e)
        {
            bool? result;
            if (luongDtg.SelectedItems.Count == 0)
            {
                result = new MessageBoxCustom("Vui lòng chọn lương cần xóa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            result = new MessageBoxCustom("Bạn có chắc chắn muốn xóa không?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (!result.Value)
                return;

            busBangLuong.XoaBangLuong(dtoBangLuong.Maluong);
            DataGridLoad();
            result = new MessageBoxCustom("Xóa lương thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            ClearBoxes();
        }

        private void suaBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (maLuongTbx.Text == String.Empty || luongCBTbx.Text == String.Empty || double.Parse(luongCBTbx.Text) <= 0 || phuCapTbx.Text == String.Empty || double.Parse(phuCapTbx.Text) <= 0 || phuCapKhacTbx.Text == String.Empty || double.Parse(phuCapKhacTbx.Text) <= 0)
                {
                    bool? result = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }

                bool checkExist = false;
                List<string> list = busBangLuong.TongHopMaLuong();

                foreach (string s in list)
                {
                    if (s == maLuongTbx.Text)
                    {
                        checkExist = true;
                        break;
                    }
                }

                if (checkExist)
                {
                    dtoBangLuong.Maluong = maLuongTbx.Text;
                    dtoBangLuong.Lcb = double.Parse(luongCBTbx.Text);
                    dtoBangLuong.Phucapchucvu = double.Parse(phuCapTbx.Text);
                    dtoBangLuong.Phucapkhac = double.Parse(phuCapKhacTbx.Text);
                    dtoBangLuong.Ghichu = ghiChuTbx.Text;
                    busBangLuong.SuaBangLuong(dtoBangLuong);
                    bool? result = new MessageBoxCustom("Sửa lương thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    DataGridLoad();
                    ClearBoxes();
                }
                else
                {
                    bool? result = new MessageBoxCustom("Mã lương không tồn tại!", MessageType.Error, MessageButtons.Ok).ShowDialog();
                }
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu!\nVui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        private void moneyTextBoxes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        public void ClearBoxes()
        {
            //maBoPhanCbx.SelectedIndex = -1;
            maLuongTbx.Text = "";
            luongCBTbx.Text = "";
            phuCapTbx.Text = "";
            phuCapKhacTbx.Text = "";
            ghiChuTbx.Text = "";
        }

        public void DataGridLoad()
        {
            luongDtg.DataContext = busBangLuong.getBangLuong();
        }   
    }
}
