using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace AppointmentScheduling.Utility
{
    public class EmailSender : IEmailSender
    {
        public async Task  SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient("5fa42d2c02d7763828db7de59fc3b383", "0f0112d71b8f902c374ea565cc767d6f")
            {
                
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "email@gmail.com")
            .Property(Send.FromName, "Appointment Scheduler")
            .Property(Send.Subject, subject)
            .Property(Send.HtmlPart, htmlMessage)
            .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", email}
                 }
                });
            MailjetResponse response = await client.PostAsync(request);
        }
    }
}
