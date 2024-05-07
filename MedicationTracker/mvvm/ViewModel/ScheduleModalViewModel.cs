using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class ScheduleModalViewModel : ObservableObject
    {
        public DataAccessLayer DAL { get; set; }
        public CreateScheduleViewModel RefreshMedSchedInfo { get; set; }
        public RelayCommand CreateMed => new RelayCommand(execute => CreateMedicine());
        public RelayCommand CreatePrescDoc => new RelayCommand(execute => CreatePrescriptionAndDoctor());
        
        public ScheduleModalViewModel()
        {
            DAL = new DataAccessLayer();
            MedicationInformation = new ScheduleModalModel.MedicationInfo();
            MedicationScheduleInformation = new ScheduleModalModel.MedicationScheduleInfo();
            MedicationReminderInformation = new ScheduleModalModel.MedicationReminderInfo();
            MedicationPrescriptionInformation = new ScheduleModalModel.MedicationPrescriptionInfo();
            MedicationPrescriptionDoctorInformation = new ScheduleModalModel.MedicationPrescriptionDoctor();

            RefreshMedSchedInfo = new CreateScheduleViewModel();
        }

        private ScheduleModalModel.MedicationInfo medicationInformation;

        public ScheduleModalModel.MedicationInfo MedicationInformation
        {
            get { return medicationInformation; }
            set 
            {
                medicationInformation = value;
                OnPropertyChanged();
            }
        }

        private ScheduleModalModel.MedicationScheduleInfo medicationScheduleInformation;

        public ScheduleModalModel.MedicationScheduleInfo MedicationScheduleInformation
        {
            get { return medicationScheduleInformation; }
            set 
            {
                medicationScheduleInformation = value; 
                OnPropertyChanged();
            }
        }

        private ScheduleModalModel.MedicationReminderInfo medicationReminderInformation;

        public ScheduleModalModel.MedicationReminderInfo MedicationReminderInformation
        {
            get { return medicationReminderInformation; }
            set 
            {
                medicationReminderInformation = value; 
                OnPropertyChanged();
            }
        }

        private ScheduleModalModel.MedicationPrescriptionInfo medicationPrescriptionInformation;

        public ScheduleModalModel.MedicationPrescriptionInfo  MedicationPrescriptionInformation
        {
            get { return medicationPrescriptionInformation; }
            set 
            { 

                medicationPrescriptionInformation = value;
                OnPropertyChanged();
            }
        }

        private ScheduleModalModel.MedicationPrescriptionDoctor medicationPrescriptionDoctorInformation;

        public ScheduleModalModel.MedicationPrescriptionDoctor MedicationPrescriptionDoctorInformation
        {
            get { return medicationPrescriptionDoctorInformation; }
            set 
            { 
                medicationPrescriptionDoctorInformation = value;
                OnPropertyChanged();
            }
        }

        public void CreateMedicine()
        {
            try
            {
                if(medicationScheduleInformation.MedicationPeriod is not "As Needed")
                {
                    Trace.WriteLine(DateTime.ParseExact(MedicationScheduleInformation.Time_1, "HH:mm", CultureInfo.InvariantCulture));
                    if (MedicationScheduleInformation.Time_2 != null) Trace.WriteLine(DateTime.ParseExact(MedicationScheduleInformation.Time_2, "HH:mm", CultureInfo.InvariantCulture));
                    if (MedicationScheduleInformation.Time_3 != null) Trace.WriteLine(DateTime.ParseExact(MedicationScheduleInformation.Time_3, "HH:mm", CultureInfo.InvariantCulture));
                    if (MedicationScheduleInformation.Time_4 != null) Trace.WriteLine(DateTime.ParseExact(MedicationScheduleInformation.Time_4, "HH:mm", CultureInfo.InvariantCulture));
                }

                //MessageBox.Show("Time Input Success", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                DAL.CreateMedication(ServiceLocator.CurrentUser.UserID, MedicationInformation); 

                MedicationScheduleInformation.MedicationID = DAL.SearchMedIDByUserIDAndMedName(ServiceLocator.CurrentUser.UserID, MedicationInformation.MedicationName);    // userID here is temporary

                DAL.CreateSchedule(MedicationScheduleInformation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Time Input. \n\n Exception: " + ex, "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            RefreshMedSchedInfo.RefreshMedicationsAndSchedulesInfo();

            OnPropertyChanged();

        }

        public void CreatePrescriptionAndDoctor()
        {
            DAL.CreateMedication(ServiceLocator.CurrentUser.UserID, MedicationInformation);

            MedicationScheduleInformation.MedicationID = DAL.SearchMedIDByUserIDAndMedName(ServiceLocator.CurrentUser.UserID, MedicationInformation.MedicationName);    // userID here is temporary
            MedicationPrescriptionInformation.MedicationID = MedicationScheduleInformation.MedicationID;

            DAL.CreateSchedule(MedicationScheduleInformation);
            DAL.CreatePrescription(MedicationPrescriptionInformation);

            MedicationPrescriptionDoctorInformation.MedicationPrescriptionID = DAL.SearchPrescID(MedicationPrescriptionInformation.MedicationID);

            DAL.CreateDoctor(MedicationPrescriptionDoctorInformation);

            RefreshMedSchedInfo.RefreshMedicationsAndSchedulesInfo();

            OnPropertyChanged();
        }



    }
}
