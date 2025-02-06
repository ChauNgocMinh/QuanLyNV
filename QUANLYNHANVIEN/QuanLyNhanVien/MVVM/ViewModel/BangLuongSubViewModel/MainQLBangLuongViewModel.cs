using QuanLyNhanVien.Core;
using QuanLyNhanVien.MVVM.ViewModel.PhongBanSubViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanVien.MVVM.ViewModel.BangLuongSubViewModel
{
    class MainQLBangLuongViewModel : ObservableObject
    {
        public RelayCommand BangLuongCommand { get; set; }
        public RelayCommand ThayDoiBangluongCommand { get; set; }
        public RelayCommand BangTinhLuongCommand { get; set; }

        //public RelayCommand SettingRoomStatusesViewCommand { get; set; }
        //public RelayCommand SettingRoomTypesViewCommand { get; set; }
        //public RelayCommand SettingRulesViewCommand { get; set; }

        public BangLuongViewModel BangLuongVM { get; set; }
        public ThayDoiBangLuongViewModel ThayDoiBangLuongVM { get; set; }
        public BangTinhLuongViewModel BangTinhLuongVM { get; set; }

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

        public MainQLBangLuongViewModel()
        {
            BangLuongVM = new BangLuongViewModel();
            ThayDoiBangLuongVM = new ThayDoiBangLuongViewModel();
            BangTinhLuongVM = new BangTinhLuongViewModel();

            CurrentView = BangLuongVM;

            BangLuongCommand = new RelayCommand(o =>
            {
                CurrentView = BangLuongVM;
            });

            ThayDoiBangluongCommand = new RelayCommand(o =>
            {
                CurrentView = ThayDoiBangLuongVM;
            });

            BangTinhLuongCommand = new RelayCommand(o =>
            {
                CurrentView = BangTinhLuongVM;
            });


        }
    }
}