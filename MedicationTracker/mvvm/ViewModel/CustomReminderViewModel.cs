using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using MedicationTracker.MVVM.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class CustomReminderViewModel : ObservableObject
    {
        public DataAccessLayer DAL { get; set; }
 
        public RelayCommand UpdateRemInfo => new RelayCommand(execute => UpdateReminderInformation());
        public CustomReminderViewModel() 
        {
            DAL = new DataAccessLayer();
            CustomReminderInfo = new CustomReminderModel();
        }

        private CustomReminderModel customReminderInfo;

        public CustomReminderModel CustomReminderInfo
        {
            get { return customReminderInfo; }
            set 
            { 
                customReminderInfo = value;
                OnPropertyChanged();
            }
        }


        public void UpdateReminderInformation()
        {
            DAL.UpdateReminderInformation(ServiceLocator.CurrentUser.UserID, ServiceLocator.CustomRem.CustomRem_ID,
                CustomReminderInfo.CustomReminderTitle, CustomReminderInfo.CustomReminderMessage);

            OnPropertyChanged("CustomerReminderInfo");
        }


    }
}
