using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeTrainningClassLibrary.DAL
{
    public class EnrollmentDAL : IEnrollmentDAL
    {
        private readonly IDataAccessLayer _dataAccessLayer;
        public EnrollmentDAL(IDataAccessLayer dataAccessLayer) { 
            _dataAccessLayer = dataAccessLayer;
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
        public bool EnrollToTrainning(int trainningId, int userId, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    _dataAccessLayer.Connect();

                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@FileName", file.FileName));
                    parameters.Add(new SqlParameter("@ContentType", file.ContentType));
                    parameters.Add(new SqlParameter("@UserId", userId));
                    parameters.Add(new SqlParameter("@TrainingId", trainningId));
                    using (var stream = file.InputStream)
                    using (var reader = new BinaryReader(stream))
                    {
                        parameters.Add(new SqlParameter("@Data", reader.ReadBytes((int)stream.Length)));
                    }

                    const string SQLQUERY = " INSERT INTO FileStorage (FileName, ContentType, Data, TrainingId, UserId) " +
                        "VALUES (@FileName, @ContentType, @Data, @TrainingId, @UserId)";

                    _dataAccessLayer.ExecuteNonQueryData(SQLQUERY, parameters);

                }

                return true;
            }
            catch {
                return false;
            }

        }
        public List<FileStorage> GetFileData(int userId)
        {
            _dataAccessLayer.Connect();
            List<FileStorage> fileList = new List<FileStorage>();
            FileStorage fileStorage = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", userId));

            const string SQLQUERY = "SELECT FileId, FileName, ContentType, Data FROM FileStorage WHERE UserId = @UserId";

            using (var reader = _dataAccessLayer.GetData(SQLQUERY,parameters))
            {
                while (reader.Read())
                {
                    fileStorage = new FileStorage
                    {
                        FileName = reader["FileName"].ToString(),
                        ContentType = reader["ContentType"].ToString(),
                        Data = (byte[])reader["Data"],
                        FileId = (int) reader["FileId"]
                        
                    };
                    fileList.Add(fileStorage);  
                }
            }

            return fileList;

        }

        public FileStorage GetFileDataToDownload(int fileId)
        {
            _dataAccessLayer.Connect();
            FileStorage fileStorage = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FileId", fileId));
            const string SQLQUERY = "SELECT FileName, ContentType, Data FROM FileStorage WHERE FileId = @FileId";
            using (var reader = _dataAccessLayer.GetData(SQLQUERY, parameters))
            {
                while (reader.Read())
                {
                    fileStorage = new FileStorage
                    {
                        FileName = reader["FileName"].ToString(),
                        ContentType = reader["ContentType"].ToString(),
                        Data = (byte[])reader["Data"]
                    };
                }
            }
            return fileStorage;
        }
    }
}
