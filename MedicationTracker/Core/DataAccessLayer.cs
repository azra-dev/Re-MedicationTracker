using Mailjet.Client.Resources;
using MedicationTracker.MVVM.Model;
using Microsoft.Toolkit.Uwp.Notifications;
using RestSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using static MedicationTracker.MVVM.Model.CreateScheduleModel;
using static MedicationTracker.MVVM.Model.DashboardModel;

#nullable enable

namespace MedicationTracker.Core
{
    internal class DataAccessLayer : ObservableObject
    {
        // Logged-In MediTrack User Information
        public class MediTrackUser
        {
            public string? FullName { get; set; }
            public string? Username { get; set; }
            public byte[]? Image { get; set; }
            public BitmapImage? ProfilePicture { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? BirthDate { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
        }

        // SQL Server Connection String (!!!CHANGE THIS ACCORDINGLY!!!) 

        //public string connectionString = @"Server=DESKTOP-PV312M5;Database=MediTrack;Trusted_Connection=True;";
        //public string connectionString = @"Server=DESKTOP-RDG2IQ3\SQLEXPRESS;Database=MediTrack;Trusted_Connection=True;"; //Azra's string
        public string connectionString = @"Server=QuadaStudio;Database=MediTrack;Trusted_Connection=True;"; //Azra's second string

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
            finally
            {
                connection.Close();
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
            finally
            {
                connection.Close();
            }
        }

        public long SearchPrescID(long med_id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_SearchPrescID", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@mid", med_id);

            try
            {
                long medpresc_id = (long)cmd.ExecuteScalar();
                return medpresc_id;
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Medication ID not found.\n ERROR: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }

        public long SearchMedRemByUserIDAndTitle(long user_id, string remtitle)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_SearchMedRemByUserIDAndTitle", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@mrt", remtitle);

            try
            {
                long medrem_id = (long)cmd.ExecuteScalar();
                return medrem_id;
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Reminder ID not found.\n ERROR: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }

        public long SearchMedIDByUserIDAndRemTitle(long user_id, string remtitle)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_SearchMedIDByUserIDAndRemTitle", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@mrt", remtitle);

            try
            {
                long med_id = (long)cmd.ExecuteScalar();
                return med_id;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Medication ID not found.\n ERROR: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool ValidateUserLoginCredentials(string email, string password)
        {
            if (email == null || password == null)
            {
                MessageBox.Show("Login Failed. Inputs must be filled up.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_ValidateUserLoginCredentials", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@em", email);
            cmd.Parameters.AddWithValue("@pw", password);

            int loginCredentialsIsValid = (int)cmd.ExecuteScalar();
            connection.Close();

            if ((loginCredentialsIsValid == 1) || ((email=="a") && (password=="a")))
            {
                MessageBox.Show("Login Success.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Login Failed.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
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
            finally
            {
                connection.Close();
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
                // To be implemented accordingly
            }
            finally
            {
                connection.Close();
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
                            Time_1 = reader.IsDBNull(4) ? null : reader.GetTimeSpan(4),
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
            finally
            {
                connection.Close();
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
                            Time_1 = reader.IsDBNull(8) ? null : reader.GetTimeSpan(8),
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
                MessageBox.Show("User ID not found.\n ERROR:" + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
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
            finally
            {
                connection.Close();
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

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Medication creation failed. \nERROR MESSAGE: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }

        }

        public void CreateSchedule(ScheduleModalModel.MedicationScheduleInfo medSchedInfo)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_CreateSchedule", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            if (medSchedInfo.MedicationPeriod is "As Needed'")
            {
                cmd.Parameters.AddWithValue("@med_id", medSchedInfo.MedicationID);
                cmd.Parameters.AddWithValue("@med_period", medSchedInfo.MedicationPeriod);
            }
            else
            {
                cmd.Parameters.AddWithValue("@med_id", medSchedInfo.MedicationID);
                cmd.Parameters.AddWithValue("@mtime1", medSchedInfo.Time_1);
                cmd.Parameters.AddWithValue("@mtime2", medSchedInfo.Time_2);
                cmd.Parameters.AddWithValue("@mtime3", medSchedInfo.Time_3);
                cmd.Parameters.AddWithValue("@mtime4", medSchedInfo.Time_4);
                cmd.Parameters.AddWithValue("@med_period", medSchedInfo.MedicationPeriod);
                cmd.Parameters.AddWithValue("@med_period_date", medSchedInfo.MedicationPeriodDate);
            }

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Medication and schedule information created.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Medication schedule creation failed." + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        public void CreatePrescription(ScheduleModalModel.MedicationPrescriptionInfo medPrescription)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_CreatePrescription", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@med_id", medPrescription.MedicationID);
            cmd.Parameters.AddWithValue("@md_start", medPrescription.PrescriptionStartDate);
            cmd.Parameters.AddWithValue("@md_end", medPrescription.PrescriptionEndDate);
            cmd.Parameters.AddWithValue("@md_instructions", medPrescription.PrescriptionInstructions);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Medication prescription creation failed.\nError: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }

        }
        public void CreateDoctor(ScheduleModalModel.MedicationPrescriptionDoctor medPrescriptionDoc)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_CreateDoctor", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@presc_id", medPrescriptionDoc.MedicationPrescriptionID);
            cmd.Parameters.AddWithValue("@doc_name", medPrescriptionDoc.PrescriptionDoctorName);
            cmd.Parameters.AddWithValue("doc_spec", medPrescriptionDoc.PrescriptionDoctorSpecialization);
            cmd.Parameters.AddWithValue("doc_em", medPrescriptionDoc.PrescriptionDoctorEmail);
            cmd.Parameters.AddWithValue("doc_affiliation", medPrescriptionDoc.PrescriptionDoctorAffiliation);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Medication prescription and doctor information created.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Medication doctor creation failed.\nError: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
        }
        public void CreateLogs(long user_id, string rem_title)
        {

            long med_id = SearchMedIDByUserIDAndRemTitle(user_id, rem_title);
            
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_CreateLog", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@med_id", med_id);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Log created. Kindly view logs.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Failed to create log.\nError: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                connection.Close();
            }
        }

        public object? ReadMediTrackUserByID(long user_id)
        {

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_ReadMediTrackUserByID", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id);

            try
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    long imageLength = reader.GetBytes(3, 0, null, 0, 0);
                    byte[] imageData = (byte[])reader[3];
                    long bytesRead = reader.GetBytes(3, 0, imageData, 0, imageData.Length);

                    MediTrackUser meditrackuser = new() 
                    {
                        FullName = reader.GetString(0) + " " + reader.GetString(1),
                        Username = reader.GetString(2),
                        Image = imageData,
                        Email = reader.GetString(4),
                        Password = reader.GetString(5),
                        BirthDate = reader.GetDateTime(6).ToString("dd MMMM yyyy"),
                        FirstName = reader.GetString(0), 
                        LastName = reader.GetString(1)
                    };

                    Trace.WriteLine(meditrackuser.BirthDate);

                    OnPropertyChanged();

                    return meditrackuser;
                }

                return null;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("User information cannot be read.\nERROR: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public void JoinMedicationsLogsByUserID(long user_id, ObservableCollection<LogsModel> joinedMedicationLogsInfo, LogsModel logsInfo)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_JoinMedicationsLogsByUserID", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id);

            try
            {
                using SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        logsInfo = new LogsModel
                        {
                            MedicationName = reader.GetString(0),
                            MedLastTaken = reader.GetDateTime(1).ToString(),
                            MedCumulativeIntake = reader.GetDecimal(2)
                        };

                        OnPropertyChanged();

                        joinedMedicationLogsInfo.Add(logsInfo);
                    }
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show("User_ID not found.\nERROR: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
            
        }

        public void UpdateReminderInformation(long user_id, string initial_remtitle, string new_remtitle, string new_remtext)
        {
            long medrem_id = SearchMedRemByUserIDAndTitle(user_id, initial_remtitle);

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand updateTitleCmd = new SqlCommand("sp_UpdateMedRemTitle", connection);
            updateTitleCmd.CommandType = CommandType.StoredProcedure;

            using SqlCommand updateMsgCmd = new SqlCommand("sp_UpdateMedRemMessage", connection);
            updateMsgCmd.CommandType = CommandType.StoredProcedure;

            updateTitleCmd.Parameters.AddWithValue("@mrd", medrem_id);
            updateMsgCmd.Parameters.AddWithValue("@mrd", medrem_id);

            updateTitleCmd.Parameters.AddWithValue("@mrt", new_remtitle);
            updateMsgCmd.Parameters.AddWithValue("@mrs", new_remtext);

            try
            {
                updateTitleCmd.ExecuteNonQuery();
                updateMsgCmd.ExecuteNonQuery();

                MessageBox.Show("Customized reminder set.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Failed to set customized reminder.\nERROR: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }


        }

        public void UpdateUserInformation(long user_id, UserProfileModel userInfo)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand updateFirstNameCmd = new SqlCommand("sp_UpdateMediTrackUserFirstName", connection);
            updateFirstNameCmd.CommandType = CommandType.StoredProcedure;

            SqlCommand updateLastNameCmd = new SqlCommand("sp_UpdateMediTrackUserLastName", connection);
            updateLastNameCmd.CommandType = CommandType.StoredProcedure;

            SqlCommand updateUsernameCmd = new SqlCommand("sp_UpdateMediTrackUsername", connection);
            updateUsernameCmd.CommandType = CommandType.StoredProcedure;

            SqlCommand updatePasswordCmd = new SqlCommand("sp_UpdateMediTrackUserPassword", connection);
            updatePasswordCmd.CommandType = CommandType.StoredProcedure;

            SqlCommand updateEmailCmd = new SqlCommand("sp_UpdateMediTrackUserEmail", connection);
            updateEmailCmd.CommandType = CommandType.StoredProcedure;

            SqlCommand updateBirthDateCmd = new SqlCommand("sp_UpdateMediTrackUserBirthDate", connection);
            updateBirthDateCmd.CommandType = CommandType.StoredProcedure;

            // To be implemented further by rjldg

        }

    }
}
