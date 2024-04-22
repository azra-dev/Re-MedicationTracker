using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace MedicationTracker.Core
{
    internal class MailJetService
    {
        static void SendMail()
        {
            RunAsync().Wait();
        }
        static async Task RunAsync()
        {
            MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("****************************1234"), Environment.GetEnvironmentVariable("****************************abcd"))
            {
                //Version = ApiVersion.V3_1,
                BaseAdress = "https://api.mailjet.com"
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
              .Property(Send.Messages, new JArray {
         new JObject {
           {
             "From",
             new JObject {
               {
                 "Email",
                 "acanonas@mymail.mapua.edu.ph"
               }, {
                 "Name",
                 "Azra"
               }
             }
           }, {
             "To",
             new JArray {
               new JObject {
                 {
                   "Email",
                   "acanonas@mymail.mapua.edu.ph"
                 }, {
                   "Name",
                   "Azra"
                 }
               }
             }
           }, {
             "Subject",
             "Greetings from Mailjet."
           }, {
             "TextPart",
             "My first Mailjet email"
           }, {
             "HTMLPart",
             "<h3>Dear passenger 1, welcome to <a href='https://www.mailjet.com/'>Mailjet</a>!</h3><br />May the delivery force be with you!"
           }, {
             "CustomID",
             "AppGettingStartedTest"
           }
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
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }
        }
    }
}
