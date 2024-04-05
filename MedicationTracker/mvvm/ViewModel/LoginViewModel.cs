using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class LoginViewModel : ObservableObject
    {
        // Insert bindings here

        public RelayCommand ValidateCredentialsCmd => new RelayCommand(execute => ValidateLoginCredentials());

        public LoginViewModel()
        {
            LoginCredentials = new LoginModel();
        }

        private LoginModel loginCredentials;

        public LoginModel LoginCredentials
        {
            get { return loginCredentials; }
            set 
            { 
                loginCredentials = value;
                OnPropertyChanged();
            }
        }

        public void ValidateLoginCredentials()
        {
            
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("sp_ValidateUserLoginCredentials", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@em", LoginCredentials.Email);
            cmd.Parameters.AddWithValue("@pw", LoginCredentials.Password);

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
    }

        
}
