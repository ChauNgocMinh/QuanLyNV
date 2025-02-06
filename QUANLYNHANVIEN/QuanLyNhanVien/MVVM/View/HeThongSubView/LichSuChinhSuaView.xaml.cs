using BUS;
using DTO;
using QuanLyNhanVien.MessageBox;
using QuanLyNhanVien.WindowView;
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

namespace QuanLyNhanVien.MVVM.View.HeThongSubView
{
    /// <summary>
    /// Interaction logic for LichSuChinhSuaView.xaml
    /// </summary>
    public partial class LichSuChinhSuaView : UserControl
    {
        public BUS_LSCHINHSUA busLSChinhSua = new BUS_LSCHINHSUA();
        public BUS_PHONGBAN busPhongBan = new BUS_PHONGBAN();
        public BUS_BOPHAN busBoPhan = new BUS_BOPHAN();
        public DTO_LSCHINHSUA dtoLSChinhSua = new DTO_LSCHINHSUA();

        public LichSuChinhSuaView()
        {
            InitializeComponent();
            DataGridLoad();
            ComboBoxesLoad();
        }

        private void dsNhanVienDtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsChinhSuaDtg.SelectedItems.Count == 0) return;
            DataRowView row = lsChinhSuaDtg.SelectedItem as DataRowView;

            if (row == null)
            {
                ClearBoxes();
                return;
            }

            dtoLSChinhSua.Macs = int.Parse(row[0].ToString());
            dtoLSChinhSua.Lancs = int.Parse(row[2].ToString());
            dtoLSChinhSua.Hoten = row[1].ToString();
            boPhanCbx.SelectedItem = busBoPhan.TimKiemTheoMaBoPhan(busPhongBan.TimKiemBoPhanTheoPhong(row[3].ToString()));
            phongCbx.SelectedItem = busPhongBan.TimKiemTenPhongBanTheoMa(row[3].ToString());
            maNVTbx.Text = row[1].ToString();
        }

        private void boPhanCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (boPhanCbx.SelectedItem == null)
            {
                DataGridLoad();
                return;
            }

            phongCbx.Items.Clear();

            foreach (var tenPhong in busPhongBan.TongHopPhongBan(busBoPhan.TimKiemTheoTenBoPhan(boPhanCbx.SelectedItem.ToString())))
            {
                phongCbx.Items.Add(tenPhong);
            }
        }

        private void locBtn_Click(object sender, RoutedEventArgs e)
        {
            LocNhanVien();
        }

        private void lamMoiBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            DataGridLoad();
        }

        private void xoaBtn_Click(object sender, RoutedEventArgs e)
        {
            busLSChinhSua.XoaLSChinhSua(dtoLSChinhSua.Macs);
            DataGridLoad();
            bool? Result = new MessageBoxCustom("Xóa lịch sử chỉnh sửa lần " + dtoLSChinhSua.Lancs.ToString() + " của nhân viên có mã nhân viên " + dtoLSChinhSua.Hoten + " thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            ClearBoxes();
        }

        private void tenNVTbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LocNhanVien();
            }
        }

        public void DataGridLoad()
        {
            lsChinhSuaDtg.DataContext = busLSChinhSua.getLSChinhSua();
        }

        public void ComboBoxesLoad()
        {
            boPhanCbx.Text = "";
            phongCbx.Text = "";
            foreach (var tenPhong in busPhongBan.TongHopPhongBan(""))
            {
                phongCbx.Items.Add(tenPhong);
            }

            foreach (var tenBoPhan in busBoPhan.TongHopTenBoPhan())
            {
                boPhanCbx.Items.Add(tenBoPhan);
            }
        }

        public void ClearBoxes()
        {
            phongCbx.Text = "";
            boPhanCbx.Text = "";
            maNVTbx.Text = "";
        }

        public void LocNhanVien()
        {
            if (phongCbx.Text == string.Empty && maNVTbx.Text == string.Empty)
            {
                DataGridLoad();
                return;
            }

            if (phongCbx.Text != string.Empty && maNVTbx.Text == string.Empty)
            {
                lsChinhSuaDtg.DataContext = busLSChinhSua.TongHopLSChinhSuaNhanVienTheoPhong(busPhongBan.TimKiemMaPhongBan(phongCbx.SelectedItem.ToString()), "");
            }

            if (phongCbx.Text == string.Empty && maNVTbx.Text != string.Empty)
            {
                lsChinhSuaDtg.DataContext = busLSChinhSua.TongHopLSChinhSuaNhanVienTheoPhong("", maNVTbx.Text);
            }

            if (phongCbx.Text != string.Empty && maNVTbx.Text != string.Empty)
            {
                lsChinhSuaDtg.DataContext = busLSChinhSua.TongHopLSChinhSuaNhanVienTheoPhong(busPhongBan.TimKiemMaPhongBan(phongCbx.SelectedItem.ToString()), maNVTbx.Text);
            }
        }

        private void lsChinhSuaDtg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DTO_LSCHINHSUA ctChinhSua = new DTO_LSCHINHSUA();
            DataRowView row = lsChinhSuaDtg.SelectedItem as DataRowView;
            ChiTietChinhSua chiTietChinhSua = new ChiTietChinhSua();

            ctChinhSua.Manv = int.Parse(row[1].ToString());
            ctChinhSua.Lancs = int.Parse(row[2].ToString());
            ctChinhSua.Maphong = row[3].ToString();
            ctChinhSua.Maluong = row[4].ToString();
            ctChinhSua.Hoten = row[5].ToString();
            ctChinhSua.Ngaysinh = DateTime.Parse(row[6].ToString());
            ctChinhSua.Gioitinh = row[7].ToString();
            ctChinhSua.Dantoc = row[8].ToString();
            ctChinhSua.Cmnd_cccd = row[9].ToString();
            ctChinhSua.Noicap = row[10].ToString();
            ctChinhSua.Chucvu = row[11].ToString();
            ctChinhSua.Maloainv = row[12].ToString();
            ctChinhSua.Loaihd = row[13].ToString();
            ctChinhSua.Thoigian = int.Parse(row[14].ToString());
            ctChinhSua.Ngaydangki = DateTime.Parse(row[15].ToString());
            ctChinhSua.Ngayhethan = DateTime.Parse(row[16].ToString());
            ctChinhSua.Sdt = row[17].ToString();
            ctChinhSua.Hocvan = row[18].ToString();
            ctChinhSua.Ghichu = row[19].ToString();
            ctChinhSua.Ngaychinhsua = DateTime.Parse(row[20].ToString());

            chiTietChinhSua.ctChinhSua = ctChinhSua;
            chiTietChinhSua.ShowDialog();
            DataGridLoad();
        }
    }
}
