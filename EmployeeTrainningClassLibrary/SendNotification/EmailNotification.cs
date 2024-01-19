using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrainningClassLibrary.SendNotification
{
    public class EmailNotification : ISendNotification
    {
        public async Task SendNotificationAsync(string recipientEmail, string subject, string message)
        {
            string senderEmail = "TrainningManager@ceridian.com";

            string htmlBody = $@"
          <html>
              <head>
                  <title>HTML Email</title>
              </head>
              <body>
                  <p>{message}</p>
              </body>
          </html>";

            var smtpClient = new SmtpClient("relay.ceridian.com")
            {
                Port = 25,
                EnableSsl = true,
                UseDefaultCredentials = true
            };
            var mailMessage = new MailMessage(senderEmail, recipientEmail)
            {
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
