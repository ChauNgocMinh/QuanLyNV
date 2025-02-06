using System;
using System.Collections.Generic;
using System.Data;
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
using BUS;
using DTO;
using QuanLyNhanVien.MessageBox;

namespace QuanLyNhanVien.MVVM.View.ChamCongSubView
{
    /// <summary>
    /// Interaction logic for KhenThuongKyLuatView.xaml
    /// </summary>
    public partial class KhenThuongKyLuatView : UserControl
    {
        public BUS_KHENTHUONG busKhenThuong = new BUS_KHENTHUONG();
        public DTO_KHENTHUONG dtoKhenThuong = new DTO_KHENTHUONG();
        public BUS_KYLUAT busKyLuat = new BUS_KYLUAT();
        public DTO_KYLUAT dtoKyLuat = new DTO_KYLUAT();

        public KhenThuongKyLuatView()
        {
            InitializeComponent();
            DataGridLoad();
            ClearBoxesKT();
            ClearBoxesKL();
        }

        private void moiKTbtn_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxesKT();
            DataGridLoad();
        }

        private void themKTbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tienKTTbx.Text == String.Empty || lyDoKTTbx.Text == String.Empty)
                {
                    bool? result = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }
                bool flat = true;
                List<string> list = busKhenThuong.TongHopMaKhenThuong();
                foreach (string s in list)
                {
                    if (s == maKTTbx.Text)
                    {
                        flat = false;
                        break;
                    }
                }
                if (flat)
                {
                    string a = list[list.Count - 1];
                    DTO_KHENTHUONG dtoKhenThuong1 = new DTO_KHENTHUONG();
                    dtoKhenThuong1.Makt = int.Parse(a) + 1;
                    dtoKhenThuong1.Tien = int.Parse(tienKTTbx.Text);
                    dtoKhenThuong1.Lydo = lyDoKTTbx.Text;
                    busKhenThuong.ThemKhenThuong(dtoKhenThuong1);
                    bool? result = new MessageBoxCustom("Thêm khen thưởng thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    DataGridLoad();
                    ClearBoxesKT();
                }
                else
                {
                    bool? result = new MessageBoxCustom("Mã khen thưởng đã tồn tại!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                }
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu!\nVui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }

        }

        private void xoaKTbtn_Click(object sender, RoutedEventArgs e)
        {
            bool? result;
            if (dsKhenThuongDtg.SelectedItems.Count == 0)
            {
                result = new MessageBoxCustom("Vui lòng chọn khen thưởng cần xóa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            result = new MessageBoxCustom("Bạn có chắc chắn muốn xóa không?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (!result.Value)
                return;

            busKhenThuong.XoaKhenThuong(dtoKhenThuong.Makt);
            DataGridLoad();
            result = new MessageBoxCustom("Xóa khen thưởng thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            ClearBoxesKT();
        }

        private void suaKTbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (maKTTbx.Text == String.Empty || tienKTTbx.Text == String.Empty || lyDoKTTbx.Text == String.Empty)
                {
                    bool? show = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }
                bool flat2 = true;
                List<string> list = busKhenThuong.TongHopMaKhenThuong();
                foreach (string s in list)
                {
                    if (s == maKTTbx.Text)
                    {
                        flat2 = false;
                        break;
                    }
                }
                if (!flat2)
                {
                    DTO_KHENTHUONG dtoKhenThuong2 = new DTO_KHENTHUONG();
                    dtoKhenThuong2.Makt = int.Parse(maKTTbx.Text);
                    dtoKhenThuong2.Tien = double.Parse(tienKTTbx.Text);
                    dtoKhenThuong2.Lydo = lyDoKTTbx.Text;
                    busKhenThuong.SuaKhenThuong(dtoKhenThuong2);
                    bool? show = new MessageBoxCustom("Sửa khen thưởng thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    DataGridLoad();
                    ClearBoxesKT();
                }
                else
                {
                    bool? show = new MessageBoxCustom("Khen thưởng không tồn tại!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                }
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu!\nVui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }

        }

        private void dsKhenThuongdtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dsKhenThuongDtg.SelectedItems.Count == 0) return;
            DataRowView row = dsKhenThuongDtg.SelectedItem as DataRowView;

            if (row == null)
            {
                ClearBoxesKT();
                return;
            }

            dtoKhenThuong.Makt = int.Parse(row[0].ToString());
            maKTTbx.Text = dtoKhenThuong.Makt.ToString();
            tienKTTbx.Text = row[1].ToString();
            lyDoKTTbx.Text = row[2].ToString();
        }
        public void DataGridLoad()
        {
            dsKhenThuongDtg.DataContext = busKhenThuong.getKhenThuong();
            dsKyLuatDtg.DataContext = busKyLuat.getKyLuat();
        }
        public void ClearBoxesKT()
        {
            maKTTbx.Text = "";
            tienKTTbx.Text = "";
            lyDoKTTbx.Text = "";
        }

        public void ClearBoxesKL()
        {
            maKLTbx.Text = "";
            tienKLTbx.Text = "";
            lyDoKLTbx.Text = "";
        }
        private void moiKLBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxesKL();
            DataGridLoad();
        }

        private void themKLBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tienKLTbx.Text == String.Empty || lyDoKLTbx.Text == String.Empty)
                {
                    bool? result = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }
                bool flat = true;
                List<string> list = busKyLuat.TongHopMaKyLuat();
                foreach (string s in list)
                {
                    if (s == maKLTbx.Text)
                    {
                        flat = false;
                        break;
                    }
                }
                if (flat)
                {
                    string a = list[list.Count - 1];
                    DTO_KYLUAT dtoKyLuat1 = new DTO_KYLUAT();
                    dtoKyLuat1.Makl = int.Parse(a) + 1;
                    dtoKyLuat1.Tien = int.Parse(tienKLTbx.Text);
                    dtoKyLuat1.Lydo = lyDoKLTbx.Text;
                    busKyLuat.ThemKyLuat(dtoKyLuat1);
                    bool? result = new MessageBoxCustom("Thêm kỷ luật thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    DataGridLoad();
                    ClearBoxesKL();
                }
                else
                {
                    bool? result = new MessageBoxCustom("Mã kỷ luật đã tồn tại!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                }
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu!\nVui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }

        }

        private void xoaKLBtn_Click(object sender, RoutedEventArgs e)
        {
            bool? result;
            if (dsKyLuatDtg.SelectedItems.Count == 0)
            {
                result = new MessageBoxCustom("Vui lòng chọn kỷ luật cần xóa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            result = new MessageBoxCustom("Bạn có chắc chắn muốn xóa không?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (!result.Value)
                return;

            busKyLuat.XoaKyLuat(dtoKyLuat.Makl);
            DataGridLoad();
            result = new MessageBoxCustom("Xóa kỷ luật thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            ClearBoxesKT();
        }

        private void suaKLBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (maKLTbx.Text == String.Empty || tienKLTbx.Text == String.Empty || lyDoKLTbx.Text == String.Empty)
                {
                    bool? show = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }
                bool flat2 = true;
                List<string> list = busKyLuat.TongHopMaKyLuat();
                foreach (string s in list)
                {
                    if (s == maKLTbx.Text)
                    {
                        flat2 = false;
                        break;
                    }
                }
                if (!flat2)
                {
                    DTO_KYLUAT dtoKyLuat2 = new DTO_KYLUAT();
                    dtoKyLuat2.Makl = int.Parse(maKLTbx.Text);
                    dtoKyLuat2.Tien = double.Parse(tienKLTbx.Text);
                    dtoKyLuat2.Lydo = lyDoKLTbx.Text;
                    busKyLuat.SuaKyLuat(dtoKyLuat2);
                    bool? show = new MessageBoxCustom("Sửa kỷ luật thành công", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    DataGridLoad();
                    ClearBoxesKT();
                }
                else
                {
                    bool? show = new MessageBoxCustom("Kỷ luật không tồn tại!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                }
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu!\nVui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }

        }

        private void dsKyLuatDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dsKyLuatDtg.SelectedItems.Count == 0) return;
            DataRowView row = dsKyLuatDtg.SelectedItem as DataRowView;

            if (row == null)
            {
                ClearBoxesKL();
                return;
            }

            dtoKyLuat.Makl = int.Parse(row[0].ToString());
            maKLTbx.Text = dtoKyLuat.Makl.ToString();
            tienKLTbx.Text = row[1].ToString();
            lyDoKLTbx.Text = row[2].ToString();
        }
    }
}
