using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace MedicationTracker.MVVM.View
{
    /// <summary>
    /// Interaction logic for ScheduleModal.xaml
    /// </summary>
    public partial class ScheduleModal : Window, INotifyPropertyChanged
    {
        public ScheduleModal()
        {
            DataContext = this;
            InitializeComponent();
        }

        private string scheduleMode = null;
        private bool isPrescribed = false;

        public bool IsPrescribed
        {
            get { return isPrescribed; }
            set { 
                isPrescribed = value;
                if (isPrescribed) SubmitButton.Content = "Next";
                else SubmitButton.Content = "Create";
                OnPropertyChanged("IsPrescribed");
            }
        }

        public string ScheduleMode
        {
            get { return scheduleMode = null; }
            set { 
                scheduleMode = value;
                Trace.WriteLine(scheduleMode);
                if      (value == "System.Windows.Controls.ComboBoxItem: Daily")  { ScheduleList_Daily.Visibility = Visibility.Visible;   ScheduleList_Weekly.Visibility = Visibility.Collapsed; }
                else if (value == "System.Windows.Controls.ComboBoxItem: Weekly") { ScheduleList_Daily.Visibility = Visibility.Collapsed; ScheduleList_Weekly.Visibility = Visibility.Visible  ; }
                else                                                              { ScheduleList_Daily.Visibility = Visibility.Collapsed; ScheduleList_Weekly.Visibility = Visibility.Collapsed; }
                OnPropertyChanged("ScheduleMode"); 
            }
        }


        // events
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
