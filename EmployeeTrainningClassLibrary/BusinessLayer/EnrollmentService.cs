using EmployeeTrainningClassLibrary.DAL;
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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentDAL _enrollmentDAL;
        public EnrollmentService(IEnrollmentDAL enrollmentDAL)
        {
            _enrollmentDAL = enrollmentDAL;
        }

        public bool ApproveTrainning()
        {
            throw new NotImplementedException();
        }

        public bool DeclineTrainning()
        {
            throw new NotImplementedException();
        }

        public SqlDataReader GetEnrollment()
        {
            throw new NotImplementedException();
        }
        public bool IsEmployeeEnrolled(int trainningId, int userId, HttpPostedFileBase file)
        {
            return _enrollmentDAL.EnrollToTrainning(trainningId, userId, file);
        }
        public List<FileStorage> GetFileData(int userId)
        {
            return _enrollmentDAL.GetFileData(userId);
        }
        public FileStorage GetFileDataToDownload(int fileId)
        {
           return _enrollmentDAL.GetFileDataToDownload(fileId);
        }
    }
}
