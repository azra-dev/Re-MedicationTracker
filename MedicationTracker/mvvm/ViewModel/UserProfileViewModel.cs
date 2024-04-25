using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class UserProfileViewModel : ObservableObject
    {
        public DataAccessLayer DAL { get; set; }

        // Add relaycommand for the confirm edits button
        public RelayCommand SetUserPFP => new RelayCommand(execute => SetMediTrackUserProfilePicture());
        public UserProfileViewModel() 
        { 
            DAL = new DataAccessLayer();
            NewUserInformation = new UserProfileModel();
            CurrentUserInfo = new DataAccessLayer.MediTrackUser();
            CurrentUserInfo = (DataAccessLayer.MediTrackUser)DAL.ReadMediTrackUserByID(1);  // user_id here is temporary

            byte[] imageData = CurrentUserInfo.Image;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();

            CurrentUserInfo.ProfilePicture = image;
            CurrentUserBirthDate = CurrentUserInfo.BirthDate;

            OnPropertyChanged("CurrentUserInfo");

        }

        private UserProfileModel newUserInformation;

        public UserProfileModel NewUserInformation
        {
            get { return newUserInformation; }
            set 
            {
                newUserInformation = value;
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

        private string currentUserBirthDate;

        public string CurrentUserBirthDate
        {
            get { return currentUserBirthDate; }
            set
            {
                currentUserBirthDate = value;
                OnPropertyChanged();
            }
        }

        private DataAccessLayer.MediTrackUser currentUserInfo;

        public DataAccessLayer.MediTrackUser CurrentUserInfo
        {
            get { return currentUserInfo; }
            set 
            {
                currentUserInfo = value; 
                OnPropertyChanged();
            }
        }


        public void SetMediTrackUserProfilePicture()
        {
            OpenFileDialog fileDialog = new()
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.bmp;*.png)|*.jpg;*.jpeg;*.bmp;*.png;",
                Title = "Select a New Profile Picture."
            };

            bool? success = fileDialog.ShowDialog();

            if (success == true)
            {
                string fullPath = fileDialog.FileName;
                NewUserInformation.ProfilePicturePath = fullPath;
                CurrentUserInfo.ProfilePicture = null;

                OnPropertyChanged("NewUserInformation");
                OnPropertyChanged("CurrentUserInfo");

            }

        }

        public void UpdateUserInformation()
        {
            if (ConfirmPasswordInput != NewUserInformation.Password)
            {
                MessageBox.Show("Passwords do not match.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                DAL.UpdateUserInformation(1, newUserInformation);
            }


        }

    }
}
