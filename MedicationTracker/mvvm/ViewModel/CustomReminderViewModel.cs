using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class CustomReminderViewModel : ObservableObject
    {

        public RelayCommand UpdateRemInfo => new RelayCommand(execute => UpdateReminderInformation());
        public CustomReminderViewModel() 
        {
            CustomerReminderInfo = new CustomReminderModel();
        }

        private CustomReminderModel customreminderinfo;

        public CustomReminderModel CustomerReminderInfo
        {
            get { return customreminderinfo; }
            set 
            { 
                customreminderinfo = value;
                OnPropertyChanged();
            }
        }

        public void UpdateReminderInformation()
        {
            // TO BE IMPLEMENTED BY RJLDG WHEN CONNECTED TO CUSTOMREMINDER MODAL
        }


    }
}
