using EmployeeTrainningClassLibrary.DAL;
using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrainningClassLibrary.BusinessLayer
{
    public class TrainningService : ITrainningService
    {
        private readonly ITrainningDAL _trainningDAL;
        public TrainningService(ITrainningDAL trainningDAL)
        {
            _trainningDAL = trainningDAL;
        }

        public bool AddTraining(Trainning trainning)
        {
            return _trainningDAL.AddTraining(trainning);
        }

        public List<Trainning> DisplayTrainning(int userId)
        {
            List<Trainning> employeeTrainningList = new List<Trainning>();
            using (SqlDataReader reader = _trainningDAL.DisplayTrainning(userId))
            {
                while (reader.Read())
                {
                    Trainning trainning = new Trainning
                    {
                        TrainingId = Convert.ToInt16(reader["TrainingId"]),
                        TrainingName = reader["TrainingName"].ToString(),
                        Description = reader["Description"].ToString(),
                        Capacity = Convert.ToInt32(reader["Capacity"])
                    };
                    employeeTrainningList.Add(trainning);
                }               
            }
            return employeeTrainningList;
        }
        //TODO
        public bool UpdateTrainnig(Trainning trainning, string[] parameterToInclude)
        {
            return _trainningDAL.UpdateTrainnig(trainning, parameterToInclude);
        }
    }
}
