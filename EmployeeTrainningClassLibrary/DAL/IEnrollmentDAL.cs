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
        SqlDataReader GetEnrollment();
        bool ApproveTrainning();
        bool DeclineTrainning();
        bool EnrollToTrainning(int trainningId, int userId, HttpPostedFileBase file);
        List<FileStorage> GetFileData(int userId);
        FileStorage GetFileDataToDownload(int fileId);
    }
}
