using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeTrainningClassLibrary.DAL
{
    public interface IUserDAL : IRetrieveUserForAuthenticationDAL
    {
        bool IsEmployeeRegistered(User user);
        bool CheckEmailExistance(string email);
    }
}
