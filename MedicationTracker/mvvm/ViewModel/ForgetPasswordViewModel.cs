//using Mailjet.Client;
//using MailKit.Net.Smtp;
//using MailKit.Security;
using MedicationTracker.Core;
using MedicationTracker.MVVM.Model;
//using MimeKit;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;


namespace MedicationTracker.MVVM.ViewModel
{
    internal class ForgetPasswordViewModel : ObservableObject
    {
        public RelayCommand SendEmailCommand => new RelayCommand(execute => SendMail());

        public ForgetPasswordViewModel()
        {
            EmailCredential = new ForgetPasswordModel();
        }

        private ForgetPasswordModel emailCredential;
        public ForgetPasswordModel EmailCredential
        {
            get { return emailCredential; }
            set { emailCredential = value; OnPropertyChanged("EmailAddressInput");  }
        }

        public void SendMail()
        {
            RunAsync();
        }
        public void RunAsync()
        {
            string to = "acanonas@mymail.mapua.edu.ph";
            string from = "acanonas@mymail.mapua.edu.ph";
            string subject = "MediTrack";
            string body = "this is a test email. you have forgoten your password due to not taking your memory-allocation medication.";
            MailMessage message = new MailMessage(from, to, subject, body);
            SmtpClient smtp = new SmtpClient("in-v3.mailjet.com");
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("473c936e94650da78c723e31ce02bda9", "7c7cd63a1299381acec1483b3bae1ac2");
            smtp.EnableSsl = true;
            smtp.Send(message);


        }
    }
}
