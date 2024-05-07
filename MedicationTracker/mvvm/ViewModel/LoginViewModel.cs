using Mailjet.Client.Resources;
using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using MedicationTracker.MVVM.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Navigation;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class LoginViewModel : ObservableObject
    {
        public DataAccessLayer DAL { get; set; }
        public RelayCommand ValidateCredentialsCmd => new RelayCommand(execute => ValidateLoginCredentials(execute));

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

        public void ValidateLoginCredentials(object parameter)
        {
            if (DAL.ValidateUserLoginCredentials(LoginCredentials.Email, LoginCredentials.Password)) {
                long loggedInUserID = (long)DAL.SearchUserIDByEmail(LoginCredentials.Email);
                ServiceLocator.CurrentUser = new LoggedInUser(loggedInUserID);     // To ensure post-login pages are based on the user that logged in.

                Trace.WriteLine(ServiceLocator.CurrentUser.UserID);

                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                Window window = parameter as Window;
                window.Close();
            }
            
        }
    }

        
}
