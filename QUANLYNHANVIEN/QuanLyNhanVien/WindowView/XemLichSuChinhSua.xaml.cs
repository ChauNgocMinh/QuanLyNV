using BUS;
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
using System.Windows.Shapes;

namespace QuanLyNhanVien.WindowView
{
    /// <summary>
    /// Interaction logic for XemLichSuChinhSua.xaml
    /// </summary>
    public partial class XemLichSuChinhSua : Window
    {
        BUS_LSCHINHSUA busLichSuChinhSua = new BUS_LSCHINHSUA();
        private string maNV;

        public string MaNV { get => maNV; set => maNV = value; }

        public XemLichSuChinhSua()
        {
            InitializeComponent();
        }

        private void thoatBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lsChinhSuaDtg_Loaded(object sender, RoutedEventArgs e)
        {
            lsChinhSuaDtg.DataContext = busLichSuChinhSua.getLSChinhSuaCuaTungNhanVien(MaNV);
        }
    }
}
