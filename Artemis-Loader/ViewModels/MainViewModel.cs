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
        public RelayCommand ExtensionViewCommand { get; set; }
        public RelayCommand BrowserViewCommand { get; set; }
        public RelayCommand DeveloperViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        public RelayCommand AboutViewCommand { get; set; }

        public HomeViewModel HomeView { get; set; }
        public object ExtensionView { get; set; }
        public object BrowserView { get; set; }
        public object DeveloperView { get; set; }
        public object SettingsView { get; set; }
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

            ExtensionView = new object();
            ExtensionViewCommand = new RelayCommand(o => CurrentView = ExtensionView);

            BrowserView = new object();
            BrowserViewCommand = new RelayCommand(o => CurrentView = BrowserView);

            DeveloperView = new object();
            DeveloperViewCommand = new RelayCommand(o => CurrentView = DeveloperView);

            SettingsView = new object();
            SettingsViewCommand = new RelayCommand(o => CurrentView = SettingsView);

            AboutView = new object();
            AboutViewCommand = new RelayCommand(o => CurrentView = AboutView);

            CurrentView = HomeView;
        }
    }
}
