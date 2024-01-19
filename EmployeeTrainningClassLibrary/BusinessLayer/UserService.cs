using EmployeeTrainningClassLibrary.DAL;
using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeTrainningClassLibrary.BusinessLayer
{
    public class UserService : IUserService
    {
        private readonly IUserDAL _employeeDAL;
        
        public UserService(IUserDAL employeeDAL) {

            _employeeDAL = employeeDAL;
        }
        //Tuple Literals 
        public async Task <(bool isAuthenticated, int UserId, int RoleId)> IsUserExistsAsync(LoginModel user)
        {
            (bool isAuthenticated, int UserId, int RoleId) = await _employeeDAL.RetrieveUserAsync(user);
            if (isAuthenticated)
            {
                return (isAuthenticated: true, UserId, RoleId);
            }
            else
            {
                return (isAuthenticated: false, UserId: 0, RoleId: 0);
            }
        }
        public async Task <(bool, List<string>)> IsEmployeeRegisteredAsync(User user)
        {
            user.Password = GetHashedPassword(user.Password);
            return await _employeeDAL.IsEmployeeRegisteredAsync(user);
        }
        public List<string> ListOfManagerName()
        {
            return _employeeDAL.ListOfManagerName();
        }
        private string GetHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 11);
        }
    }
}