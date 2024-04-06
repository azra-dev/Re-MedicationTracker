using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class DashboardViewModel : ObservableObject
    {
        int navMode = 0; // 0 is default (Home) <- What's this?
        public RelayCommand ReadSchedules => new RelayCommand(execute => ReadMedicationSchedules());

        public ObservableCollection<DashboardModel.JoinedMedicationSchedule> JoinedMedicationsSchedulesContent { get; set; }

        public DashboardViewModel()
        {
            JoinedMedicationsSchedulesContent = new ObservableCollection<DashboardModel.JoinedMedicationSchedule>();

        }

        private DashboardModel.JoinedMedicationSchedule medicationSchedulesContent;

        public DashboardModel.JoinedMedicationSchedule MedicationSchedulesContent
        {
            get { return medicationSchedulesContent; }
            set 
            {
                medicationSchedulesContent = value;
                OnPropertyChanged();
            }
        }

        private void ReadMedicationSchedules()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_JoinMedicationsSchedules", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", 1);

            try
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read()){
                        MedicationSchedulesContent = new DashboardModel.JoinedMedicationSchedule
                        {
                            MedicationName = reader.GetString(0),
                            MedicationDosageValue = reader.GetDecimal(1).ToString() + " " + reader.GetString(2),
                            MedicationDosageForm = reader.GetString(3),
                            Time_1 = reader.GetTimeSpan(4),
                            Time_2 = reader.IsDBNull(5) ? null : reader.GetTimeSpan(5),
                            Time_3 = reader.IsDBNull(6) ? null : reader.GetTimeSpan(6),
                            Time_4 = reader.IsDBNull(7) ? null : reader.GetTimeSpan(7),
                            MedicationPeriod = reader.GetString(8),
                            MedicationPeriodDate = reader.IsDBNull(9) ? null : reader.GetString(9),
                            MedicationPeriodWeekday = reader.IsDBNull(10) ? reader.GetString(8) : reader.GetString(10)
                        };


                        //MedicationSchedulesContent = schedules;
                        OnPropertyChanged();

                        JoinedMedicationsSchedulesContent.Add(MedicationSchedulesContent);

                        Trace.WriteLine(MedicationSchedulesContent.MedicationDosageForm);
                    }
                    
                }
            } 
            catch (SqlException ex) 
            {
                MessageBox.Show("User ID not found.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);   
            }


        }




    }

}
