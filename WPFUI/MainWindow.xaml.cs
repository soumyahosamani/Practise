using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            // UI Thread.. 
            LoginButton.IsEnabled = false;
            LoginButton.Content = "waiting..";
            var result = await LoginAsync();
            LoginButton.Content = result;
        }


        private async Task<string> LoginAsync()
        {
            var result = await Task.Run(() =>
                {
                    // different thread.
                    Thread.Sleep(1000);
                    return "welcome";
            }
                );
            LoginButton.IsEnabled = true;
            return result;
        }
    }
}
