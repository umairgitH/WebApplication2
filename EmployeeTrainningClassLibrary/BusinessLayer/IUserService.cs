using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrainningClassLibrary.BusinessLayer
{
    public interface IUserService
    {
        bool IsEmployeeRegistered(User user);
        (bool isAuthenticated, int UserId, int RoleId) IsUserExists(User user);
        bool IsEmailUnique(string email);

    }
}
