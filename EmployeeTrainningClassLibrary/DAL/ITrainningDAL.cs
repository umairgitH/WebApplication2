using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrainningClassLibrary.DAL
{
    public interface ITrainningDAL
    {
        Task <List<Trainning>> DisplayTrainningAsync(int userId);
        bool AddTraining(Trainning trainning);
        bool UpdateTrainnig(Trainning trainning, string[] parameterToInclude, int trainningId);
        List<Trainning> GetTrainningList();
        Task <bool> DeleteTrainningAsync(int trainnigId);
        List<string> GetAllTrainningName();
    }
}
