using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class CreateScheduleViewModel : ObservableObject
    {
        public CreateScheduleViewModel()
        {
            MedicationInfoSchedule = new CreateScheduleModel();
        }

        private CreateScheduleModel medicationInfoSchedule;

        public CreateScheduleModel MedicationInfoSchedule
        {
            get { return medicationInfoSchedule; }
            set 
            { 
                medicationInfoSchedule = value; 
                OnPropertyChanged();
            }
        }

    }
}
