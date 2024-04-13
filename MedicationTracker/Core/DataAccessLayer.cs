using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static MedicationTracker.MVVM.Model.DashboardModel;

namespace MedicationTracker.Core
{
    internal class DataAccessLayer : ObservableObject
    {
        // SQL Server Connection String

        //public string connectionString = @"Server=192.168.1.4,1433;Database=MediTrack;User ID=tester;Password=meditrack;Integrated Security=False;Trusted_Connection=False;";
        public string connectionString = @"Server=RDG-LENOVO;Database=MediTrack;Trusted_Connection=True;";

        
        // SQL Server Stored Procedures
        public long SearchUserIDByEmail(string email)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_SearchUserIDByEmail", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@email", email);

            try
            {
                long userID = (long)cmd.ExecuteScalar();
                return userID;
            }
            catch (SqlException)
            {
                MessageBox.Show("Medication ID not found.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        public void ValidateUserLoginCredentials(string email, string password)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_ValidateUserLoginCredentials", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@em", email);
            cmd.Parameters.AddWithValue("@pw", password);

            int loginCredentialsIsValid = (int)cmd.ExecuteScalar();

            if (loginCredentialsIsValid == 1)
            {
                MessageBox.Show("Login Success.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Login Failed.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void CreateMediTrackUser(string fn, string ln, string username, string em, string pw, string bd, string path) 
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = new SqlCommand("sp_CreateMediTrackUser", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@fn", fn);
            command.Parameters.AddWithValue("@ln", ln);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@em", em);
            command.Parameters.AddWithValue("@pw", pw);
            command.Parameters.AddWithValue("@bd", bd);

            if (path == "/Images/default_pfp.jpg")
            {
                command.Parameters.AddWithValue("@img_folder_path", null);
            }
            else
            {
                command.Parameters.AddWithValue("@img_folder_path", path);
            }

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("User " + username + " created. Please log in.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException)
            {
                MessageBox.Show("Invalid input in one of the fields.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void JoinsMedicationSchedulesReminders(long user_id, DashboardModel.MedicationReminder MedicationRemindersContent, ObservableCollection<DashboardModel.MedicationReminder> MedicationReminders)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_JoinsMedicationSchedulesReminders", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id); // user_id here is temporary

            try
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MedicationRemindersContent = new DashboardModel.MedicationReminder
                        {
                            MedicationReminderTitle = reader.GetString(0),
                            MedicationReminderMessage = reader.GetString(1)
                        };

                        OnPropertyChanged();

                        MedicationReminders.Add(MedicationRemindersContent);
                    }

                }
            }
            catch (SqlException)
            {

            }
        }

        public void JoinMedicationsSchedules(long user_id, DashboardModel.JoinedMedicationSchedule MedicationSchedulesContent, ObservableCollection<DashboardModel.JoinedMedicationSchedule> JoinedMedicationsSchedulesContent)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_JoinMedicationsSchedules", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id); // user_id here is temporary

            try
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
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
                            MedicationPeriodWeekday = reader.IsDBNull(10) ? null : "every " + reader.GetString(10)
                        };


                        OnPropertyChanged();

                        JoinedMedicationsSchedulesContent.Add(MedicationSchedulesContent);

                    }

                }
            }
            catch (SqlException)
            {
                MessageBox.Show("User ID not found.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void JoinMedicationsSchedulesComplete(long user_id, ObservableCollection<CreateScheduleModel> JoinedMedicationInfoAndSchedule, CreateScheduleModel MedicationInfoAndSchedule)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_JoinMedicationsSchedulesComplete", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id);

            try
            {
                using SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MedicationInfoAndSchedule = new CreateScheduleModel
                        {
                            MedicationName = reader.GetString(0),
                            MedicationDosageValue = reader.GetDecimal(1).ToString() + " " + reader.GetString(2),
                            MedicationDosageForm = reader.GetString(3),
                            MedicationTotalAmountValue = reader.GetDecimal(4).ToString() + " " + reader.GetString(5),
                            MedicationExpirationDate = reader.IsDBNull(6) ? null : reader.GetDateTime(6).ToString(),
                            MedicationNotes = reader.IsDBNull(7) ? null : reader.GetString(7),
                            MedicationIsPrescribed = reader.GetBoolean(8),
                            Time_1 = reader.GetTimeSpan(9),
                            Time_2 = reader.IsDBNull(10) ? null : reader.GetTimeSpan(10),
                            Time_3 = reader.IsDBNull(11) ? null : reader.GetTimeSpan(11),
                            Time_4 = reader.IsDBNull(12) ? null : reader.GetTimeSpan(12),
                            MedicationPeriod = reader.GetString(13),
                            MedicationPeriodWeekday = reader.IsDBNull(15) ? null : "every " + reader.GetString(15),
                            MedicationID = reader.GetInt64(16)

                        };

                        OnPropertyChanged();

                        Trace.WriteLine(MedicationInfoAndSchedule.MedicationID);

                        JoinedMedicationInfoAndSchedule.Add(MedicationInfoAndSchedule);
                    }
                }
            }
            catch(SqlException)
            {
                MessageBox.Show("User ID not found.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteMedication(CreateScheduleModel medicationInfoAndSched) 
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_DeleteMedication", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@med_id", medicationInfoAndSched.MedicationID);

            try
            {
                var option = MessageBox.Show("Are you sure you want to delete " + medicationInfoAndSched.MedicationName + "?",
                    "Delete Medication", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (option == MessageBoxResult.Yes)
                {
                    cmd.ExecuteNonQuery();


                }
                
            }
            catch (SqlException)
            {
                MessageBox.Show("Med ID not found.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    


}
