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
                MessageBox.Show("User ID not found.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        public long SearchMedIDByUserIDAndMedName(long user_id, string med_name)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_SearchMedIDByUserIDAndMedName", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@med_name", med_name);

            try
            {
                long medID = (long)cmd.ExecuteScalar();
                return medID;
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
                            MedicationIsPrescribed = reader.GetBoolean(7),
                            Time_1 = reader.GetTimeSpan(8),
                            Time_2 = reader.IsDBNull(9) ? null : reader.GetTimeSpan(9),
                            Time_3 = reader.IsDBNull(10) ? null : reader.GetTimeSpan(10),
                            Time_4 = reader.IsDBNull(11) ? null : reader.GetTimeSpan(11),
                            MedicationPeriod = reader.GetString(12),
                            MedicationPeriodWeekday = reader.IsDBNull(14) ? null : "every " + reader.GetString(14),
                            MedicationID = reader.GetInt64(15)

                        };

                        OnPropertyChanged();

                        Trace.WriteLine(MedicationInfoAndSchedule.MedicationID);

                        JoinedMedicationInfoAndSchedule.Add(MedicationInfoAndSchedule);
                    }
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show("User ID not found." + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public void CreateMedication(long user_id, ScheduleModalModel.MedicationInfo medInfo)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_CreateMedication", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            int isPrescribed = (medInfo.MedicationIsPrescribed == true) ? 1 : 0;

            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@md_name", medInfo.MedicationName);
            cmd.Parameters.AddWithValue("@md_dose", medInfo.MedicationDosageValue);
            cmd.Parameters.AddWithValue("@md_doseform", medInfo.MedicationDosageForm);
            cmd.Parameters.AddWithValue("@md_doseunit", medInfo.MedicationDosageUnit);
            cmd.Parameters.AddWithValue("@md_totalamt", medInfo.MedicationTotalAmount);
            cmd.Parameters.AddWithValue("@md_totalamt_unit", medInfo.MedicationTotalAmountUnit);
            cmd.Parameters.AddWithValue("@md_exp", medInfo.MedicationExpirationDate);
            cmd.Parameters.AddWithValue("@md_isprescribed", isPrescribed);

            Trace.WriteLine(user_id);
            Trace.WriteLine(medInfo.MedicationName);
            Trace.WriteLine(medInfo.MedicationDosageValue);
            Trace.WriteLine(medInfo.MedicationDosageForm);
            Trace.WriteLine(medInfo.MedicationDosageUnit);
            Trace.WriteLine(medInfo.MedicationTotalAmount);
            Trace.WriteLine(medInfo.MedicationTotalAmountUnit);
            Trace.WriteLine(medInfo.MedicationExpirationDate);
            Trace.WriteLine(isPrescribed);

            try
            {
                cmd.ExecuteNonQuery();
                Trace.WriteLine("Med created");
            }
            catch(SqlException)
            {
                MessageBox.Show("Medication information creation failed.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void CreateSchedule(ScheduleModalModel.MedicationScheduleInfo medSchedInfo)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_CreateSchedule", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@med_id", medSchedInfo.MedicationID);
            cmd.Parameters.AddWithValue("@mtime1", medSchedInfo.Time_1);
            cmd.Parameters.AddWithValue("@mtime2", medSchedInfo.Time_2);
            cmd.Parameters.AddWithValue("@mtime3", medSchedInfo.Time_3);
            cmd.Parameters.AddWithValue("@mtime4", medSchedInfo.Time_4);
            cmd.Parameters.AddWithValue("@med_period", medSchedInfo.MedicationPeriod);
            cmd.Parameters.AddWithValue("@med_period_date", medSchedInfo.MedicationPeriodDate);

            Trace.WriteLine(medSchedInfo.MedicationID);
            Trace.WriteLine(medSchedInfo.Time_1);
            Trace.WriteLine(medSchedInfo.Time_2);
            Trace.WriteLine(medSchedInfo.Time_3);
            Trace.WriteLine(medSchedInfo.Time_4);
            Trace.WriteLine(medSchedInfo.MedicationPeriod);
            Trace.WriteLine(medSchedInfo.MedicationPeriodDate);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Medication and Schedule created.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Medication schedule creation failed." + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

    


}
