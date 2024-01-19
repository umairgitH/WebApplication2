using EmployeeTrainningClassLibrary.Models;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeTrainningClassLibrary.BusinessLayer
{
    public interface IEnrollmentService
    {
        Task <List<FileStorage>> GetFileDataAsync(int userId);
        Task <FileStorage> GetFileDataToDownloadAsync(int fileId);
        Task<bool> checkForSelectionDoneAsync(int trainningId);
        Task <List<EnrollmentOfEmployee>> DiplayEnrollmentDetailsAsync(string trainningName, int managerId);
        Task <string> GetPrerequisiteNameAsync(int trainningId);
        Task <List<EnrollmentOfEmployee>> GetEmployeeEnrollmentAsync(int trainningId);
        bool ProcessEmployeeEnrollment(int trainningId);
        Task <bool> ApproveOrDeclineTrainningAsync(int enrollemntId, string action);
        Task<bool> IsEmployeeEnrolledAsync(int trainningId, int userId, HttpPostedFileBase file);
    }
}
