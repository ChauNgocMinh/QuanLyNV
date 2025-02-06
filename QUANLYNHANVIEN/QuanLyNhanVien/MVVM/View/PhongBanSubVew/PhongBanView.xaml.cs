using System;
using BUS;
using DTO;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanLyNhanVien.MessageBox;

namespace QuanLyNhanVien.MVVM.View.PhongBanSubVew
{
    /// <summary>
    /// Interaction logic for PhongBanView.xaml
    /// </summary>
    public partial class PhongBanView : UserControl
    {
        public BUS_PHONGBAN busPhongBan = new BUS_PHONGBAN();
        public DTO_PHONGBAN dtoPhongBan = new DTO_PHONGBAN();
        public BUS_BOPHAN busBoPhan = new BUS_BOPHAN();
        public DTO_BOPHAN dtoBoPhan = new DTO_BOPHAN();
        public PhongBanView()
        {
            InitializeComponent();
            DataGridLoad();
            ClearBoxes();
            ComboBoxes_Loaded();
        }

        private void dsPhongBanDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dsPhongBanDtg.SelectedItems.Count == 0) return;
            DataRowView row = dsPhongBanDtg.SelectedItem as DataRowView;

            if (row == null)
            {
                ClearBoxes();
                return;
            }

            dtoPhongBan.Maphong = row[0].ToString();
            List<string> list = busBoPhan.TongHopMaBoPhan();
            int i;
            for (i = 0; i < list.Count; i++)
            {
                if (list[i].ToString() == row[1].ToString())
                    break;
            }
            maBoPhanCbx.SelectedIndex = i;
            maPhongBanTbx.Text = row[0].ToString();
            tenPhongBanTbx.Text = row[2].ToString();
            ngaytlDpk.Text = row[3].ToString();

        }

        private void themBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (maBoPhanCbx.Text == String.Empty || tenPhongBanTbx.Text == String.Empty || ngaytlDpk.Text == String.Empty || maPhongBanTbx.Text == String.Empty)
                {
                    bool? result = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }
                bool flat = true;
                List<string> list = busPhongBan.TongHopMaPhongBan();
                foreach (string s in list)
                {
                    if (s == maPhongBanTbx.Text)
                    {
                        flat = false;
                        break;
                    }
                }
                if (flat)
                {
                    DTO_PHONGBAN dtoPhongBan1 = new DTO_PHONGBAN();
                    dtoPhongBan1.Mabp = maBoPhanCbx.SelectedValue.ToString();
                    dtoPhongBan1.Maphong = maPhongBanTbx.Text;
                    dtoPhongBan1.Tenphong = tenPhongBanTbx.Text;
                    dtoPhongBan1.Ngaythanhlap = DateTime.Parse(ngaytlDpk.Text);
                    busPhongBan.ThemPhongBan(dtoPhongBan1);
                    bool? result = new MessageBoxCustom("Thêm phòng ban thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    DataGridLoad();
                    ClearBoxes();
                }
                else
                {
                    bool? result = new MessageBoxCustom("Mã phòng ban đã tồn tại!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
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
            if (dsPhongBanDtg.SelectedItems.Count == 0)
            {
                result = new MessageBoxCustom("Vui lòng chọn phòng ban cần xóa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            result = new MessageBoxCustom("Bạn có chắc chắn muốn xóa không?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (!result.Value)
                return;

            busPhongBan.XoaPhongBan(dtoPhongBan.Maphong);
            DataGridLoad();
            result = new MessageBoxCustom("Xóa phòng ban thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            ClearBoxes();
        }

        private void suaBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (maBoPhanCbx.SelectedIndex == -1 || tenPhongBanTbx.Text == String.Empty || ngaytlDpk.Text == String.Empty || maPhongBanTbx.Text == String.Empty)
                {
                    bool? result = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();

                    return;
                }
                bool flat2 = true;
                List<string> list = busPhongBan.TongHopMaPhongBan();
                foreach (string s in list)
                {
                    if (s == maPhongBanTbx.Text)
                    {
                        flat2 = false;
                        break;
                    }
                }
                if (!flat2)
                {
                    DTO_PHONGBAN dtoPhongBan2 = new DTO_PHONGBAN();
                    dtoPhongBan2.Maphong = maPhongBanTbx.Text;
                    dtoPhongBan2.Mabp = maBoPhanCbx.SelectedValue.ToString();
                    dtoPhongBan2.Tenphong = tenPhongBanTbx.Text;
                    dtoPhongBan2.Ngaythanhlap = DateTime.Parse(ngaytlDpk.Text);
                    busPhongBan.SuaPhongBan(dtoPhongBan2);
                    bool? result = new MessageBoxCustom("Sửa phòng ban thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    DataGridLoad();
                    ClearBoxes();
                }
                else
                {
                    bool? result = new MessageBoxCustom("Phòng ban không tồn tại!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                }
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu!\nVui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            } 
        }

        private void lammoiBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            DataGridLoad();
        }
        public void ClearBoxes()
        {
            maBoPhanCbx.SelectedIndex = -1;
            maPhongBanTbx.Text = "";
            tenPhongBanTbx.Text = "";
            ngaytlDpk.Text = "";
        }
        public void DataGridLoad()
        {
            dsPhongBanDtg.DataContext = busPhongBan.getPhongBan();
        }

        public void ComboBoxes_Loaded()
        {
            foreach (var maBoPhan in busBoPhan.TongHopMaBoPhan())
            {
                maBoPhanCbx.Items.Add(maBoPhan);
            }

        }

        private void ngaytlDpk_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ngaytlDpk.SelectedDate > DateTime.Now)
            {
                bool? show = new MessageBoxCustom("Không thể chọn ngày thành lập trong tương lai!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }
    }
}
