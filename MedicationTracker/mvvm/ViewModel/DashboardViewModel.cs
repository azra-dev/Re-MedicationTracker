using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class DashboardViewModel : ObservableObject
    {
        //int navMode = 0; // 0 is default (Home) <- What's this?

        public DataAccessLayer DAL { get; set; }
        public RelayCommand ReadSchedules => new RelayCommand(execute => ReadMedicationSchedules());
        public RelayCommand ReadReminders => new RelayCommand(execute => ReadMedicationReminders());

        public ObservableCollection<DashboardModel.JoinedMedicationSchedule> JoinedMedicationsSchedulesContent { get; set; }
        public ObservableCollection<DashboardModel.MedicationReminder> MedicationReminders {  get; set; }

        public DashboardViewModel()
        {
            DAL = new DataAccessLayer();
            JoinedMedicationsSchedulesContent = new ObservableCollection<DashboardModel.JoinedMedicationSchedule>();
            MedicationReminders = new ObservableCollection<DashboardModel.MedicationReminder>();

        }

        private DashboardModel.JoinedMedicationSchedule medicationSchedulesContent;

        public DashboardModel.JoinedMedicationSchedule MedicationSchedulesContent
        {
            get { return medicationSchedulesContent; }
            set 
            {
                medicationSchedulesContent = value;
                OnPropertyChanged();
            }
        }

        private DashboardModel.MedicationReminder medicationRemindersContent;

        public DashboardModel.MedicationReminder MedicationRemindersContent
        {
            get { return medicationRemindersContent; }
            set 
            { 
                medicationRemindersContent = value; 
                OnPropertyChanged();
            }
        }

        private void ReadMedicationReminders()
        {
            DAL.JoinsMedicationSchedulesReminders(1, MedicationRemindersContent, MedicationReminders); // user_id parameter here is temporary

        }
        private void ReadMedicationSchedules()
        {
            DAL.JoinMedicationsSchedules(1, MedicationSchedulesContent, JoinedMedicationsSchedulesContent);

        }




    }

}
