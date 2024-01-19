using EmployeeTrainningClassLibrary.DAL;
using EmployeeTrainningClassLibrary.Models;
using EmployeeTrainningClassLibrary.SendNotification;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using static System.Collections.Specialized.BitVector32;

namespace EmployeeTrainningClassLibrary.BusinessLayer
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentDAL _enrollmentDAL;
        private readonly ISendNotification _sendNotification;
        public EnrollmentService(IEnrollmentDAL enrollmentDAL, ISendNotification sendNotification)
        {
            _enrollmentDAL = enrollmentDAL;
            _sendNotification = sendNotification;
        }
        public async Task <List<FileStorage>> GetFileDataAsync(int userId)
        {
            return await _enrollmentDAL.GetFileDataAsync(userId);
        }
        public async Task <FileStorage> GetFileDataToDownloadAsync(int fileId)
        {
           return await _enrollmentDAL.GetFileDataToDownloadAsync(fileId);
        }
        public async Task <List<EnrollmentOfEmployee>> DiplayEnrollmentDetailsAsync(string trainningName, int managerId)
        {
            return await _enrollmentDAL.DisplayEnrollmentDetailsAsync(trainningName, managerId);
        }
        public async Task <string> GetPrerequisiteNameAsync(int trainningId)
        {
            return await _enrollmentDAL.GetPrerequisiteNameAsync(trainningId);
        }
        public async Task <List<EnrollmentOfEmployee>> GetEmployeeEnrollmentAsync(int trainningId)
        {
            return await _enrollmentDAL.GetEmployeeEnrollmentAsync(trainningId);
        }
        public bool ProcessEmployeeEnrollment(int trainningId)
        {
            return  _enrollmentDAL.ProcessEmployeeEnrollment(trainningId);
        }
        public async Task<bool> checkForSelectionDoneAsync(int trainningId)
        {
            return  await _enrollmentDAL.checkForSelectionDoneAsync(trainningId);
        }
        public async Task<bool> ApproveOrDeclineTrainningAsync(int enrollemntId, string action)
        {
            bool result = _enrollmentDAL.ApproveOrDeclineTrainning(enrollemntId, action);
            if (result)
            {
                (string EmployeeEmail, string UserName, string trainningName) = await _enrollmentDAL.RetrieveParameterToSendMailAsync(enrollemntId, action);
                string message = "";
                if (action == "approve")
                {
                    message = "Hello " + UserName + " your application for the " + trainningName + " has been approved by your manager";
                }
                if (action == "reject")
                {
                    message = "Hello " + UserName + " your application for the " + trainningName + " has been declined by your manager";
                }
                string subject = "Updated Status on your enrollment";
                await _sendNotification.SendNotificationAsync(EmployeeEmail, subject, message);
            }
            return result;
        }
        public async Task<bool> IsEmployeeEnrolledAsync(int trainningId, int userId, HttpPostedFileBase file)
        {
            var result = _enrollmentDAL.EnrollToTrainning(trainningId, userId, file);
            if (result)
            {
                (string recipientEmail, string ManagerName, string trainningName) = await _enrollmentDAL.GetParameterToSendMailForEmployeeAsync(userId, trainningId);
                string subject = "Employee Enrollment";
                string message = "Hello " + ManagerName + " an employee under your supervision has applied for the " + trainningName + " trainning.";
                await _sendNotification.SendNotificationAsync(recipientEmail, subject, message);
            }
            return result;
        }
        private async Task SendEmailAsync(string RecipientEmail, string UserName, string trainningName, string action = null)
        {
            string message = "";
            if (action == "approve")
            {
                message = "Hello " + UserName + " your application for the " + trainningName + " has been approved by your manager";
            }
            else if (action == "reject")
            {
                message = "Hello " + UserName + " your application for the " + trainningName + " has been declined by your manager";
            }
            else
            {
                message = "Hello " + UserName + " an employee under your supervision has applied for the " + trainningName + " trainning.";
            }

            string htmlBody = $@"
          <html>
              <head>
                  <title>HTML Email</title>
              </head>
              <body>
                  <p>{message}</p>
              </body>
          </html>";
            string subject = "Update on Your Enrollment Application ";
            await EmailSender.SendEmailAsync(RecipientEmail, subject, htmlBody);
        }
    }
}
