using EmployeeTrainningClassLibrary.Enum;
using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace EmployeeTrainningClassLibrary.DAL
{
    public class UserDAL : RetrieveUserForAuthenticationDAL, IUserDAL
    {
        private readonly IRetrieveUserForAuthenticationDAL _retrieveUser;
        public UserDAL(IDataAccessLayer _dataAccessLayer, IRetrieveUserForAuthenticationDAL retrieveUser) : base(_dataAccessLayer)
        {
            _retrieveUser = retrieveUser;
        }
        public bool IsEmployeeRegistered(User user)
        {
            try
            {
                _dataAccessLayer.Connect();

                string[] parameterToInclude = { "FirstName", "LastName", "PhoneNum", "Email", "Password", "ManagerName"};
                List<SqlParameter> parameters = _dataAccessLayer.PopulateSqlParameter(user, parameterToInclude);
                parameters.Add(new SqlParameter("@RoleId", (int)RoleEnum.Employee));

                _dataAccessLayer.ExecuteNonQueryUsingProcedures("RegisterUser", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in registering the data: {ex.Message}");
                return false;
            }
        }
        public bool CheckEmailExistance(string email)
        {

            _dataAccessLayer.Connect();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@isExistsEmail", email));
            const string SQLQUERY = "select * from User_Details where Email=@isExistsEmail";
            //_dataAccessLayer.GetData(SQLQUERY, parameters);

            using(SqlDataReader reader = _dataAccessLayer.GetData(SQLQUERY, parameters))
            {
                if (reader.Read())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }   
        }
        //TODO
        public bool CheckNICExistance(string nic)
        {
            return true;
        }
    }
}