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
        public DataAccessLayer DAL { get; set; }
        public CreateScheduleViewModel()
        {
            DAL = new DataAccessLayer();
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
