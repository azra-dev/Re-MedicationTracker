using MedicationTracker.MVVM.ViewModel;
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
    public partial class ScheduleModal : Window, INotifyPropertyChanged
    {
        public ScheduleModal()
        {
            InitializeComponent();
            ScheduleModalViewModel vm = new ScheduleModalViewModel();
            DataContext = vm;
            PrescribedCheckBox.IsChecked = false;
        }

        private string scheduleMode = null;
        private bool isPrescribed = false;

        public bool IsPrescribed
        {
            get { return isPrescribed; }
            set { 
                isPrescribed = value;
                OnPropertyChanged("IsPrescribed");
            }
        }

        public string ScheduleMode
        {
            get { return scheduleMode = null; }
            set { 
                scheduleMode = value;
                OnPropertyChanged("ScheduleMode"); 
            }
        }


        // Event handlers
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PrescribedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SubmitButton.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Visible;
        }

        private void PrescribedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SubmitButton.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Collapsed;
        }

        private void PrescribedCheckBox_Indeterminate(object sender, RoutedEventArgs e)
        {
            // Do nothing
        }

        private void SchedulePeriodComboBox_Selected(object sender, RoutedEventArgs e)
        {
            string selectedValue = SchedulePeriodComboBox.SelectedValue.ToString();

            Trace.WriteLine(SchedulePeriodComboBox.SelectedValue);
            if      (selectedValue == "System.Windows.Controls.ComboBoxItem: Daily")    { ScheduleList_Daily.Visibility = Visibility.Visible; ScheduleList_Weekly.Visibility = Visibility.Collapsed; }
            else if (selectedValue == "System.Windows.Controls.ComboBoxItem: Weekly")   { ScheduleList_Daily.Visibility = Visibility.Collapsed; ScheduleList_Weekly.Visibility = Visibility.Visible; }
            else                                                                        { ScheduleList_Daily.Visibility = Visibility.Collapsed; ScheduleList_Weekly.Visibility = Visibility.Collapsed; }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(CancelButton))    { Close(); }
            else if (sender.Equals(BackButton)) {
                MedicationSchedulePage.Visibility = Visibility.Visible;
                PrescriptionPage.Visibility = Visibility.Collapsed;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            MedicationSchedulePage.Visibility = Visibility.Collapsed;
            PrescriptionPage.Visibility = Visibility.Visible;
        }
    }
}
