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

namespace QuanLyNhanVien.MVVM.View
{
    /// <summary>
    /// Interaction logic for uc_Home.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {

        private string baseDir;
        private int index = 0;

        public HomeView()
        {
            InitializeComponent();
            //lấy ra đường dẫn tương đối
            baseDir = Environment.CurrentDirectory;

            //ImageBrush ENABLED_BACKGROUND = new ImageBrush(new BitmapImage(new Uri(baseDir + "\\Res\\Home0.png")));
            ImageBrush ENABLED_BACKGROUND = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/Home0.jpg")));
            this.Background = ENABLED_BACKGROUND;

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();

        }
        #region method
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                index++;
                if (index > 2)
                    index = 0;
                ImageBrush ENABLED_BACKGROUND = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/Home" + index.ToString() + ".jpg")));
                this.Background = ENABLED_BACKGROUND;
            }
            catch (Exception ex)
            {
                bool? Result = new MessageBoxCustom(ex.ToString(), MessageType.Error, MessageButtons.Ok).ShowDialog();
            }

        }
        #endregion

        #region event
        private void right_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                index++;
                if (index > 2)
                    index = 0;
                ImageBrush ENABLED_BACKGROUND = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/Home" + index.ToString() + ".jpg")));
                this.Background = ENABLED_BACKGROUND;
            }
            catch (Exception ex)
            {
                bool? Result = new MessageBoxCustom(ex.ToString(), MessageType.Error, MessageButtons.Ok).ShowDialog();
            }

        }


        private void left_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                index--;
                if (index < 0)
                    index = 2;
                ImageBrush ENABLED_BACKGROUND = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/Home" + index.ToString() + ".jpg")));
                this.Background = ENABLED_BACKGROUND;
            }
            catch (Exception ex)
            {
                bool? Result = new MessageBoxCustom(ex.ToString(), MessageType.Error, MessageButtons.Ok).ShowDialog();
            }

        }
        #endregion
    }
}
