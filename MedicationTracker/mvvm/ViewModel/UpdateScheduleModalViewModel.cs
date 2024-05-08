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
    internal class UpdateScheduleModalViewModel : ObservableObject
    {
        public DataAccessLayer DAL { get; set; }
        public RelayCommand UpdateMed => new RelayCommand(execute => UpdateMedicine(execute));
        public RelayCommand UpdatePrescDoc => new RelayCommand(execute => UpdatePrescriptionDoctor());

        public UpdateScheduleModalViewModel()
        {
            DAL = new DataAccessLayer();
            MedicationInformation = new UpdateScheduleModalModel.MedicationInfo();
            MedicationScheduleInformation = new UpdateScheduleModalModel.MedicationScheduleInfo();
            MedicationReminderInformation = new UpdateScheduleModalModel.MedicationReminderInfo();
            MedicationPrescriptionInformation = new UpdateScheduleModalModel.MedicationPrescriptionInfo();
            MedicationPrescriptionDoctorInformation = new UpdateScheduleModalModel.MedicationPrescriptionDoctor();

        }

        private UpdateScheduleModalModel.MedicationInfo medicationInformation;

        public UpdateScheduleModalModel.MedicationInfo MedicationInformation
        {
            get { return medicationInformation; }
            set
            {
                medicationInformation = value;
                OnPropertyChanged();
            }
        }

        private UpdateScheduleModalModel.MedicationScheduleInfo medicationScheduleInformation;

        public UpdateScheduleModalModel.MedicationScheduleInfo MedicationScheduleInformation
        {
            get { return medicationScheduleInformation; }
            set
            {
                medicationScheduleInformation = value;
                OnPropertyChanged();
            }
        }

        private UpdateScheduleModalModel.MedicationReminderInfo medicationReminderInformation;

        public UpdateScheduleModalModel.MedicationReminderInfo MedicationReminderInformation
        {
            get { return medicationReminderInformation; }
            set
            {
                medicationReminderInformation = value;
                OnPropertyChanged();
            }
        }

        private UpdateScheduleModalModel.MedicationPrescriptionInfo medicationPrescriptionInformation;

        public UpdateScheduleModalModel.MedicationPrescriptionInfo MedicationPrescriptionInformation
        {
            get { return medicationPrescriptionInformation; }
            set
            {

                medicationPrescriptionInformation = value;
                OnPropertyChanged();
            }
        }

        private UpdateScheduleModalModel.MedicationPrescriptionDoctor medicationPrescriptionDoctorInformation;

        public UpdateScheduleModalModel.MedicationPrescriptionDoctor MedicationPrescriptionDoctorInformation
        {
            get { return medicationPrescriptionDoctorInformation; }
            set
            {
                medicationPrescriptionDoctorInformation = value;
                OnPropertyChanged();
            }
        }

        public void UpdateMedicine(object parameter)
        {
            try
            {
                if (medicationScheduleInformation.MedicationPeriod is not "As Needed")
                {
                    if (MedicationScheduleInformation.Time_1 != null) Trace.WriteLine(DateTime.ParseExact(MedicationScheduleInformation.Time_1, "HH:mm", CultureInfo.InvariantCulture));
                    if (MedicationScheduleInformation.Time_2 != null) Trace.WriteLine(DateTime.ParseExact(MedicationScheduleInformation.Time_2, "HH:mm", CultureInfo.InvariantCulture));
                    if (MedicationScheduleInformation.Time_3 != null) Trace.WriteLine(DateTime.ParseExact(MedicationScheduleInformation.Time_3, "HH:mm", CultureInfo.InvariantCulture));
                    if (MedicationScheduleInformation.Time_4 != null) Trace.WriteLine(DateTime.ParseExact(MedicationScheduleInformation.Time_4, "HH:mm", CultureInfo.InvariantCulture));
                }

                DAL.UpdateMedication(ServiceLocator.CurrentUser.UserID, MedicationInformation);

                MedicationScheduleInformation.MedicationID = DAL.SearchMedIDByUserIDAndMedName(ServiceLocator.CurrentUser.UserID, MedicationInformation.MedicationName);    // userID here is temporary

                DAL.UpdateSchedule(MedicationScheduleInformation);

                Window window = parameter as Window;
                window.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Time Input. \n\n Exception: " + ex, "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            OnPropertyChanged();

        }

        public void UpdatePrescriptionDoctor()
        {
            DAL.UpdateMedication(ServiceLocator.CurrentUser.UserID, MedicationInformation);

            MedicationScheduleInformation.MedicationID = DAL.SearchMedIDByUserIDAndMedName(ServiceLocator.CurrentUser.UserID, MedicationInformation.MedicationName);    // userID here is temporary
            MedicationPrescriptionInformation.MedicationID = MedicationScheduleInformation.MedicationID;

            DAL.UpdateSchedule(MedicationScheduleInformation);
            DAL.UpdatePrescription(MedicationPrescriptionInformation);

            MedicationPrescriptionDoctorInformation.MedicationPrescriptionID = DAL.SearchPrescID(MedicationPrescriptionInformation.MedicationID);

            DAL.UpdateDoctor(MedicationPrescriptionDoctorInformation);

            OnPropertyChanged();
        }
    }
}
