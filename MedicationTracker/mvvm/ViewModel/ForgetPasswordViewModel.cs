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
        public RelayCommand SendEmailCommand => new RelayCommand(execute => SendEmail());

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

        public void SendEmail()
        {
            _SendEmail().Wait();
        }

        static async Task _SendEmail()
        {
            // set up mail content
            MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("473c936e94650da78c723e31ce02bda9"), Environment.GetEnvironmentVariable("7c7cd63a1299381acec1483b3bae1ac2"));
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
               .Property(Send.FromEmail, "meditrack@mailjet.com")
               .Property(Send.FromName, "MediTrack Mail Service")
               .Property(Send.Subject, "Password Recovery")
               .Property(Send.TextPart, "You forgot your password because you did not use our MediTrack services and hence, you have not took your memory-allocation medication.\r\n " +
                                        "Do not want to forget this message? You will forgot you read this email anyway and take a look again after a while ")
               .Property(Send.HtmlPart, "<html>\r\n             <body>\r\n                 <p><b>You forgot your password</b> because <i>you did not use our MediTrack services</i> and hence, you have not took your memory-allocation medication.\r\n                 Do not want to forget this message? You will forgot you read this email anyway and take a look again after a while :)</p>\r\n             </body>\r\n         </html>")
               .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", "aztx.256@gmail.com"}
                 }
                   });
            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }


        }
    }
}
