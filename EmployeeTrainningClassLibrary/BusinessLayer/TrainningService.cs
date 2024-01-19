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
        public async Task <List<Trainning>> DisplayTrainningAsync(int userId)
        {
            return await _trainningDAL.DisplayTrainningAsync(userId);
        }
        public bool UpdateTrainnig(Trainning trainning, string[] parameterToInclude, int trainningId)
        {
            return _trainningDAL.UpdateTrainnig(trainning, parameterToInclude, trainningId);
        }
        public List<Trainning> GetTrainningList()
        {
            return _trainningDAL.GetTrainningList();
        }
        public async Task <bool> DeleteTrainningAsync(int trainnigId)
        {
            return await _trainningDAL.DeleteTrainningAsync(trainnigId);
        }

        public List<string> GetAllTrainningName()
        {
            return _trainningDAL.GetAllTrainningName();
        }
    }
}
