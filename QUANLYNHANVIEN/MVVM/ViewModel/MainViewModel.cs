using QuanLyNhanVien.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanVien.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand QLNhanVienViewCommand { get; set; }
        public RelayCommand QLPhongBanViewCommand { get; set; }
        public RelayCommand QLChamCongViewCommand { get; set; }
        public RelayCommand TraCuuThongTinViewCommand { get; set; }
        public RelayCommand QLBaoCaoThongKeViewCommand { get; set; }
        public RelayCommand QLBangLuongViewCommand { get; set; }
        public RelayCommand QLHeThongViewCommand { get; set; }
        //public RelayCommand DiscoverViewCommand { get; set; }
        //public RelayCommand QL_NhanVienViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public QLNhanVienViewModel QLNhanVienVM { get; set; }
        public QLPhongBanViewModel QLPhongBanVM { get; set; }
        public QLChamCongViewModel QLChamCongVM { get; set; }
        public TraCuuThongTinViewModel TraCuuThongTinVM { get; set; }
        public QLBaoCaoThongKeViewModel QLBaoCaoThongKeVM { get; set; }
        public QLBangLuongViewModel QLBangLuongVM { get; set; }
        public QLHeThongViewModel QLHeThongVM { get; set; }
        //public DiscoverViewModel DiscoverVM { get; set; }
        //public DiscoverViewModel QL_NhanVienVM { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            QLNhanVienVM = new QLNhanVienViewModel();
            QLPhongBanVM = new QLPhongBanViewModel();
            QLChamCongVM = new QLChamCongViewModel();
            TraCuuThongTinVM = new TraCuuThongTinViewModel();  
            QLBaoCaoThongKeVM = new QLBaoCaoThongKeViewModel(); 
            QLBangLuongVM = new QLBangLuongViewModel(); 
            QLHeThongVM = new QLHeThongViewModel(); 
            //DiscoverVM = new DiscoverViewModel();

            CurrentView = HomeVM; //Đúng phải là HomeVM

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            QLNhanVienViewCommand = new RelayCommand(o =>
            {
                CurrentView = QLNhanVienVM;
            });

            QLPhongBanViewCommand = new RelayCommand(o =>
            {
                CurrentView = QLPhongBanVM;
            });

            QLChamCongViewCommand = new RelayCommand(o =>
            {
                CurrentView = QLChamCongVM;
            });

            TraCuuThongTinViewCommand = new RelayCommand(o =>
            {
                CurrentView = TraCuuThongTinVM;
            });

            QLBaoCaoThongKeViewCommand = new RelayCommand(o =>
            {
                CurrentView = QLBaoCaoThongKeVM;
            });
            QLBangLuongViewCommand = new RelayCommand(o =>
            {
                CurrentView = QLBangLuongVM;
            });
            QLHeThongViewCommand = new RelayCommand(o =>
            {
                CurrentView = QLHeThongVM;
            });


        }
    }
}
