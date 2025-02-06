using QuanLyNhanVien.WindowView;
using BUS;
using DTO;
using System;
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

namespace QuanLyNhanVien.MVVM.View.SubView
{
    /// <summary>
    /// Interaction logic for BaoHiemNhanVien.xaml
    /// </summary>
    public partial class BaoHiemNhanVienView : UserControl
    {
        public BUS_SOTHAISAN busSoThaiSan = new BUS_SOTHAISAN();
        public DTO_SOTHAISAN dtoSoThaiSan = new DTO_SOTHAISAN();
        public BUS_SOBH busSoBH = new BUS_SOBH();
        public DTO_SOBH dtoSoBH = new DTO_SOBH();
        public BaoHiemNhanVienView()
        {
            InitializeComponent();
            DataGridLoad();
        }

        private void btnThemThaiSan_Click(object sender, RoutedEventArgs e)
        {
            ThemThaiSan thaiSan = new ThemThaiSan(true);
            thaiSan.ShowDialog();
            DataGridLoad();
        }

        private void dsThaiSanDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void DataGridLoad()
        {
            dsThaiSanDtg.DataContext = busSoThaiSan.getSoThaiSan();
            dtgBaoHiem.DataContext = busSoBH.getSoBH();
        }

        private void btnXoaThaiSan_Click(object sender, RoutedEventArgs e)
        {
            if (dsThaiSanDtg.SelectedItems.Count == 0)
            {
                bool? result = new MessageBoxCustom("Vui lòng chọn thai sản cần xóa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }
                
            busSoThaiSan.XoaSoThaiSan(dtoSoThaiSan.Mats);
            DataGridLoad();
            bool? Result = new MessageBoxCustom("Xóa thai sản thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();

        }

        private void btnSuaThaiSan_Click(object sender, RoutedEventArgs e)
        {
            if (dsThaiSanDtg.SelectedItems.Count == 0)
            {
                bool? result = new MessageBoxCustom("Vui lòng chọn thai sản cần sửa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            DTO_SOTHAISAN suaSoThaiSan = new DTO_SOTHAISAN();
            DataRowView row = dsThaiSanDtg.SelectedItem as DataRowView;
            ThemThaiSan themThaiSan = new ThemThaiSan(false);

            suaSoThaiSan.Mats = int.Parse(row[0].ToString());
            suaSoThaiSan.Manv = int.Parse(row[1].ToString());
            suaSoThaiSan.Ngayvesom = DateTime.Parse(row[2].ToString());
            suaSoThaiSan.Ngaynghisinh = DateTime.Parse(row[3].ToString());
            suaSoThaiSan.Ngaylamtrolai = DateTime.Parse(row[4].ToString());
            suaSoThaiSan.Trocapcty = double.Parse(row[5].ToString());
            suaSoThaiSan.Ghichu = row[6].ToString();

            themThaiSan.suaThaiSan = suaSoThaiSan;
            themThaiSan.ShowDialog();
            DataGridLoad();
        }

        private void btnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (dsThaiSanDtg.SelectedItems.Count == 0)
            {
                bool? result = new MessageBoxCustom("Vui lòng chọn thai sản cần xem!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            DTO_SOTHAISAN ctSoThaiSan = new DTO_SOTHAISAN();
            DataRowView row = dsThaiSanDtg.SelectedItem as DataRowView;
            ChiTietThaiSan ctThaiSan = new ChiTietThaiSan();
            ctThaiSan.checkAdd = false;

            ctSoThaiSan.Mats = int.Parse(row[0].ToString());
            ctSoThaiSan.Manv = int.Parse(row[1].ToString());
            ctSoThaiSan.Ngayvesom = DateTime.Parse(row[2].ToString());
            ctSoThaiSan.Ngaynghisinh = DateTime.Parse(row[3].ToString());
            ctSoThaiSan.Ngaylamtrolai = DateTime.Parse(row[4].ToString());
            ctSoThaiSan.Trocapcty = double.Parse(row[5].ToString());
            ctSoThaiSan.Ghichu = row[6].ToString();

            ctThaiSan.ctThaiSan = ctSoThaiSan;
            ctThaiSan.ShowDialog();
            DataGridLoad();
        }

        private void bthXoaBaoHiem_Click(object sender, RoutedEventArgs e)
        {
            if (dtgBaoHiem.SelectedItems.Count == 0)
            {
                bool? result = new MessageBoxCustom("Vui lòng chọn bảo hiểm cần xóa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            busSoBH.XoaSoBH(dtoSoBH.Mabh);
            DataGridLoad();
            bool? Result = new MessageBoxCustom("Xóa bảo hiểm thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
        }

        private void btnThemBaoHiem_Click(object sender, RoutedEventArgs e)
        {
            ThemBaoHiem themBaoHiem = new ThemBaoHiem(true);

            themBaoHiem.ShowDialog();
            DataGridLoad();
        }
        private void btn_SuaBaoHiem_Click(object sender, RoutedEventArgs e)
        {
            if (dtgBaoHiem.SelectedItems.Count == 0)
            {
                bool? result = new MessageBoxCustom("Vui lòng chọn bảo hiểm cần sửa!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            DTO_SOBH suaSoBH = new DTO_SOBH();
            DataRowView row = dtgBaoHiem.SelectedItem as DataRowView;
            ThemBaoHiem themBaoHiem = new ThemBaoHiem(false);

            suaSoBH.Mabh = int.Parse(row[0].ToString());
            suaSoBH.Manv = int.Parse(row[1].ToString());
            suaSoBH.Ngaycapso = DateTime.Parse(row[2].ToString());
            suaSoBH.Noicapso = row[3].ToString();
            suaSoBH.Ghichu = row[4].ToString();

            themBaoHiem.suaBaoHiem = suaSoBH;
            themBaoHiem.ShowDialog();
            DataGridLoad();
        }

        private void btn_XemChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (dtgBaoHiem.SelectedItems.Count == 0)
            {
                bool? result = new MessageBoxCustom("Vui lòng chọn bảo hiểm cần xem!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            DTO_SOBH ctSoBaoHiem = new DTO_SOBH();
            DataRowView row = dtgBaoHiem.SelectedItem as DataRowView;
            ChiTietBaoHiem ctBaoHiem = new ChiTietBaoHiem();
            ctBaoHiem.checkAdd = false;

            ctSoBaoHiem.Mabh = int.Parse(row[0].ToString());
            ctSoBaoHiem.Manv = int.Parse(row[1].ToString());
            ctSoBaoHiem.Ngaycapso = DateTime.Parse(row[2].ToString());
            ctSoBaoHiem.Noicapso =row[3].ToString();
            ctSoBaoHiem.Ghichu = row[4].ToString();

            ctBaoHiem.ctBaoHiem = ctSoBaoHiem;
            ctBaoHiem.ShowDialog();
            DataGridLoad();
        }
    }
}
