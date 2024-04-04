using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MedicationTracker.Controls
{
    /// <summary>
    /// Interaction logic for reminderActive.xaml
    /// </summary>
    public partial class reminderActive : UserControl, INotifyPropertyChanged
    {
        public reminderActive()
        {
            DataContext = this;
            InitializeComponent();
        }

        private string reminderTitle;
        private string reminderDescription;

        public string ReminderTitle
        {
            get { return reminderTitle; }
            set { reminderTitle = value; OnPropertyChanged("ReminderTitle"); }
        }
        public string ReminderDescription
        {
            get { return reminderDescription; }
            set { reminderDescription = value; OnPropertyChanged("ReminderDescription"); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
