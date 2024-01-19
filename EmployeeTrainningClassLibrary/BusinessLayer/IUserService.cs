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
        Task <(bool isAuthenticated, int UserId, int RoleId)> IsUserExistsAsync(LoginModel user);
        Task <(bool, List<string>)> IsEmployeeRegisteredAsync(User user);
        List<string> ListOfManagerName();
    }
}
