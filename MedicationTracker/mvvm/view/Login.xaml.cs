using MedicationTracker.MVVM.View;
using MedicationTracker.MVVM.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedicationTracker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginViewModel vm = new LoginViewModel();
            DataContext = vm;
        }

        // Drag the window without taskbar
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void RegisterLink_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        private void ForgetPassword_Click(object sender, RoutedEventArgs e)
        {
            ForgetPassword forgetpass = new ForgetPassword();
            forgetpass.ShowDialog();
        }

        public void Exit_Window()
        {
            this.Close();
        }
    }
}
