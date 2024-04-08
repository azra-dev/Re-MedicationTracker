﻿using MedicationTracker.Core;
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

namespace MedicationTracker.MVVM.ViewModel
{
    internal class RegisterViewModel : ObservableObject
    {

        public RelayCommand RegisterUser => new RelayCommand(execute => RegisterMediTrackUser(), canexecute => RegisterCredentials != null);
        public RelayCommand SetUserPFP => new RelayCommand(execute => SetMediTrackUserProfilePicture());


        public RegisterViewModel() 
        {
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

        public void RegisterMediTrackUser()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = new SqlCommand("sp_CreateMediTrackUser", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@fn", RegisterCredentials.FirstName);
            command.Parameters.AddWithValue("@ln", RegisterCredentials.LastName);
            command.Parameters.AddWithValue("@username", RegisterCredentials.Username);
            command.Parameters.AddWithValue("@em", RegisterCredentials.EmailAddress);
            command.Parameters.AddWithValue("@pw", RegisterCredentials.Password);
            command.Parameters.AddWithValue("@bd", RegisterCredentials.BirthDate);

            if (!string.IsNullOrEmpty(RegisterCredentials.ProfilePicturePath))
            {
                command.Parameters.AddWithValue("@img_folder_path", RegisterCredentials.ProfilePicturePath);
            } else
            {
                command.Parameters.AddWithValue("@img_folder_path", null);
            }

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                MessageBox.Show("One of the fields is invalid.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}