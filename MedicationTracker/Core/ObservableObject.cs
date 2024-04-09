using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace MedicationTracker.Core
{
    internal class ObservableObject : INotifyPropertyChanged
    {
        // SQL 

        public string connectionString = @"Server=192.168.1.4,1433;Database=MediTrack;User ID=tester;Password=meditrack;Integrated Security=False;Trusted_Connection=False;";
        //public string connectionString = @"Server=RDG-LENOVO;Database=MediTrack;Trusted_Connection=True;";
        public long GetMediTrackUserID(string email)
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
            catch (SqlException ex)
            {
                MessageBox.Show("Medication ID not found.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
