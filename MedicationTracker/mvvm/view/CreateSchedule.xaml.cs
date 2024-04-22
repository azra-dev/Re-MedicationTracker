using MedicationTracker.MVVM.Model;
using MedicationTracker.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class CreateSchedule : Window
    {
        public CreateSchedule()
        {
            InitializeComponent();
            CreateScheduleViewModel vm = new CreateScheduleViewModel();
            DataContext = vm;
            vm.ReadMedAndSched.Execute(null);
            vm.ReadUserInfo.Execute(null);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
