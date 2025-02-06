using BUS;
using DTO;
using QuanLyNhanVien.MessageBox;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyNhanVien.MVVM.View.NhanVien_ThongTinCaNhanSubView
{
    /// <summary>
    /// Interaction logic for ChamCongView.xaml
    /// </summary>
    public partial class ChamCongView : UserControl
    {
        BUS_LICHSUCHAMCONG busLichSuChamCong = new BUS_LICHSUCHAMCONG();
        BUS_NHANVIENHIENTAI busNhanVienHienTai = new BUS_NHANVIENHIENTAI();
        BUS_LICHSUVANGMAT busLichSuVangMat = new BUS_LICHSUVANGMAT();
        BUS_SOTHAISAN busSoThaiSan = new BUS_SOTHAISAN();
        string maNV;

        public ChamCongView()
        {
            InitializeComponent();
            maNV = busNhanVienHienTai.getNhanVienHienTai();
            
            DataGridLoad();
        }

        public void DataGridLoad()
        {
            thuTbx.Text = GetNgayTiengViet(DateTime.Now.DayOfWeek.ToString());
            if (busSoThaiSan.KiemTraTonTai(maNV) && (busSoThaiSan.TimNgayLamTroLai(maNV) > busLichSuChamCong.TimLanCuoiChamCongTheoMa(maNV)))
            {
                ngayChamCongGanNhatTbx.Text = busSoThaiSan.TimNgayLamTroLai(maNV).ToString("MM/dd/yyyy");
            }
            else if (!busLichSuChamCong.KiemTraTonTai(maNV))
            {
                ngayChamCongGanNhatTbx.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }
            else
            {
                ngayChamCongGanNhatTbx.Text = busLichSuChamCong.TimLanCuoiChamCongTheoMa(maNV).ToString("MM/dd/yyyy");
            }

            thoiGianTbx.Text = DateTime.Today.ToString("MM/dd/yyyy");
            ngayNghiDtg.DataContext = busLichSuVangMat.getLichSuVangMat(maNV);
        }

        public string GetNgayTiengViet(string day)
        {
            string thu = string.Empty;
            switch (day)
            {
                case "Monday":
                    thu = "Hai";
                    break;
                case "Tuesday":
                    thu = "Ba";
                    break;
                case "Wednesday":
                    thu = "Tư";
                    break;
                case "Thursday":
                    thu = "Năm";
                    break;
                case "Friday":
                    thu = "Sáu";
                    break;
                case "Saturday":
                    thu = "Bảy";
                    break;
            }
            return thu;
        }

        private void chamCongBtn_Click(object sender, RoutedEventArgs e)
        {
            DTO_LICHSUCHAMCONG dtoLichSuChamCong = new DTO_LICHSUCHAMCONG();
            if (!busLichSuChamCong.KiemTraTonTai(maNV))
            {
                dtoLichSuChamCong.Manv = int.Parse(maNV);
                dtoLichSuChamCong.Ngaychamconggannhat = DateTime.Now;
                busLichSuChamCong.ThemLichSuChamCong(dtoLichSuChamCong);
            }
            else if (busLichSuChamCong.TimLanCuoiChamCongTheoMa(maNV).Date == DateTime.Now.Date)
            {
                bool? result = new MessageBoxCustom("Hôm nay đã chấm công", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;

            }
            else
            {            
                TimeSpan time = DateTime.Parse(thoiGianTbx.Text) - DateTime.Parse(ngayChamCongGanNhatTbx.Text);
                
                for (int i = 1; i < time.Days; i++)
                {
                    if (DateTime.Now.Date.AddDays(-i).DayOfWeek.ToString() == "Sunday")
                    {
                        continue;
                    }
                    else
                    {
                        DTO_LICHSUVANGMAT lichSuVangMat = new DTO_LICHSUVANGMAT();

                        lichSuVangMat.Manv = int.Parse(maNV);
                        lichSuVangMat.Ngaynghi = DateTime.Now.Date.AddDays(-i);

                        busLichSuVangMat.ThemLichSuVangMat(lichSuVangMat);
                    }
                }

                dtoLichSuChamCong.Manv = int.Parse(maNV);
                dtoLichSuChamCong.Ngaychamconggannhat = DateTime.Now;
                busLichSuChamCong.SuaLichSuChamCong(dtoLichSuChamCong);

                bool? result = new MessageBoxCustom("Chấm công thành công", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }
            DataGridLoad();
        }
    }
}
