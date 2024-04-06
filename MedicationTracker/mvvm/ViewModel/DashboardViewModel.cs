using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class DashboardViewModel : ObservableObject
    {
        int navMode = 0; // 0 is default (Home) <- What's this?
        public RelayCommand ReadSchedules => new RelayCommand(execute => ReadMedicationSchedule());

        public long medID = 1;

        public DashboardViewModel()
        {
            DashboardContents = new DashboardModel();

        }

        private DashboardModel dashboardContents;

        public DashboardModel DashboardContents
        {
            get { return dashboardContents; }
            set 
            { 
                dashboardContents = value;
                OnPropertyChanged();
            }
        }

        private void ReadMedicationSchedule()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_JoinMedicationsSchedules", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", GetMediTrackUserID("jptcruz@gmail.com"));

            try
            {
                int MedScheduleID = (int)cmd.ExecuteScalar();
            } 
            catch (Exception ex) 
            {
                MessageBox.Show("Medication ID not found.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);   
            }
        }


    }
}
