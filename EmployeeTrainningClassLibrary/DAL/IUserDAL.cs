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
        Task<bool> CheckEmailExistanceAsync(string email);
        Task <bool> CheckNICExistanceAsync(string nic);
        Task <(bool, List<string>)> IsEmployeeRegisteredAsync(User user);
        List<string> ListOfManagerName();
    }
}
