using EmployeeTrainningClassLibrary.DAL;
using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeTrainningClassLibrary.BusinessLayer
{
    public class UserService : IUserService
    {
        private readonly IUserDAL _employeeDAL;
        
        public UserService(IUserDAL employeeDAL) {

            _employeeDAL = employeeDAL;
        }
        //Tuple Literals to Return Multiple Values
        public (bool isAuthenticated, int UserId, int RoleId) IsUserExists(User user)
        {
            using(SqlDataReader reader = _employeeDAL.RetrieveUser(user))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    int UserId = Convert.ToInt32(reader["UserId"]);
                    int RoleId = Convert.ToInt32(reader["RoleId"]);
                    return (isAuthenticated: true, UserId, RoleId);
                }
                else
                {
                    return (isAuthenticated: false, UserId: 0, RoleId: 0);
                }
            }
        }
        public bool IsEmployeeRegistered(User user)
        {
            return _employeeDAL.IsEmployeeRegistered(user);      
        }
        public bool IsEmailUnique(string email)
        {
           return _employeeDAL.CheckEmailExistance(email);
        }
    }
}