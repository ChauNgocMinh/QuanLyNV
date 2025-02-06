using System;
using BUS;
using DTO;
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
using System.Data;
using QuanLyNhanVien.MessageBox;

namespace QuanLyNhanVien.MVVM.View.PhongBanSubVew
{
    /// <summary>
    /// Interaction logic for BoPhanView.xaml
    /// </summary>
    public partial class BoPhanView : UserControl
    {
        public BUS_PHONGBAN busPhongBan = new BUS_PHONGBAN();
        public BUS_BOPHAN busBoPhan = new BUS_BOPHAN();
        public DTO_BOPHAN dtoBoPhan = new DTO_BOPHAN();
        public BoPhanView()
        {
            InitializeComponent();
            DataGridLoad();
            ClearBoxes();
        }

        private void dsBoPhanDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dsBoPhanDtg.SelectedItems.Count == 0) return;
            DataRowView row = dsBoPhanDtg.SelectedItem as DataRowView;

            if (row == null)
            {
                ClearBoxes();
                return;
            }

            dtoBoPhan.Mabp = row[0].ToString();
            mabpTbx.Text = dtoBoPhan.Mabp;
            tenbpTbx.Text = row[1].ToString();
            ngaytlDpk.Text = row[2].ToString();

        }
        public void DataGridLoad()
        {
            dsBoPhanDtg.DataContext = busBoPhan.getBoPhan();
        }

        private void themBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mabpTbx.Text == String.Empty || tenbpTbx.Text == String.Empty || ngaytlDpk.Text == String.Empty)
                {
                    bool? result = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }
                bool flat = true;
                List<string> list = busBoPhan.TongHopMaBoPhan();
                foreach (string s in list)
                {
                    if (s == mabpTbx.Text)
                    {
                        flat = false;
                        break;
                    }
                }
                if (flat)
                {
                    DTO_BOPHAN dtoBoPhan1 = new DTO_BOPHAN();
                    dtoBoPhan1.Mabp = mabpTbx.Text;
                    dtoBoPhan1.Tenbophan = tenbpTbx.Text;
                    dtoBoPhan1.Ngaythanhlap = DateTime.Parse(ngaytlDpk.Text);
                    busBoPhan.ThemBoPhan(dtoBoPhan1);
                    bool? result = new MessageBoxCustom("Thêm bộ phận thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    DataGridLoad();
                    ClearBoxes();
                }
                else
                {
                    bool? result = new MessageBoxCustom("Mã bộ phận đã tồn tại!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
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
            if (dsBoPhanDtg.SelectedItems.Count == 0)
            {
                result = new MessageBoxCustom("Vui lòng chọn bộ phận cần xóa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            result = new MessageBoxCustom("Bạn có chắc chắn muốn xóa không?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (!result.Value)
                return;

            busBoPhan.XoaBoPhan(dtoBoPhan.Mabp);
            DataGridLoad();
            result = new MessageBoxCustom("Xóa bộ phận thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            ClearBoxes();
        }

        private void suaBtn_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                if (mabpTbx.Text == String.Empty || tenbpTbx.Text == String.Empty || ngaytlDpk.Text == String.Empty)
                {
                    bool? show = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }
                bool flat2 = true;
                List<string> list = busBoPhan.TongHopMaBoPhan();
                foreach (string s in list)
                {
                    if (s == mabpTbx.Text)
                    {
                        flat2 = false;
                        break;
                    }
                }
                if (!flat2)
                {
                    DTO_BOPHAN dtoBoPhan2 = new DTO_BOPHAN();
                    dtoBoPhan2.Mabp = mabpTbx.Text;
                    dtoBoPhan2.Tenbophan = tenbpTbx.Text;
                    dtoBoPhan2.Ngaythanhlap = DateTime.Parse(ngaytlDpk.Text);
                    busBoPhan.SuaBoPhan(dtoBoPhan2);
                    bool? show = new MessageBoxCustom("Sửa bộ phận thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    DataGridLoad();
                    ClearBoxes();
                }
                else
                {
                    bool? show = new MessageBoxCustom("Bộ phận không tồn tại!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                }
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu!\nVui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        public void ClearBoxes()
        {
            mabpTbx.Text = "";
            tenbpTbx.Text = "";
            ngaytlDpk.Text = "";
        }
        private void lamMoiBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            DataGridLoad();
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
