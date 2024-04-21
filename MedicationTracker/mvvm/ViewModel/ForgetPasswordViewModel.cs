using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MedicationTracker.MVVM.ViewModel
{
    internal class ForgetPasswordViewModel : ObservableObject
    {
        public RelayCommand SendEmailCommand => new RelayCommand(execute => SendEmail());

        public ForgetPasswordViewModel()
        {
            EmailAddressInput = new ForgetPasswordModel();
        }

        private ForgetPasswordModel emailAddressInput;
        public ForgetPasswordModel EmailAddressInput
        {
            get { return emailAddressInput; }
            set { emailAddressInput = value; OnPropertyChanged("EmailAddressInput"); }
        }


        public void SendEmail()
        {
            // kunwari meron
        }
    }
}
