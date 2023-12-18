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
        bool IsEmployeeEnrolled(int trainningId, int userId, HttpPostedFileBase file);
        SqlDataReader GetEnrollment();
        bool ApproveTrainning();
        bool DeclineTrainning();
        List<FileStorage> GetFileData(int userId);
        FileStorage GetFileDataToDownload(int fileId);
    }
}
