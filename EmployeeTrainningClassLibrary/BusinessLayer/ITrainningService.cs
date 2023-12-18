using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrainningClassLibrary.BusinessLayer
{
    public interface ITrainningService
    {
        List<Trainning> DisplayTrainning(int userId);
        bool AddTraining(Trainning trainning);
        bool UpdateTrainnig(Trainning trainning, string[] parameterToInclude);
    }
}
