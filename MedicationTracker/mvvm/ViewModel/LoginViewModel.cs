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
        public DataAccessLayer DAL { get; set; }
        public RelayCommand ValidateCredentialsCmd => new RelayCommand(execute => ValidateLoginCredentials());

        public LoginViewModel()
        {
            LoginCredentials = new LoginModel();
            DAL = new DataAccessLayer();
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
            
            DAL.ValidateUserLoginCredentials(LoginCredentials.Email, LoginCredentials.Password);
            
        }
    }

        
}
