using System;
using QuanLyNhanVien.Core;

namespace QuanLyNhanVien.MVVM.ViewModel.SubViewModel
{
    class MainQLNhanVienViewModel : ObservableObject
    {
        public RelayCommand NhanVienViewCommand { get; set; }
        public RelayCommand BaoHiemNhanVienViewCommand { get; set; }
        public RelayCommand XuatDSNhanVienViewCommand { get; set; }
        public RelayCommand QLThuViecThoiViecViewCommand { get; set; }
        //public RelayCommand SettingRoomStatusesViewCommand { get; set; }
        //public RelayCommand SettingRoomTypesViewCommand { get; set; }
        //public RelayCommand SettingRulesViewCommand { get; set; }

        public NhanVienViewModel NhanVienVM { get; set; }
        public BaoHiemNhanVienViewModel BaoHiemNhanVienVM { get; set; }
        public XuatDSNhanVienViewModel XuatDSNhanVienVM { get; set; }
        public QLThuViecThoiViecViewModel QLThuViecThoiViecVM { get; set; }
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

        public MainQLNhanVienViewModel()
        {
            NhanVienVM = new NhanVienViewModel();
            BaoHiemNhanVienVM = new BaoHiemNhanVienViewModel();
            XuatDSNhanVienVM = new XuatDSNhanVienViewModel();
            QLThuViecThoiViecVM = new QLThuViecThoiViecViewModel();          

            CurrentView = NhanVienVM;

            NhanVienViewCommand = new RelayCommand(o =>
            {
                CurrentView = NhanVienVM;
            });

            BaoHiemNhanVienViewCommand = new RelayCommand(o =>
            {
                CurrentView = BaoHiemNhanVienVM;
            });

            XuatDSNhanVienViewCommand = new RelayCommand(o =>
            {
                CurrentView = XuatDSNhanVienVM;
            });

            QLThuViecThoiViecViewCommand = new RelayCommand(o =>
            {
                CurrentView = QLThuViecThoiViecVM;
            });

            
        }
    }
}
