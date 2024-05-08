using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class RegisterViewModel : ObservableObject
    {

        public DataAccessLayer DAL { get; set; }
        public RelayCommand RegisterUser => new RelayCommand(execute => RegisterMediTrackUser(execute), canexecute => RegisterCredentials != null);
        public RelayCommand SetUserPFP => new RelayCommand(execute => SetMediTrackUserProfilePicture());


        public RegisterViewModel() 
        {
            DAL = new DataAccessLayer();
            RegisterCredentials = new RegisterModel();
            RegisterCredentials.ProfilePicturePath = "/Images/default_pfp.jpg";
        }

        private RegisterModel registerCredentials;

        public RegisterModel RegisterCredentials
        {
            get { return registerCredentials; }
            set 
            { 
                registerCredentials = value;
                OnPropertyChanged();
            }
        }

        private string confirmPasswordInput;

        public string ConfirmPasswordInput
        {
            get { return confirmPasswordInput; }
            set 
            { 
                confirmPasswordInput = value; 
                OnPropertyChanged();
            }
        }


        public void SetMediTrackUserProfilePicture()
        {
            OpenFileDialog fileDialog = new()
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.bmp;*.png)|*.jpg;*.jpeg;*.bmp;*.png;",
                Title = "Select a Profile Picture."
            };

            bool? success = fileDialog.ShowDialog();

            if (success == true)
            {
                string fullPath = fileDialog.FileName;
                RegisterCredentials.ProfilePicturePath = fullPath;

                OnPropertyChanged("RegisterCredentials");

            }

        }

        public void RegisterMediTrackUser(object parameter)
        {
            if (ConfirmPasswordInput != RegisterCredentials.Password)
            {
                MessageBox.Show("Passwords do not match.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                DAL.CreateMediTrackUser(RegisterCredentials.FirstName, RegisterCredentials.LastName, RegisterCredentials.Username,
                    RegisterCredentials.EmailAddress, RegisterCredentials.Password, RegisterCredentials.BirthDate, RegisterCredentials.ProfilePicturePath);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window currentWindow = parameter as Window;
                currentWindow.Close();
            }
            
            
        }

    }
}
