using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class DashboardViewModel : ObservableObject
    {
        //int navMode = 0; // 0 is default (Home) <- What's this?

        public DataAccessLayer DAL { get; set; }
        public RelayCommand ReadUserInfo => new RelayCommand(execute => ReadMediTrackUserInformation());
        public RelayCommand ReadSchedules => new RelayCommand(execute => ReadMedicationSchedules());
        public RelayCommand ReadReminders => new RelayCommand(execute => ReadMedicationReminders());
        public RelayCommand GetInitialRemTitle => new RelayCommand(execute => GetInitialReminderTitle(execute));
        public RelayCommand MakeLog => new RelayCommand(execute => CreateNewLog(execute));

        public ObservableCollection<DashboardModel.JoinedMedicationSchedule> JoinedMedicationsSchedulesContent { get; set; }
        public ObservableCollection<DashboardModel.MedicationReminder> MedicationReminders {  get; set; }

        public DashboardViewModel()
        {
            DAL = new DataAccessLayer();
            MediTrackUserInfo = new DataAccessLayer.MediTrackUser();
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

        private DataAccessLayer.MediTrackUser meditrackuserinfo;

        public DataAccessLayer.MediTrackUser MediTrackUserInfo
        {
            get { return meditrackuserinfo; }
            set 
            { 
                meditrackuserinfo = value; 
                OnPropertyChanged();
            }
        }


        public void ReadMediTrackUserInformation()
        {
            MediTrackUserInfo = (DataAccessLayer.MediTrackUser)DAL.ReadMediTrackUserByID(1);    // user_id here is temporary

            byte[] imageData = MediTrackUserInfo.Image;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();

            MediTrackUserInfo.ProfilePicture = image;

            OnPropertyChanged("MediTrackUserInfo");
        }

        public void ReadMedicationReminders()
        {
            DAL.JoinsMedicationSchedulesReminders(1, MedicationRemindersContent, MedicationReminders); // user_id parameter here is temporary

        }
        public void ReadMedicationSchedules()
        {
            DAL.JoinMedicationsSchedules(1, MedicationSchedulesContent, JoinedMedicationsSchedulesContent); // user_id parameter here is temporary

        }

        public void GetInitialReminderTitle(object parameter)
        {
            string reminderToUpdate = parameter as string;

            if (reminderToUpdate != null)
            {
                Trace.WriteLine(reminderToUpdate);

                // TO BE IMPLEMENTED BY RJLDG WHEN CONNECTED TO CUSTOMREMINDER MODAL
            }
        }

        public void CreateNewLog(object parameter)
        {
            string reminderTitle = parameter as string;

            if(reminderTitle != null)
            {
                DAL.CreateLogs(1, reminderTitle);   // user_id here is temporary
            }
        }



    }

}
