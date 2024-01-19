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
    public class TrainningDAL : ITrainningDAL
    {
        private readonly IDataAccessLayer _dataAccessLayer;
        public TrainningDAL(IDataAccessLayer dataAcessLayer) { 
            _dataAccessLayer = dataAcessLayer;
        }
        public bool AddTraining(Trainning trainning)
        {
            try
            {
                _dataAccessLayer.Connect();

                string[] parameterToInclude = { "TrainingName", "Description", "Capacity", "PriorityDepartment", "Deadline", "PrerequisiteName" };
                List<SqlParameter> parameters = _dataAccessLayer.PopulateSqlParameter(trainning, parameterToInclude);
                const string SQLQUERY = "INSERT INTO Training_Details(TrainingName, Description, Capacity, PriorityDepartment, Deadline) "
                        + "VALUES(@TrainingName, @Description, @Capacity, @PriorityDepartment, @Deadline)"+
                        " DECLARE @TrainningId INT = SCOPE_IDENTITY();"+
                        " INSERT INTO Trainning_Prerequisite(PrerequisiteName, TrainingId)"+
                        " VALUES(@PrerequisiteName, @TrainningId)";
                _dataAccessLayer.ExecuteNonQueryData(SQLQUERY, parameters);

                return true;
            }
            finally
            {
                _dataAccessLayer.Dispose();
            }
        }
        public async Task <List<Trainning>> DisplayTrainningAsync(int userId)
        {
            _dataAccessLayer.Connect();
            const string SQLQUERY = "select TrainingId, TrainingName, Description, Capacity, Deadline from Training_Details " +
                "where TrainingId not in (select TrainingId from Enrollment where UserId = @UserId)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", userId));
            List<Trainning> employeeTrainningList = new List<Trainning>();
            using (var reader = await _dataAccessLayer.GetData(SQLQUERY, parameters))
            {
                while (reader.Read())
                {
                    Trainning trainning = new Trainning
                    {
                        TrainingId = Convert.ToInt16(reader["TrainingId"]),
                        TrainingName = reader["TrainingName"].ToString(),
                        Description = reader["Description"].ToString(),
                        Capacity = Convert.ToInt32(reader["Capacity"]),
                        Deadline = Convert.ToDateTime(reader["Deadline"])
                    };
                    employeeTrainningList.Add(trainning);
                }
            }
            return employeeTrainningList;
        }
        public bool UpdateTrainnig(Trainning trainning, string[] parameterToInclude, int trainningId)
        {
            try
            {
                _dataAccessLayer.Connect();
                
                List<SqlParameter> parameters =_dataAccessLayer.PopulateSqlParameter(trainning, parameterToInclude);
                StringBuilder setClause = new StringBuilder();
                foreach(var parameter in parameters)
                {
                    setClause.Append($"{parameter.ParameterName.Substring(1)} = {parameter.ParameterName}, ");
                }

                //remove the last "," and space.
                string setClause2 = setClause.ToString().TrimEnd(',' , ' ');
                string SQLQUERY = "UPDATE Training_Details "+
                    "SET " + setClause2 +
                    " WHERE TrainingId = @TrainingId";
                parameters.Add(new SqlParameter("@TrainingId", trainningId));
                _dataAccessLayer.ExecuteNonQueryData(SQLQUERY,parameters);
                return true;
            }
            finally
            {
                _dataAccessLayer.Dispose();
            }
        }
        public List<Trainning> GetTrainningList()
        {
            _dataAccessLayer.Connect();
            const string SQLQUERY = "SELECT TrainingId, TrainingName, Description, Capacity, PriorityDepartment, Deadline" +
                " FROM Training_Details";
            List<Trainning> trainningList = new List<Trainning>();
            using (SqlDataReader reader = _dataAccessLayer.GetAllData(SQLQUERY))
            {
                while(reader.Read())
                {
                    Trainning trainning = new Trainning()
                    {
                        TrainingId = Convert.ToInt16(reader["TrainingId"]),
                        TrainingName = reader["TrainingName"].ToString(),
                        Description = reader["Description"].ToString(),
                        Capacity = Convert.ToInt32(reader["Capacity"]),
                        PriorityDepartment = reader["PriorityDepartment"].ToString(),
                        Deadline = Convert.ToDateTime(reader["Deadline"])
                    };
                    trainningList.Add(trainning);
                }
            }
            return trainningList;
        }
        public async Task <bool> DeleteTrainningAsync(int trainnigId)
        {
            _dataAccessLayer.Connect();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@trainningId", trainnigId));
            const string SQLQUERY = "SELECT * FROM Enrollment WHERE TrainingId =@trainningId AND Status = 'Active' ";
            bool isRecordExists ;
            using(SqlDataReader reader = await _dataAccessLayer.GetData(SQLQUERY, parameters))
            {
                if (reader.Read())
                {
                    isRecordExists = false;
                }else
                {
                    reader.Close();
                    List<SqlParameter> deleteParameters = new List<SqlParameter>();
                    deleteParameters.Add(new SqlParameter("@trainningId", trainnigId));
                    const string DELETEQUERY = "DELETE FROM Trainning_Prerequisite WHERE TrainingId = @trainningId"+
                        " DELETE FROM Training_Details WHERE TrainingId = @trainningId ";
                    _dataAccessLayer.ExecuteNonQueryData(DELETEQUERY, deleteParameters);
                    isRecordExists = true;
                }
            }
            return isRecordExists;
        }
        public List<string> GetAllTrainningName()
        {
            try
            {
                _dataAccessLayer.Connect();
                const string SQLQUERY = "SELECT TrainingName from Training_Details";
                List<string> TrainninNameList = new List<string>();
                using(var reader = _dataAccessLayer.GetAllData(SQLQUERY))
                {
                    while (reader.Read())
                    {
                        TrainninNameList.Add(reader["TrainingName"].ToString());
                    }
                }
                return TrainninNameList;
            }finally{ _dataAccessLayer.Dispose();}
        }
    }
}