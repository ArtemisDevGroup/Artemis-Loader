using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.IO;

using Artemis.Loader.Core;
using System.Windows.Media;

namespace Artemis.Loader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ITrayIcon
    {
        public IntPtr InstanceHandle { get; set; }
        public IntPtr HelperWindowHandle { get; set; }
        public IntPtr NotifyIconDataPtr { get; set; }
        public IntPtr WndClassExPtr { get; set; }
        public bool IsValidInstance { get; set; }

        private ITrayIcon TrayIconInstance { get { return this; } }

        public MainWindow()
        {
            InitializeComponent();

            new DiscordRichPresence();

            TrayIconInstance.Initialize();
            TrayIconInstance.ShowIcon();

            if (File.Exists(Globals.file.GameDir + @"\LumaPlayFiles\LumaPlay.ini"))
            {
                using (FileStream fs = new FileStream(Globals.file.GameDir + @"\LumaPlayFiles\LumaPlay.ini", FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        sr.ReadLine();
                        sr.ReadLine();
                        try
                        {
                            Globals.CurrentUsername = sr.ReadLine()?.Substring(13) ?? String.Empty;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Globals.CurrentUsername = String.Empty;
                        }

                        sr.Close();
                    }
                    fs.Close();
                }
            }
            else Globals.CurrentUsername = String.Empty;
        }

        public void TrayIcon_OnNotifyIcon()
        {
            Dispatcher.Invoke(() =>
            {
                if (Visibility == Visibility.Visible)
                    Visibility = Visibility.Hidden;
                else Visibility = Visibility.Visible;
            });
        }

        private void OnClick_Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OnClick_Exit(object sender, RoutedEventArgs e)
        {
            TrayIconInstance.HideIcon();
            TrayIconInstance.Release();
            Application.Current.Shutdown();
        }

        private void OnEnter_ChangeRed(object sender, MouseEventArgs e)
        {
            Control c = (Control)sender;
            c.Background = Brushes.Red;
        }

        private void OnEnter_ChangeGray(object sender, MouseEventArgs e)
        {
            Control c = (Control)sender;
            c.Background = new BrushConverter().ConvertFromString("#2A2A2A") as Brush;
        }

        private void OnLeave_Reset(object sender, MouseEventArgs e)
        {
            Control c = (Control)sender;
            c.Background = Brushes.Transparent;
        }

        private void TopBorder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
