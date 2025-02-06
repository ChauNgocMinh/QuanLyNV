using QuanLyNhanVien.Core;
using QuanLyNhanVien.MVVM.ViewModel.HeThongSubViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanVien.MVVM.ViewModel.NhanVien_ThongTinCaNhanSubViewModel
{
    class MainNhanVien_QLThongTinCaNhanViewModel : ObservableObject
    {
        public RelayCommand ThongTinCaNhanCommand { get; set; }
        public RelayCommand BangLuongCaNhanCommand { get; set; }
        public RelayCommand BangChamCongCaNhanCommand { get; set; }
        public RelayCommand ChamCongCommand { get; set; }

        public ThongTinCaNhanViewModel ThongTinCaNhanVM { get; set; }
        public BangLuongCaNhanViewModel BangLuongCaNhanVM { get; set; }
        public BangChamCongCaNhanViewModel BangChamCongCaNhanVM { get; set; }
        public ChamCongViewModel ChamCongVM { get; set; }

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

        public MainNhanVien_QLThongTinCaNhanViewModel()
        {
            ThongTinCaNhanVM = new ThongTinCaNhanViewModel();
            BangLuongCaNhanVM = new BangLuongCaNhanViewModel();
            BangChamCongCaNhanVM = new BangChamCongCaNhanViewModel();
            ChamCongVM = new ChamCongViewModel();

            CurrentView = ThongTinCaNhanVM;

            ThongTinCaNhanCommand = new RelayCommand(o =>
            {
                CurrentView = ThongTinCaNhanVM;
            });

            BangLuongCaNhanCommand = new RelayCommand(o =>
            {
                CurrentView = BangLuongCaNhanVM;
            });

            BangChamCongCaNhanCommand = new RelayCommand(o =>
            {
                CurrentView = BangChamCongCaNhanVM;
            });

            ChamCongCommand = new RelayCommand(o =>
            {
                CurrentView = ChamCongVM;
            });


        }
    }
}
