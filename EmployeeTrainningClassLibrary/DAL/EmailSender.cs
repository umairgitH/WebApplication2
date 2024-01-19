using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrainningClassLibrary.DAL
{
    public static class EmailSender
    {
        public static async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            string senderEmail = "TrainningManager@ceridian.com";
            
            var smtpClient =  new SmtpClient("relay.ceridian.com")
            {
                Port = 25,
                EnableSsl = true,
                UseDefaultCredentials = true
            };
            var mailMessage = new MailMessage(senderEmail, recipientEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
