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
    /// <summary>
    /// Interaction logic for CustomReminder.xaml
    /// </summary>
    public partial class CustomReminder : Window
    {
        public CustomReminder()
        {
            InitializeComponent();
            CustomReminderViewModel vm = new CustomReminderViewModel();
            DataContext = vm;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CancelButton_Click1(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(CancelButton)) { Close(); }
        }
    }
}
