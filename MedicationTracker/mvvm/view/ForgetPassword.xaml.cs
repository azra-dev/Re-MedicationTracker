using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MedicationTracker.MVVM.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ForgetPassword : Window
    {
        public ForgetPassword()
        {
            InitializeComponent();
        }

        private void taskClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            // not being used
        }

        private void Proceed_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        // Drag the window without taskbar
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
