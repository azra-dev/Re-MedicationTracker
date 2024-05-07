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
using System.Windows.Shapes;

namespace MedicationTracker.MVVM.View
{
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            DashboardViewModel vm = new DashboardViewModel();
            DataContext = vm;
            vm.ReadSchedules.Execute(null);
            vm.ReadReminders.Execute(null);
            vm.ReadUserInfo.Execute(null);
        }

        // Drag the window without taskbar
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CustomReminder_Click(object sender, RoutedEventArgs e)
        {
            CustomReminder customReminder = new CustomReminder();
            customReminder.ShowDialog();
        }

        // Sidebar Default Events
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            CreateSchedule createSchedule = new CreateSchedule();
            createSchedule.Show();
            this.Close();
        }

        private void Logs_Click(object sender, RoutedEventArgs e)
        {
            Logs logs = new Logs();
            logs.Show();
            this.Close();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            UserProfile profile = new UserProfile();
            profile.Show();
            this.Close();
        }
    }
}
