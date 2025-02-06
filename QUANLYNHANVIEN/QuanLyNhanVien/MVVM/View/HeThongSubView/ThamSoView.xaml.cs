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

namespace QuanLyNhanVien.MVVM.View.HeThongSubView
{
    /// <summary>
    /// Interaction logic for ThamSoView.xaml
    /// </summary>
    public partial class ThamSoView : UserControl
    {
        BUS_THAMSO bUS_THAMSO = new BUS_THAMSO();
        
        public ThamSoView()
        {
            InitializeComponent();
            DataGridLoad();
        }

        public void DataGridLoad()
        {
            thamSoDtg.DataContext = bUS_THAMSO.getThamSo();
        }

        private void thamSoDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (thamSoDtg.SelectedItems.Count == 0) return;
            DataRowView row = thamSoDtg.SelectedItem as DataRowView;

            if (row == null)
            {
                ClearBoxes();
                return;
            }
            maTSTbx.Text = row[0].ToString();
            tenTSTbx.Text = row[1].ToString();
            giaTriTbx.Text = row[2].ToString();
        }

        public void ClearBoxes()
        {
            maTSTbx.Text = tenTSTbx.Text = giaTriTbx.Text = "";
        }

        private void suaBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool? show;
                if (maTSTbx.Text == "")
                {
                    show = new MessageBoxCustom("Vui lòng chọn tham số muốn sửa!", MessageType.Error, MessageButtons.Ok).ShowDialog();
                    return;
                }
                DTO_THAMSO dTO_THAMSO = new DTO_THAMSO();
                dTO_THAMSO.Mats = maTSTbx.Text;
                dTO_THAMSO.Tents = tenTSTbx.Text;
                dTO_THAMSO.Giatri = Convert.ToDouble(giaTriTbx.Text);
                bUS_THAMSO.SuaThamSo(dTO_THAMSO);
                show = new MessageBoxCustom("Sửa thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                DataGridLoad();
                ClearBoxes();
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu!\nVui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }

        }

        private void giaTriTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
