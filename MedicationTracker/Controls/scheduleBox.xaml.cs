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
    /// Interaction logic for scheduleBox.xaml
    /// </summary>
    public partial class scheduleBox : UserControl , INotifyPropertyChanged
    {
        public scheduleBox()
        {
            DataContext = this;
            InitializeComponent();
        }

        public string medTitle;
        public string medType;
        public string medAmount;
        public string medSchedule;
        public DateTime medTime;

        public string MedTitle
        {
            get { return medTitle; }
            set { medTitle = value; OnPropertyChanged("MedTitle"); }
        }
        public string MedType
        {
            get { return medType; }
            set { medType = value; OnPropertyChanged("MedType"); }
        }
        public string MedAmount
        {
            get { return medAmount; }
            set { medAmount = value; OnPropertyChanged("MedAmount"); }
        }
        public string MedSchedule
        {
            get { return medSchedule; }
            set { medSchedule = value; OnPropertyChanged("MedSchedule"); }
        }
        public DateTime MedTime
        {
            get { return medTime; }
            set { medTime = value; OnPropertyChanged("MedTime"); }
        }

        // events
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
