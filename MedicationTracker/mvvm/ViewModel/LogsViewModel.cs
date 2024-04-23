using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class LogsViewModel : ObservableObject
    {
        public DataAccessLayer DAL {  get; set; }

        public RelayCommand ReadUserInfo => new RelayCommand(execute => ReadMediTrackUserInformation());
        public RelayCommand CreateLogInfo => new RelayCommand(execute => CreateLogs());

        public LogsViewModel()
        {
            DAL = new DataAccessLayer();
            MediTrackUserInfo = new DataAccessLayer.MediTrackUser();
            LogsInformation = new LogsModel();
        }

        private LogsModel logsInformation;

        public LogsModel LogsInformation
        {
            get { return logsInformation; }
            set
            {
                logsInformation = value;
                OnPropertyChanged();
            }
        }

        private DataAccessLayer.MediTrackUser meditrackuserinfo;

        public DataAccessLayer.MediTrackUser MediTrackUserInfo
        {
            get { return meditrackuserinfo; }
            set 
            { 
                meditrackuserinfo = value; 
                OnPropertyChanged();
            }
        }

        public void ReadMediTrackUserInformation()
        {
            MediTrackUserInfo = (DataAccessLayer.MediTrackUser)DAL.ReadMediTrackUserByID(1);    // user_id here is temporary

            byte[] imageData = MediTrackUserInfo.Image;
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

            MediTrackUserInfo.ProfilePicture = image;

            OnPropertyChanged("MediTrackUserInfo");
        }

        public void CreateLogs()
        {
            // To be implemented by rjldg
        }

        }

    }
