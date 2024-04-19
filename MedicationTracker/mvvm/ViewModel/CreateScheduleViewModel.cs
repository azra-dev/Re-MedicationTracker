using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class CreateScheduleViewModel : ObservableObject
    {
        public DataAccessLayer DAL { get; set; }

        public ObservableCollection<CreateScheduleModel> JoinedMedicationInfoAndSchedule { get; set; }

        public RelayCommand ReadMedAndSched => new RelayCommand(execute => ReadMedicationsAndSchedules());
        public RelayCommand DeleteMedAndSched => new RelayCommand(execute => DeleteMedicationsAndSchedules(execute));

        public CreateScheduleViewModel()
        {
            DAL = new DataAccessLayer();
            JoinedMedicationInfoAndSchedule = new ObservableCollection<CreateScheduleModel>();
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

        public void ReadMedicationsAndSchedules()
        {
            DAL.JoinMedicationsSchedulesComplete(1, JoinedMedicationInfoAndSchedule, MedicationInfoAndSchedule);    // user_id is temporary
        }

        public void DeleteMedicationsAndSchedules(object parameter)
        {

            CreateScheduleModel medAndSchedToDelete = parameter as CreateScheduleModel;


            if (medAndSchedToDelete != null)
            {
                DAL.DeleteMedication(medAndSchedToDelete);

                JoinedMedicationInfoAndSchedule.Clear();

                ReadMedAndSched.Execute(null);
            }
        }

    }
}
