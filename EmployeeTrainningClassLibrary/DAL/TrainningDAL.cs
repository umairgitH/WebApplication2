using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

                string[] parameterToInclude = { "TrainingName", "Description", "Capacity", "PriorityDepartment" };
                List<SqlParameter> parameters = _dataAccessLayer.PopulateSqlParameter(trainning, parameterToInclude);
                const string SQLQUERY = "insert into Trainning_Details "
                        + "values(@Description,@Capacity, @PriorityDepartment)";
                _dataAccessLayer.ExecuteNonQueryData(SQLQUERY, parameters);

                return true;
            }
            catch
            {
                return false;
            }

        }

        public SqlDataReader DisplayTrainning(int userId)
        {
            _dataAccessLayer.Connect();
            const string SQLQUERY = "select TrainingId, TrainingName, Description, Capacity from Training_Details " +
                "where TrainingId not in (select TrainingId from Enrollment where UserId = @UserId)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", userId));

            return _dataAccessLayer.GetData(SQLQUERY, parameters);
        }
        //TODO
        public bool UpdateTrainnig(Trainning trainning, string[] parameterToInclude)
        {
            try
            {
                _dataAccessLayer.Connect();
                
                List<SqlParameter> parameters =_dataAccessLayer.PopulateSqlParameter(trainning, parameterToInclude);
                const string SQLQUERY = "";
                _dataAccessLayer.ExecuteNonQueryData(SQLQUERY,parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}