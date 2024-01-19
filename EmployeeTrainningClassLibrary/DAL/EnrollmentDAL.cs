using EmployeeTrainningClassLibrary.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Net.WebRequestMethods;

namespace EmployeeTrainningClassLibrary.DAL
{
    public class EnrollmentDAL : IEnrollmentDAL
    {
        private readonly IDataAccessLayer _dataAccessLayer;
        public EnrollmentDAL(IDataAccessLayer dataAccessLayer) { 
            _dataAccessLayer = dataAccessLayer;
        }
        public bool ApproveOrDeclineTrainning(int enrollmentId, string action)
        {
            _dataAccessLayer.Connect();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EnrollementId", enrollmentId));
            if (action == "approve")
            {
                const string SQLQUERY = "UPDATE Enrollment " +
                "SET Status = 'Active' " +
                "WHERE EnrollementId = @EnrollementId";
                _dataAccessLayer.ExecuteNonQueryData(SQLQUERY, parameters);
                //await SendEmail(enrollmentId, action);
            }
            else if (action == "reject")
            {
                const string SQLQUERY = "UPDATE Enrollment " +
                "SET Status = 'Inactive' " +
                "WHERE EnrollementId = @EnrollementId";
                _dataAccessLayer.ExecuteNonQueryData(SQLQUERY, parameters);
                List<SqlParameter> Emailparameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@EnrollementId", enrollmentId));
                //await SendEmail(enrollmentId, action);
            }
            return true;           
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
                    parameters.Add(new SqlParameter("@EnrollmentDate", DateTime.Now));
                    using (var stream = file.InputStream)
                    using (var reader = new BinaryReader(stream))
                    {
                        parameters.Add(new SqlParameter("@Data", reader.ReadBytes((int)stream.Length)));
                    }
                    const string SQLQUERY = "INSERT INTO Enrollment(EnrollmentDate,TrainingId,UserId)" +
                        " VALUES(@EnrollmentDate,@TrainingId,@UserId); " +
                        "DECLARE @EnrollmentId INT =  SCOPE_IDENTITY();" +
                        "INSERT INTO FileStorage (FileName, ContentType, Data, EnrollementId)" +
                        "VALUES (@FileName, @ContentType, @Data, @EnrollmentId)";

                    _dataAccessLayer.ExecuteNonQueryData(SQLQUERY, parameters);

                }
                return true;
            }
            finally
            {
                _dataAccessLayer.Dispose();
            }
            
        }
        public async Task <(string recipientEmail, string managerName, string trainningName)> GetParameterToSendMailForEmployeeAsync(int userId, int trainningId)
        {
            _dataAccessLayer.Connect() ;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", userId));
            parameters.Add(new SqlParameter("@trainningId", trainningId));
            const string SQLQUERY = "DECLARE @managerId INT " +
                "SET @ManagerId = (select ManagerId FROM User_Details WHERE UserId=@UserId); " +
                "SELECT TrainingName FROM Training_Details WHERE TrainingId = @trainningId " +
                "SELECT Email, FirstName, LastName FROM User_Details WHERE UserId = @ManagerId";
            string managerName = "";
            string trainningName = "";
            string recipientEmail = "";
            using(var reader = await _dataAccessLayer.GetData(SQLQUERY, parameters))
            {
                if (reader.Read())
                {
                    trainningName = reader["TrainingName"].ToString();
                }
                reader.NextResult();
                if (reader.Read())
                {
                    managerName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                    recipientEmail = reader["Email"].ToString();
                }
            }
            return (recipientEmail,managerName,trainningName);
        }
        public async Task <List<FileStorage>> GetFileDataAsync(int userId)
        {
            _dataAccessLayer.Connect();
            List<FileStorage> fileList = new List<FileStorage>();
            FileStorage fileStorage = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", userId));

            const string SQLQUERY = "SELECT FileId, FileName, ContentType, Data FROM FileStorage WHERE UserId = @UserId";

            using (var reader =  await _dataAccessLayer.GetData(SQLQUERY,parameters))
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
        public async Task <FileStorage> GetFileDataToDownloadAsync(int fileId)
        {
            _dataAccessLayer.Connect();
            FileStorage fileStorage = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FileId", fileId));
            const string SQLQUERY = "SELECT FileName, ContentType, Data FROM FileStorage WHERE FileId = @FileId";
            using (var reader = await _dataAccessLayer.GetData(SQLQUERY, parameters))
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
        public async Task <List<EnrollmentOfEmployee>> DisplayEnrollmentDetailsAsync(string trainningName, int managerId)
        {
            _dataAccessLayer.Connect();
            List<EnrollmentOfEmployee> employeeEnrollmentList = new List<EnrollmentOfEmployee>();
            EnrollmentOfEmployee enrollmentOfEmployee = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@trainningName", trainningName));
            parameters.Add(new SqlParameter("@managerId", managerId));
            const string SQLQUERY = "SELECT e.EnrollementId, e.Status, u.FirstName, u.LastName, f.FileId " +
                " FROM User_Details u"+
                " INNER JOIN Enrollment e ON u.UserId = e.UserId"+
                " INNER JOIN FileStorage f ON e.EnrollementId = f.EnrollementId" +
                " INNER JOIN Training_Details t ON e.TrainingId = t.TrainingId"+
                " WHERE t.TrainingName = @trainningName AND u.ManagerId = @managerId";
            using(var reader =await _dataAccessLayer.GetData(SQLQUERY, parameters))
            {
                while (reader.Read())
                {
                    enrollmentOfEmployee = new EnrollmentOfEmployee
                    {
                        EnrollementId = (short)reader["EnrollementId"],
                        Status = reader["Status"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        FileId = (byte)reader["FileId"]
                    };
                    employeeEnrollmentList.Add(enrollmentOfEmployee);   
                }
            }
            return employeeEnrollmentList;
        }
        public async Task <string> GetPrerequisiteNameAsync(int trainningId)
        {
            _dataAccessLayer.Connect();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@trainningId", trainningId));
            const string SQLQUERY = "SELECT PrerequisiteName FROM Trainning_Prerequisite WHERE TrainingId =@trainningId ";
            string requisiteName = "";
            using(var reader = await _dataAccessLayer.GetData(SQLQUERY,parameters))
            {
                if (reader.Read())
                {
                    requisiteName = reader["PrerequisiteName"].ToString();
                }
            }
            return requisiteName;
        }
        public async Task <bool> checkForSelectionDoneAsync(int trainningId)
        {
            _dataAccessLayer.Connect();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@trainningId", trainningId));
            const string SQLQUERY = "SELECT * FROM Selected_Candidate WHERE TrainingId = @trainningId";
            using(var reader = await _dataAccessLayer.GetData(SQLQUERY, parameters))
            {
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task <List<EnrollmentOfEmployee>> GetEmployeeEnrollmentAsync(int trainningId)
        {
            _dataAccessLayer.Connect();
            List<EnrollmentOfEmployee> employeeEnrollmentList = new List<EnrollmentOfEmployee>();
            EnrollmentOfEmployee enrollmentOfEmployee = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@trainningId", trainningId));
            const string SQLQUERY = "SELECT U.FirstName, U.LastName, U.Department, E.EnrollementId, E.Status" +
                " FROM User_Details U" +
                " INNER JOIN Enrollment E ON U.UserId = E.UserId"+
                " INNER JOIN Training_Details T ON E.TrainingId = T.TrainingId" +
                " WHERE E.Status = 'Active' AND T.TrainingId = @trainningId;";
            
            using (var reader = await _dataAccessLayer.GetData(SQLQUERY, parameters))
            {
                while (reader.Read())
                {
                    enrollmentOfEmployee = new EnrollmentOfEmployee
                    {
                        EnrollementId = (short)reader["EnrollementId"],
                        Status = reader["Status"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Department = reader["Department"].ToString()
                    };
                    employeeEnrollmentList.Add(enrollmentOfEmployee); 
                }
                return employeeEnrollmentList;
            }
        }
        public bool ProcessEmployeeEnrollment(int trainningId)
        {
            try
            {
                _dataAccessLayer.Connect();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@trainningId", trainningId));
                const string SQLQUERY = "DECLARE @priorityDept varchar(16); " +
                    "SET @priorityDept = (SELECT PriorityDepartment FROM Training_Details WHERE TrainingId =@trainningId); " +
                    "INSERT INTO Selected_Candidate(TrainingId, UserId, EnrollementId) " +
                    "SELECT TOP (SELECT Capacity FROM Training_Details WHERE TrainingId = @trainningId) " +
                    "e.TrainingId, e.UserId, e.EnrollementId " +
                    "FROM Enrollment e " +
                    "INNER JOIN Training_Details t ON e.TrainingId = t.TrainingId " +
                    "INNER JOIN User_Details u ON e.UserId = u.UserId " +
                    "WHERE e.Status = 'Active' AND t.TrainingId = @trainningId " +
                    "ORDER BY " +
                    "CASE " +
                    "WHEN u.Department = @priorityDept THEN 1 ELSE 0 END, " +
                    "e.EnrollmentDate;";
                _dataAccessLayer.ExecuteNonQueryData(SQLQUERY, parameters);
                return true;
            }
            finally
            {
                _dataAccessLayer.Dispose();
            }
        }
        public async Task<(string EmployeeEmail, string UserName, string trainningName)> RetrieveParameterToSendMailAsync(int enrollmentId, string action)
        {
            string trainningName = "";
            string UserName = "";
            string EmployeeEmail = "";
            const string SQLQUERY2 = "SELECT E.EnrollementId, T.TrainingName, U.FirstName, U.LastName, U.Email" +
                " FROM  Enrollment E" +
                " INNER JOIN Training_Details T ON E.TrainingId = T.TrainingId" +
                " INNER JOIN User_Details U ON E.UserId = U.UserId" +
                " WHERE E.EnrollementId = @EnrollementId";
            List<SqlParameter> Emailparameters = new List<SqlParameter>();
            Emailparameters.Add(new SqlParameter("@EnrollementId", enrollmentId));
            using (var reader = await _dataAccessLayer.GetData(SQLQUERY2, Emailparameters))
            {
                if (reader.Read())
                {
                    trainningName = reader["TrainingName"].ToString();
                    UserName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                    EmployeeEmail = reader["Email"].ToString();
                }
                reader.Close();
            }
            return (EmployeeEmail, UserName, trainningName);
        }
        private async Task SendEmail(int enrollmentId, string action)
        {
            string trainningName = "";
            string UserName = "";
            string EmployeeEmail = "";
            const string SQLQUERY2 = "SELECT E.EnrollementId, T.TrainingName, U.FirstName, U.LastName, U.Email" +
                " FROM  Enrollment E" +
                " INNER JOIN Training_Details T ON E.TrainingId = T.TrainingId" +
                " INNER JOIN User_Details U ON E.UserId = U.UserId" +
                " WHERE E.EnrollementId = @EnrollementId";
            List<SqlParameter> Emailparameters = new List<SqlParameter>();
            Emailparameters.Add(new SqlParameter("@EnrollementId", enrollmentId));
            using (var reader = await _dataAccessLayer.GetData(SQLQUERY2, Emailparameters))
            {
                if (reader.Read())
                {
                    trainningName = reader["TrainingName"].ToString();
                    UserName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                    EmployeeEmail = reader["Email"].ToString();
                }
                reader.Close();
            }
            string message = "";
            if (action == "approve")
            {
                message = "Hello " + UserName + "your application for the " + trainningName + "has been approved by your manager";
            }
            else
            {
                message = "Hello " + UserName + "your application for the " + trainningName + "has been declined by your manager";
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
            await EmailSender.SendEmailAsync(EmployeeEmail, subject, htmlBody);
        }
    }
}

