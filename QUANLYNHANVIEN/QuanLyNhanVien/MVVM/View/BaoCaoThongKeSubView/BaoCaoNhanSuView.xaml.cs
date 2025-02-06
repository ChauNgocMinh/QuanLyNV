using BUS;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace QuanLyNhanVien.MVVM.View.BaoCaoThongKeSubView
{
    /// <summary>
    /// Interaction logic for BaoCaoNhanSuView.xaml
    /// </summary>
    public partial class BaoCaoNhanSuView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        BUS_NHANVIEN busNV = new BUS_NHANVIEN();
        BUS_NVTHOIVIEC busNVTV = new BUS_NVTHOIVIEC();
        BUS_HOSOTHUVIEC busHSTV = new BUS_HOSOTHUVIEC();
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public void add_items_cbx()
        {
            for (int i = 1; i <= 12; i++)
            {
                thangCbx.Items.Add(i);
            }

            if (busNV.TimNamDauTienNVVaoLam() < 2000)
            {
                for (int i = busNV.TimNamDauTienNVVaoLam(); i <= (busNV.TimNamGanNhatNVVaoLam() + 20); i++)
                {
                    namCbx.Items.Add(i);
                }
            }
            else
            {
                for (int i = 2000; i <= (busNV.TimNamGanNhatNVVaoLam() + 20); i++)
                {
                    namCbx.Items.Add(i);
                }
            }

            
        }
        ColumnSeries nvtv = new ColumnSeries()
        {
            Title = "Nhân viên thử việc",
        };
        ColumnSeries nv = new ColumnSeries()
        {
            Title = "Nhân viên vào làm",
        };
        ColumnSeries nvnv = new ColumnSeries()
        {
            Title = "Nhân viên nghỉ việc",
        };

        public BaoCaoNhanSuView()
        {
            InitializeComponent();
            add_items_cbx();
            SeriesCollection = new SeriesCollection();
            set_default();
            DataContext = this;
        }

        private void set_default()
        {
            thangCbx.Text = DateTime.Now.Month.ToString();
            namCbx.Text = DateTime.Now.Year.ToString();
            int n = busNV.SoLuongNhanVienVaoLam(1, 2023);
            nv.Values = new ChartValues<int> { n };
            SeriesCollection.Add(nv);
            n = busNVTV.SoLuongNhanVienNghiViec(1, 2023);
            nvnv.Values = new ChartValues<int> { n };
            SeriesCollection.Add(nvnv);
            n = busHSTV.SoLuongNhanVienThuViec(1, 2023);
            nvtv.Values = new ChartValues<int> { n };
            SeriesCollection.Add(nvtv);
        }

        private void timKiemBtn_Click(object sender, RoutedEventArgs e)
        {
            if(thangCbx.Text!=""&&namCbx.Text!="")
            {
                int n = busNV.SoLuongNhanVienVaoLam(Convert.ToInt32(thangCbx.Text),Convert.ToInt32(namCbx.Text));
                nv.Values = new ChartValues<int> { n };
                n = busNVTV.SoLuongNhanVienNghiViec(Convert.ToInt32(thangCbx.Text), Convert.ToInt32(namCbx.Text));
                nvnv.Values = new ChartValues<int> { n };
                n = busHSTV.SoLuongNhanVienThuViec(Convert.ToInt32(thangCbx.Text), Convert.ToInt32(namCbx.Text));
                nvtv.Values = new ChartValues<int> { n };
            }
        }
    }
}
