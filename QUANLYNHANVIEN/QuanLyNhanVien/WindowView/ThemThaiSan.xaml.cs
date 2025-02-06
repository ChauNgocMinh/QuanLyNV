using System;
using System.Data;
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
using System.Windows.Shapes;
using QuanLyNhanVien.MessageBox;
using System.Text.RegularExpressions;

namespace QuanLyNhanVien.WindowView
{
    /// <summary>
    /// Interaction logic for ThemThaiSan.xaml
    /// </summary>
    public partial class ThemThaiSan : Window
    {
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        public BUS_SOTHAISAN busSoThaiSan = new BUS_SOTHAISAN();
        public BUS_THAMSO busThamSo = new BUS_THAMSO();
        public DTO_SOTHAISAN suaThaiSan;
        public DTO_NHANVIEN suaNhanVien;
        public DTO_LSCHINHSUA dtoLSChinhSua = new DTO_LSCHINHSUA();
        public bool checkAdd;
        public ThemThaiSan(bool CheckAdd)
        {
            InitializeComponent();
            checkAdd = CheckAdd;
            ComboBoxes_Loaded();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void ComboBoxes_Loaded()
        {
            foreach (var maNV in busNhanVien.TongHopMaNhanVienTheoGioiTinh("Nữ"))
            {
                maNVCbx.Items.Add(maNV);
            }

            foreach (var maNV in busNhanVien.TongHopMaNhanVienTheoGioiTinh("Khác"))
            {
                maNVCbx.Items.Add(maNV);
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ngayNghiSinhDpk.Text == String.Empty || ngayVeSomDpk.Text == String.Empty || ngayLamTLDpk.Text == String.Empty
                || troCapTbx.Text == String.Empty)

                {
                    bool? Result = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }
                DTO_SOTHAISAN dtoSoThaiSan = new DTO_SOTHAISAN();
                dtoSoThaiSan.Manv = int.Parse(maNVCbx.SelectedValue.ToString());
                dtoSoThaiSan.Ngaynghisinh = DateTime.Parse(ngayNghiSinhDpk.Text);
                dtoSoThaiSan.Ngayvesom = DateTime.Parse(ngayVeSomDpk.Text);
                dtoSoThaiSan.Ngaylamtrolai = DateTime.Parse(ngayLamTLDpk.Text);
                dtoSoThaiSan.Trocapcty = int.Parse(troCapTbx.Text);
                dtoSoThaiSan.Ghichu = ghiChuTbx.Text;

                if (maTSTbx.Text == string.Empty)
                {
                    busSoThaiSan.ThemSoThaiSan(dtoSoThaiSan);
                    bool? Result = new MessageBoxCustom("Thêm thai sản thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                else
                {
                    dtoSoThaiSan.Mats = int.Parse(maTSTbx.Text);
                    busSoThaiSan.SuaSoThaiSan(dtoSoThaiSan);
                    bool? Result = new MessageBoxCustom("Sửa thai sản thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                this.Close();
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu!\nVui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        private void maTSTbx_Loaded(object sender, RoutedEventArgs e)
        {
            if (checkAdd)
                return;
            maTSTbx.Text = suaThaiSan.Mats.ToString();
            maNVCbx.SelectedItem = suaThaiSan.Manv.ToString();

            ngayNghiSinhDpk.Text = suaThaiSan.Ngaynghisinh.ToString();
            ngayVeSomDpk.Text = suaThaiSan.Ngayvesom.ToString();
            ngayLamTLDpk.Text = suaThaiSan.Ngaylamtrolai.ToString();
            troCapTbx.Text = suaThaiSan.Trocapcty.ToString();
            ghiChuTbx.Text = suaThaiSan.Ghichu.ToString();
        }

        private void ngayNghiSinhDpk_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int soThangNghiTruocVaSauSinh = int.Parse(busThamSo.Get_soThangNghiSinh().ToString()) / 2;
            DTO_NHANVIEN dtoNhanVien = busNhanVien.GetChiTietNhanVienTheoMa(maNVCbx.Text);

            if (checkAdd)
            {
                if (maNVCbx.Text == "")
                {
                    bool? Result = new MessageBoxCustom("Vui lòng chọn nhân viên.", MessageType.Error, MessageButtons.Ok).ShowDialog();
                    return;
                }

                if (ngayNghiSinhDpk.Text == "")
                {
                    return;
                }

                if (ngayNghiSinhDpk.SelectedDate.Value.Date.AddMonths(-soThangNghiTruocVaSauSinh) < dtoNhanVien.Ngaydangki)
                {
                    bool? Result = new MessageBoxCustom("Không thể nghỉ sinh khi chưa vào làm.", MessageType.Error, MessageButtons.Ok).ShowDialog();
                    ClearYearDpk();
                    return;
                }

                if (busSoThaiSan.KiemTraTonTai(maNVCbx.Text))
                {
                    if (ngayNghiSinhDpk.SelectedDate < busSoThaiSan.TimNgayLamTroLai(maNVCbx.Text))
                    {
                        bool? Result = new MessageBoxCustom("Nhân viên chưa kết thúc đợt nghỉ sinh trước.", MessageType.Error, MessageButtons.Ok).ShowDialog();
                        ClearYearDpk();
                        return;
                    }
                }
                checkAdd = true;
            }

            ngayVeSomDpk.SelectedDate = ngayNghiSinhDpk.SelectedDate.Value.Date.AddMonths(-soThangNghiTruocVaSauSinh);
            ngayLamTLDpk.SelectedDate = ngayNghiSinhDpk.SelectedDate.Value.Date.AddMonths(soThangNghiTruocVaSauSinh);

            if (ngayVeSomDpk.SelectedDate.Value.Date.AddMonths(-soThangNghiTruocVaSauSinh) > dtoNhanVien.Ngayhethan)
            {
                bool? Result = new MessageBoxCustom("Không thể hưởng nghỉ sinh sau khi nghỉ việc.", MessageType.Error, MessageButtons.Ok).ShowDialog();

                return;
            }
        }

        private void troCapTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        public void ClearYearDpk()
        {
            ngayVeSomDpk.Text = "";
            ngayNghiSinhDpk.Text = "";
            ngayLamTLDpk.Text = "";
        }
    }
}
