﻿using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class ScheduleModalViewModel : ObservableObject
    {
        public DataAccessLayer DAL { get; set; }
        public RelayCommand CreateMed => new RelayCommand(execute => CreateMedicine());
        public RelayCommand CreatePrescDoc => new RelayCommand(execute => CreatePrescriptionAndDoctor());
        public ScheduleModalViewModel()
        {
            DAL = new DataAccessLayer();
            MedicationInformation = new ScheduleModalModel.MedicationInfo();
            MedicationScheduleInformation = new ScheduleModalModel.MedicationScheduleInfo();
            MedicationReminderInformation = new ScheduleModalModel.MedicationReminderInfo();
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


        public void CreateMedicine()
        {
            
            DAL.CreateMedication(1, MedicationInformation);     // userID here is temporary

            MedicationScheduleInformation.MedicationID = DAL.SearchMedIDByUserIDAndMedName(1, MedicationInformation.MedicationName);    // userID here is temporary

            DAL.CreateSchedule(MedicationScheduleInformation);
        }

        public void CreatePrescriptionAndDoctor()
        {
            // Godwyn will implement this
        }



    }
}
