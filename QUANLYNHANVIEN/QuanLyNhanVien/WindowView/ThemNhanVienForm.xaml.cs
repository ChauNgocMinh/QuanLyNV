using System;
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
using DTO;
using BUS;
using System.Data;
using QuanLyNhanVien.MVVM.View.SubView;
using QuanLyNhanVien.MessageBox;
using System.Text.RegularExpressions;

namespace QuanLyNhanVien.WindowView
{
    /// <summary>
    /// Interaction logic for ThemNhanVienForm.xaml
    /// </summary>
    public partial class ThemNhanVienForm : Window
    {
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        public BUS_PHONGBAN busPhongBan = new BUS_PHONGBAN();
        public BUS_LOAINHANVIEN busLoaiNV = new BUS_LOAINHANVIEN();
        public BUS_BANGLUONG busBangLuong = new BUS_BANGLUONG();
        public BUS_LSCHINHSUA busLSChinhSua = new BUS_LSCHINHSUA();
        public DTO_NHANVIEN suaNhanVien;
        public DTO_LSCHINHSUA dtoLSChinhSua = new DTO_LSCHINHSUA();
        public int flag;

        public ThemNhanVienForm(int CheckAdd)
        {
            InitializeComponent();
            ComboBoxes_Loaded();
            flag = CheckAdd;

            if (flag == 1)
            {
                themSuaBtn.Content = "Thêm";
                ngayKyDpk.SelectedDate = DateTime.Now;
            }
            else if (flag == 2)
            {
                themSuaBtn.Content = "Sửa";
                tenTbx.IsEnabled = false;
            }
            else if (flag == 3)
            {
                thoiGianTbx.IsEnabled = false;
                loaiHopDongCbx.IsEnabled = false;
                maLuongCbx.IsEnabled = false;
                chucVuTbx.IsEnabled = false;
                loaiNVCbx.IsEnabled = false;
                phongCbx.IsEnabled = false;
                tenTbx.IsEnabled = false;
                themSuaBtn.Content = "Sửa";
            }
        }

        private void huyBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void themSuaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckData())
                return;

            DTO_NHANVIEN dtoNhanVien = new DTO_NHANVIEN();
            dtoNhanVien.Maphong = busPhongBan.TimKiemMaPhongBan(phongCbx.Text);
            dtoNhanVien.Hoten = tenTbx.Text;
            dtoNhanVien.Ngaysinh = DateTime.Parse(ngaySinhDpk.Text);
            dtoNhanVien.Gioitinh = gioiTinhCbx.Text;
            dtoNhanVien.Cmnd_cccd = cccdTbx.Text;
            dtoNhanVien.Noicap = noiCapTbx.Text;
            dtoNhanVien.Maluong = maLuongCbx.Text;
            dtoNhanVien.Maloainv = busLoaiNV.TimKiemTheoLoaiNhanVien(loaiNVCbx.Text);
            dtoNhanVien.Chucvu = chucVuTbx.Text;
            dtoNhanVien.Loaihd = loaiHopDongCbx.Text;
            dtoNhanVien.Thoigian = int.Parse(thoiGianTbx.Text);
            dtoNhanVien.Ngaydangki = DateTime.Parse(ngayKyDpk.Text);
            dtoNhanVien.Ngayhethan = DateTime.Parse(ngayHetHanDpk.Text);
            dtoNhanVien.Sdt = soDienThoaiTbx.Text;
            dtoNhanVien.Hocvan = hocVanTbx.Text;
            dtoNhanVien.Ghichu = ghiChuTbx.Text;
            dtoNhanVien.Dantoc = danTocCbx.Text;

            if (maNVTbx.Text == string.Empty)
            {
                busNhanVien.ThemNhanVien(dtoNhanVien);
                bool? Result = new MessageBoxCustom("Thêm nhân viên thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }
            else
            {
                dtoNhanVien.Manv = int.Parse(maNVTbx.Text);
                busNhanVien.SuaNhanVien(dtoNhanVien);
                bool? Result = new MessageBoxCustom("Sửa nhân viên thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                dtoLSChinhSua.Ngaychinhsua = DateTime.Now;
                busLSChinhSua.ThemLSChinhSua(dtoLSChinhSua);
                if (maLuongCbx.Text != suaNhanVien.Maluong)
                {
                    DTO_THAYDOIBANGLUONG dtoThayDoiBangLuong = new DTO_THAYDOIBANGLUONG();
                    BUS_THAYDOIBANGLUONG busThayDoiBangLuong = new BUS_THAYDOIBANGLUONG();

                    dtoThayDoiBangLuong.Manv = int.Parse(maNVTbx.Text);
                    dtoThayDoiBangLuong.Maluong = suaNhanVien.Maluong;
                    dtoThayDoiBangLuong.Maluongmoi = maLuongCbx.Text;
                    dtoThayDoiBangLuong.Ngaysua = DateTime.Now;

                    busThayDoiBangLuong.ThemThayDoiBangLuong(dtoThayDoiBangLuong);
                }
            }
            this.Close();
        }

        public void GetOldData()
        {
            dtoLSChinhSua.Manv = int.Parse(maNVTbx.Text);
            dtoLSChinhSua.Lancs = busLSChinhSua.TimLanChinhSuaGanNhat(maNVTbx.Text) + 1;
            dtoLSChinhSua.Maphong = busPhongBan.TimKiemMaPhongBan(phongCbx.Text);
            dtoLSChinhSua.Hoten = tenTbx.Text;
            dtoLSChinhSua.Ngaysinh = DateTime.Parse(ngaySinhDpk.Text);
            dtoLSChinhSua.Gioitinh = gioiTinhCbx.Text;
            dtoLSChinhSua.Cmnd_cccd = cccdTbx.Text;
            dtoLSChinhSua.Noicap = noiCapTbx.Text;
            dtoLSChinhSua.Maluong = maLuongCbx.Text;
            dtoLSChinhSua.Maloainv = busLoaiNV.TimKiemTheoLoaiNhanVien(loaiNVCbx.Text);
            dtoLSChinhSua.Chucvu = chucVuTbx.Text;
            dtoLSChinhSua.Loaihd = loaiHopDongCbx.Text;
            dtoLSChinhSua.Thoigian = int.Parse(thoiGianTbx.Text);
            dtoLSChinhSua.Ngaydangki = DateTime.Parse(ngayKyDpk.Text);
            dtoLSChinhSua.Ngayhethan = DateTime.Parse(ngayHetHanDpk.Text);
            dtoLSChinhSua.Sdt = soDienThoaiTbx.Text;
            dtoLSChinhSua.Hocvan = hocVanTbx.Text;
            dtoLSChinhSua.Ghichu = ghiChuTbx.Text;
            dtoLSChinhSua.Dantoc = danTocCbx.Text;
        }

        public void ComboBoxes_Loaded()
        {
            foreach (var tenPhong in busPhongBan.TongHopPhongBan(""))
            {
                phongCbx.Items.Add(tenPhong);
            }

            foreach (var loaiNV in busLoaiNV.TongHopLoaiNhanVien())
            {
                loaiNVCbx.Items.Add(loaiNV);
            }

            foreach (var maLuong in busBangLuong.TongHopMaLuong())
            {
                maLuongCbx.Items.Add(maLuong);
            }

            string[] listDanToc = new string[] { "Kinh", "Tày", "Thái", "Mường", "Khmer", "Hoa", "Nùng", "H'Mông", "Dao", "Gia Rai",
                                            "Ê Đê", "Ba Na", "Sán Chay", "Chăm", "Kơ Ho", "Xơ Đăng", "Sán Dìu", "Hrê", "Ra Glai",
                                            "Mnông", "Thổ", "Stiêng", "Khơ mú", "Bru - Vân Kiều", "Cơ Tu", "Giáy", "Tà Ôi", "Mạ",
                                            "Giẻ-Triêng", "Co", "Chơ Ro", "Xinh Mun", "Hà Nhì", "Chu Ru", "Lào", "La Chí", "Kháng",
                                            "Phù Lá", "La Hủ", "La Ha", "Pà Thẻn", "Lự", "Ngái", "Chứt", "Lô Lô", "Mảng", "Cơ Lao",
                                            "Bố Y", "Cống", "Si La", "Pu Péo", "Rơ Măm", "Brâu", "Ơ Đu" };

            foreach (string danToc in listDanToc)
            {
                danTocCbx.Items.Add(danToc);
            }

            string[] listGioiTinh = new string[] { "Nam", "Nữ", "Khác" };

            foreach (string gioiTinh in listGioiTinh)
            {
                gioiTinhCbx.Items.Add(gioiTinh);
            }

            string[] listLoaiHD  = new string[] { "Ngắn hạn", "Dài hạn" };

            foreach (string LoaiHD in listLoaiHD)
            {
                loaiHopDongCbx.Items.Add(LoaiHD);
            }
        }

        private void maNVTbx_Loaded(object sender, RoutedEventArgs e)
        {
            if (flag == 1)
                return;
            maNVTbx.Text = suaNhanVien.Manv.ToString();
            tenTbx.Text = suaNhanVien.Hoten.ToString();
            ngaySinhDpk.Text = suaNhanVien.Ngaysinh.ToString("MM/dd/yyyy");
            gioiTinhCbx.Text = suaNhanVien.Gioitinh.ToString();
            cccdTbx.Text = suaNhanVien.Cmnd_cccd.ToString();
            noiCapTbx.Text = suaNhanVien.Noicap.ToString();
            chucVuTbx.Text = suaNhanVien.Chucvu.ToString();
            loaiHopDongCbx.Text = suaNhanVien.Loaihd.ToString();
            thoiGianTbx.Text = suaNhanVien.Thoigian.ToString();
            ngayKyDpk.Text = suaNhanVien.Ngaydangki.ToString("MM/dd/yyyy");
            ngayHetHanDpk.Text = suaNhanVien.Ngayhethan.ToString("MM/dd/yyyy");
            soDienThoaiTbx.Text = suaNhanVien.Sdt.ToString();
            hocVanTbx.Text = suaNhanVien.Hocvan.ToString();
            ghiChuTbx.Text = suaNhanVien.Ghichu.ToString();

            danTocCbx.SelectedItem = suaNhanVien.Dantoc.ToString();
            loaiNVCbx.SelectedItem = busLoaiNV.TimKiemTheoMaLoaiNhanVien(suaNhanVien.Maloainv.ToString());
            phongCbx.SelectedItem = busPhongBan.TimKiemTenPhongBanTheoMa(suaNhanVien.Maphong.ToString());
            maLuongCbx.SelectedItem = suaNhanVien.Maluong.ToString();
            GetOldData();
        }

        private void numberTextBoxes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        public bool CheckData()
        {
            if (phongCbx.Text == String.Empty || tenTbx.Text == String.Empty || ngaySinhDpk.Text == String.Empty
                || gioiTinhCbx.Text == String.Empty || cccdTbx.Text == String.Empty
                || maLuongCbx.Text == String.Empty || loaiNVCbx.Text == String.Empty || chucVuTbx.Text == String.Empty
                || loaiHopDongCbx.Text == String.Empty || thoiGianTbx.Text == String.Empty || ngayKyDpk.Text == String.Empty
                || ngayHetHanDpk.Text == String.Empty || soDienThoaiTbx.Text == String.Empty || hocVanTbx.Text == String.Empty 
                || danTocCbx.Text == String.Empty )
            {
                bool? Result = new MessageBoxCustom("Vui lòng điền đầy đủ thông tin!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return false;
            }

            if (cccdTbx.Text.Length != 9 && cccdTbx.Text.Length != 12)
            {
                bool? result = new MessageBoxCustom("Vui lòng nhập đúng định dạng\n(CMND: 9 chữ số/CCCD: 12 chữ số)", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return false;
            }

            if (soDienThoaiTbx.Text.Length != 10)
            {
                bool? result = new MessageBoxCustom("Vui lòng nhập đúng định dạng số điện thoại 10 số", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return false;
            }
            return true;
        }

        private void thoiGianTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (thoiGianTbx.Text != "")
            {
                if (int.Parse(thoiGianTbx.Text) > 1)
                {
                    loaiHopDongCbx.Text = "Dài hạn";
                }
            }

            if (thoiGianTbx.Text == "1")
            {
                loaiHopDongCbx.Text = "Ngắn hạn";
            }

            if (thoiGianTbx.Text == "")
            {
                ngayHetHanDpk.Text = "";
                return;
            }

            if (ngayKyDpk.Text == "")
            {
                ngayHetHanDpk.Text = "";
                return;
            }

            if (thoiGianTbx.Text.Length > 2)
            {
                bool? Result = new MessageBoxCustom("Số năm quá lớn!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                thoiGianTbx.Text = "";
                return;
            }

            ngayHetHanDpk.SelectedDate = ngayKyDpk.SelectedDate.Value.Date.AddYears(int.Parse(thoiGianTbx.Text));
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

        private void loaiHopDongCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaiHopDongCbx.SelectedValue.ToString() == "Ngắn hạn")
            {
                thoiGianTbx.Text = "1";
            }
            else if (thoiGianTbx.Text == "1")
            {
                thoiGianTbx.Text = "";
            }
            else return;
        }
    }
}
