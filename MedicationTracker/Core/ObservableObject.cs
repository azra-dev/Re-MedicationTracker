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
    internal class ObservableObject : INotifyPropertyChanged, IValueConverter
    {
        // SQL 
        public string connectionString = @"Server=192.168.1.2,1433;Database=MediTrack;User ID=tester;Password=meditrack;Integrated Security=False;Trusted_Connection=False;";

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

        // IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                int hours = timeSpan.Hours;
                string period = hours >= 12 ? "PM" : "AM";
                hours = hours > 12 ? hours - 12 : hours;
                return $"{hours:h}:{timeSpan.Minutes:mm} {period}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); // Not required for one-way binding
        }
    }
}
