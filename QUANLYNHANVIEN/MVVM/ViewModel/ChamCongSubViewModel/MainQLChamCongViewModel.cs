using QuanLyNhanVien.Core;
using QuanLyNhanVien.MVVM.ViewModel.BangLuongSubViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanVien.MVVM.ViewModel.ChamCongSubViewModel
{
    class MainQLChamCongViewModel : ObservableObject
    {
        public RelayCommand BangChamCongCommand { get; set; }
        public RelayCommand BangChamCongThuViecCommand { get; set; }
        public RelayCommand KhenThuongKyLuatCommand { get; set; }

        //public RelayCommand SettingRoomStatusesViewCommand { get; set; }
        //public RelayCommand SettingRoomTypesViewCommand { get; set; }
        //public RelayCommand SettingRulesViewCommand { get; set; }

        public BangChamCongViewModel BangChamCongVM { get; set; }
        public BangChamCongThuViecViewModel BangChamCongThuViecVM { get; set; }
        public KhenThuongKyLuatViewModel KhenThuongKyLuatVM { get; set; }

        //public SettingRoomStatusesViewModel SettingRoomStatusesVM { get; set; }
        //public SettingRoomTypesViewModel SettingRoomTypesVM { get; set; }
        //public SettingRulesViewModel SettingRulesVM { get; set; }

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

        public MainQLChamCongViewModel()
        {
            BangChamCongVM = new BangChamCongViewModel();
            BangChamCongThuViecVM = new BangChamCongThuViecViewModel();
            KhenThuongKyLuatVM = new KhenThuongKyLuatViewModel();

            CurrentView = BangChamCongVM;

            BangChamCongCommand = new RelayCommand(o =>
            {
                CurrentView = BangChamCongVM;
            });

            KhenThuongKyLuatCommand = new RelayCommand(o =>
            {
                CurrentView = KhenThuongKyLuatVM;
            });

            BangChamCongThuViecCommand = new RelayCommand(o =>
            {
                CurrentView = BangChamCongThuViecVM;
            });


        }
    }
}
