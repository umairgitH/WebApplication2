using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeTrainningClassLibrary.DAL
{
    public interface IEnrollmentDAL
    {
        bool ApproveOrDeclineTrainning(int enrollmentId, string action);
        bool EnrollToTrainning(int trainningId, int userId, HttpPostedFileBase file);
        Task <(string recipientEmail, string managerName, string trainningName)> GetParameterToSendMailForEmployeeAsync(int userId, int trainningId);
        Task <List<FileStorage>> GetFileDataAsync(int userId);
        Task <FileStorage> GetFileDataToDownloadAsync(int fileId);
        Task <bool> checkForSelectionDoneAsync(int trainningId);
        Task <List<EnrollmentOfEmployee>> DisplayEnrollmentDetailsAsync(string trainningName, int managerId);
        Task <string> GetPrerequisiteNameAsync(int trainningId);
        Task <List<EnrollmentOfEmployee>> GetEmployeeEnrollmentAsync(int trainningId);
        bool ProcessEmployeeEnrollment(int trainningId);
        Task <(string EmployeeEmail, string UserName, string trainningName)> RetrieveParameterToSendMailAsync(int enrollmentId, string action);
    }
}
