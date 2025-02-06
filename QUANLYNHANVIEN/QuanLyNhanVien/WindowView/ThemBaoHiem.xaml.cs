using System;
using DTO;
using BUS;
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

namespace QuanLyNhanVien.WindowView
{
    /// <summary>
    /// Interaction logic for ThemBaoHiem.xaml
    /// </summary>
    public partial class ThemBaoHiem : Window
    {
        public BUS_SOBH busBaoHiem = new BUS_SOBH();
        public BUS_NHANVIEN busNhanVien = new BUS_NHANVIEN();
        public DTO_SOBH suaBaoHiem;
        
        public bool checkAdd;
        public ThemBaoHiem(bool CheckAdd)
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
            foreach (var maNV in busNhanVien.TongHopMaNhanVien())
            {
                maNVCbx.Items.Add(maNV);
            }
            ngayCapTbx.SelectedDate = DateTime.Now;
        }

        private void btnThemSua_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                if (ngayCapTbx.Text == String.Empty || noiCapTbx.Text == String.Empty)
                {
                    bool? Result = new MessageBoxCustom("Vui lòng thêm thông tin đầy đủ!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }

                DTO_SOBH dtoSoBH = new DTO_SOBH();
                dtoSoBH.Manv = int.Parse(maNVCbx.SelectedValue.ToString());
                dtoSoBH.Ngaycapso = DateTime.Parse(ngayCapTbx.Text);
                dtoSoBH.Noicapso = noiCapTbx.Text;
                dtoSoBH.Ghichu = ghiChuTbx.Text;

                if (maBHTbx.Text == string.Empty)
                {
                    busBaoHiem.ThemSoBH(dtoSoBH);
                    bool? Result = new MessageBoxCustom("Thêm bảo hiểm thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                else
                {
                    dtoSoBH.Mabh = int.Parse(maBHTbx.Text);
                    busBaoHiem.SuaSoBH(dtoSoBH);
                    bool? Result = new MessageBoxCustom("Sửa bảo hiểm thành công!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                this.Close();
            }
            catch
            {
                bool? result = new MessageBoxCustom("Đã xảy ra lỗi khi lưu!\nVui lòng kiểm tra lại dữ liệu.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            
        }

        private void maBHTbx_Loaded(object sender, RoutedEventArgs e)
        {
            if (checkAdd)
                return;
            maBHTbx.Text = suaBaoHiem.Mabh.ToString();
            maNVCbx.SelectedItem = suaBaoHiem.Manv.ToString();
            ngayCapTbx.Text = suaBaoHiem.Ngaycapso.ToString();
            noiCapTbx.Text = suaBaoHiem.Noicapso.ToString();
            ghiChuTbx.Text = suaBaoHiem.Ghichu.ToString();
        }
    }
}
