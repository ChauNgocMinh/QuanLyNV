using QuanLyNhanVien.Core;
using QuanLyNhanVien.MVVM.ViewModel.ChamCongSubViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanVien.MVVM.ViewModel.BaoCaoThongKeSubViewModel
{
    class MainQLBaoCaoThongKeViewModel : ObservableObject
    {
        public RelayCommand BaoCaoThongKeCommand { get; set; }


        //public RelayCommand SettingRoomStatusesViewCommand { get; set; }
        //public RelayCommand SettingRoomTypesViewCommand { get; set; }
        //public RelayCommand SettingRulesViewCommand { get; set; }

        public BaoCaoThongKeViewModel BaoCaoThongKeVM { get; set; }


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

        public MainQLBaoCaoThongKeViewModel()
        {
            BaoCaoThongKeVM = new BaoCaoThongKeViewModel();


            CurrentView = BaoCaoThongKeVM;

            BaoCaoThongKeCommand = new RelayCommand(o =>
            {
                CurrentView = BaoCaoThongKeVM;
            });

        }
    }
}
