using Mailjet.Client.Resources;
using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.Windows.Shapes;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class CreateScheduleViewModel : ObservableObject
    {
        public DataAccessLayer DAL { get; set; }

        public ObservableCollection<CreateScheduleModel> JoinedMedicationInfoAndSchedule { get; set; }
        public RelayCommand ReadUserInfo => new RelayCommand(execute => ReadMediTrackUserInformation());
        public RelayCommand ReadMedAndSched => new RelayCommand(execute => ReadMedicationsAndSchedules());
        public RelayCommand DeleteMedAndSched => new RelayCommand(execute => DeleteMedicationsAndSchedules(execute));

        public CreateScheduleViewModel()
        {
            DAL = new DataAccessLayer();
            MediTrackUserInfo = new DataAccessLayer.MediTrackUser();
            JoinedMedicationInfoAndSchedule = new ObservableCollection<CreateScheduleModel>();
            MedicationInfoAndSchedule = new CreateScheduleModel();
        }

        private CreateScheduleModel medicationInfoAndSchedule;
        public CreateScheduleModel MedicationInfoAndSchedule
        {
            get { return medicationInfoAndSchedule; }
            set 
            { 
                medicationInfoAndSchedule = value;
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
            MediTrackUserInfo = (DataAccessLayer.MediTrackUser)DAL.ReadMediTrackUserByID(ServiceLocator.CurrentUser.UserID);    // user_id here is temporary

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


        public void ReadMedicationsAndSchedules()
        {
            DAL.JoinMedicationsSchedulesComplete(ServiceLocator.CurrentUser.UserID, JoinedMedicationInfoAndSchedule, MedicationInfoAndSchedule);    // user_id is temporary
        }

        public void DeleteMedicationsAndSchedules(object parameter)
        {

            CreateScheduleModel medAndSchedToDelete = parameter as CreateScheduleModel;


            if (medAndSchedToDelete != null)
            {
                DAL.DeleteMedication(medAndSchedToDelete);

                JoinedMedicationInfoAndSchedule.Clear();

                ReadMedAndSched.Execute(null);
            }
        }

        public void RefreshMedicationsAndSchedulesInfo()
        {
            JoinedMedicationInfoAndSchedule.Clear();

            ReadMedAndSched.Execute(null);
        }

    }
}
