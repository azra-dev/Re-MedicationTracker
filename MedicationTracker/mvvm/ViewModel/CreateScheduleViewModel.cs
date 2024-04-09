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
            MedicationInfoAndSchedule = new CreateScheduleModel();
        }

        private CreateScheduleModel medicationInfoAndSchedule;

        public CreateScheduleModel MedicationInfoAndSchedule
        {
            get { return medicationInfoAndSchedule; }
            set 
            {
                medicationInfoAndSchedule = value; 
                OnPropertyChanged();
            }
        }

    }
}
