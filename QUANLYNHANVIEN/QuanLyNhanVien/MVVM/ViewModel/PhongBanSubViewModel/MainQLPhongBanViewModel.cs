using QuanLyNhanVien.Core;
using QuanLyNhanVien.MVVM.ViewModel.PhongBanSubViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanVien.MVVM.ViewModel.PhongBanSubViewModel
{
    class MainQLPhongBanViewModel : ObservableObject
    {
        public RelayCommand PhongBanViewCommand { get; set; }
        public RelayCommand BoPhanViewCommand { get; set; }

        //public RelayCommand SettingRoomStatusesViewCommand { get; set; }
        //public RelayCommand SettingRoomTypesViewCommand { get; set; }
        //public RelayCommand SettingRulesViewCommand { get; set; }

        public PhongBanViewModel PhongBanVM { get; set; }
        public BoPhanViewModel BoPhanVM { get; set; }

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

        public MainQLPhongBanViewModel()
        {
            PhongBanVM = new PhongBanViewModel();
            BoPhanVM = new BoPhanViewModel();

            CurrentView = PhongBanVM;

            PhongBanViewCommand = new RelayCommand(o =>
            {
                CurrentView = PhongBanVM;
            });

            BoPhanViewCommand = new RelayCommand(o =>
            {
                CurrentView = BoPhanVM;
            });
           

        }
    }
}
