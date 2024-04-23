using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class LogsViewModel : ObservableObject
    {
        public DataAccessLayer DAL {  get; set; }

        public RelayCommand CreateLogInfo => new RelayCommand(execute => CreateLogs());

        public LogsViewModel()
        {
            DAL = new DataAccessLayer();
            LogsInformation = new LogsModel();

        }

        private LogsModel logsInformation;

        public LogsModel LogsInformation
        {
            get { return logsInformation; }
            set
            {
                logsInformation = value;
                OnPropertyChanged();
            }
        }

        public void CreateLogs()
        {
          //Logic function for CreateLogs, send help pls. 
        }

        }

    }
