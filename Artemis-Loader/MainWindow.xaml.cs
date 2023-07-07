using Artemis_Loader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Artemis_Loader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MessageRecipent? recipent;
        public MainWindow()
        {
            InitializeComponent();
            recipent = new MessageRecipent(this);
        }

        private void TopBorder_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void MinimizeButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void ExitButton_Click(object sender, RoutedEventArgs e) => Artemis.Exit(recipent);
        private void HomeViewButton_Loaded(object sender, RoutedEventArgs e) => ((RadioButton)sender).IsChecked = true;
    }
}
