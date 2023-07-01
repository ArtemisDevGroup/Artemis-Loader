using Artemis_Loader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemis_Loader.ViewModels
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand AboutViewCommand { get; set; }
        public HomeViewModel HomeView { get; set; }
        public object AboutView { get; set; }

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
            HomeView = new HomeViewModel();
            HomeViewCommand = new RelayCommand(o => CurrentView = HomeView);

            AboutView = new object();
            AboutViewCommand = new RelayCommand(o => CurrentView = AboutView);

            CurrentView = HomeView;
        }
    }
}
