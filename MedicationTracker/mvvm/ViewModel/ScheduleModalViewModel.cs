using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class ScheduleModalViewModel : ObservableObject
    {
        public DataAccessLayer DAL { get; set; }
        public RelayCommand CreateMed => new RelayCommand(execute => CreateMedication());
        public ScheduleModalViewModel()
        {
            MedicationInformation = new ScheduleModalModel.MedicationInfo();
            MedicationScheduleInformation = new ScheduleModalModel.MedicationScheduleInfo();
            MedicationReminderInformation = new ScheduleModalModel.MedicationReminderInfo();
            MedicationPrescriptionInformation = new ScheduleModalModel.MedicationPrescriptionInfo();
            MedicationPrescriptionDoctorInformation = new ScheduleModalModel.MedicationPrescriptionDoctor();
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
        public void CreateMedication()
        {
            DAL.CreateMedication(MedicationInformation);

            MedicationScheduleInformation.MedicationID = DAL.SearchMedIDByUserIDAndMedName(1, MedicationInformation.MedicationName);

            if(MedicationReminderInformation.MedicationReminderMessage == null && MedicationReminderInformation.MedicationReminderTitle == null)
            {
                DAL.CreateSchedule(MedicationScheduleInformation);
            } else
            {
                DAL.CreateSchedule(MedicationScheduleInformation);
                // To be implemented by Rance
                
            }
        }



    }
}
