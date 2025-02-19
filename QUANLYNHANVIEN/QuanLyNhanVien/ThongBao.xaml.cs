using BUS;
using System.Data;
using System.Windows;

namespace QuanLyNhanVien
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ThongBao : Window
    {
        BUS_THONGBAO tb = new BUS_THONGBAO();

        public ThongBao()
        {
            InitializeComponent();
            LoadThongBao(1);
        }

        private void LoadThongBao(int maUser)
        {
            DataTable dt = tb.getThongBao(maUser);
            dgThongBao.ItemsSource = dt.DefaultView;
        }
    }
}
