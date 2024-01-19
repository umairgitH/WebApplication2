using EmployeeTrainningClassLibrary.Enum;
using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
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
        public async Task<bool> CheckEmailExistanceAsync(string email)
        {
            _dataAccessLayer.Connect();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@isExistsEmail", email));
            const string SQLQUERY = "select * from User_Details where Email=@isExistsEmail";
            //_dataAccessLayer.GetData(SQLQUERY, parameters);
            int count = 0;
            using (SqlDataReader reader = await _dataAccessLayer.GetData(SQLQUERY, parameters))
            {

                while (reader.Read())
                {
                    count++;
                }
            }
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task <bool> CheckNICExistanceAsync(string nic)
        {
            _dataAccessLayer.Connect();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@isExistsNIC", nic));
            const string SQLQUERY = "select * from User_Details where NIC=@isExistsNIC";
            int count = 0;
            using (SqlDataReader reader =await _dataAccessLayer.GetData(SQLQUERY, parameters))
            {

                while (reader.Read())
                {
                    count++;
                }
            }
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }        
        }
        public async Task <(bool, List<string>)> IsEmployeeRegisteredAsync(User user)
        {
            try
            {
                _dataAccessLayer.Connect();
                List<string> errorList = new List<string>();

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                parameters.Add(new SqlParameter("@LastName", user.LastName));
                parameters.Add(new SqlParameter("@PhoneNum", user.PhoneNum));
                parameters.Add(new SqlParameter("@Email", user.Email));
                parameters.Add(new SqlParameter("@Password", user.Password));
                parameters.Add(new SqlParameter("@ManagerName", user.ManagerName));
                parameters.Add(new SqlParameter("@NIC", user.NIC));
                parameters.Add(new SqlParameter("@RoleId", (int)RoleEnum.Employee));

                const string SQLQUERY = "DECLARE @ManagerId INT;" +
                    "SET @ManagerId = (select UserId FROM User_Details WHERE FirstName=@ManagerName);" +
                    "DECLARE @Department varchar(64);" +
                    "SET @Department = (select Department from User_Details WHERE FirstName=@ManagerName);" +
                    "INSERT INTO User_Details (FirstName, LastName, phoneNum, Email, Password, ManagerId, Department, NIC)" +
                    "VALUES (@FirstName, @LastName, @phoneNum, @Email, @Password, @ManagerId, @Department, @NIC);" +
                    "DECLARE @UserId INT = SCOPE_IDENTITY();" +
                    "INSERT INTO User_Role (RoleId, UserId)" +
                    "VALUES (@RoleId, @UserId);";

                if (!await CheckEmailExistanceAsync(user.Email))
                {
                    errorList.Add("emailNotUnique");
                }
                if (!await CheckNICExistanceAsync(user.NIC))
                {
                    errorList.Add("NicNotUnique");
                }
                if (errorList.Count > 0)
                {
                    return (false, errorList);
                }
                else
                {
                    _dataAccessLayer.ExecuteNonQueryData(SQLQUERY, parameters);
                    return (true, errorList);
                }
            }finally { _dataAccessLayer.Dispose(); }  
        }
        public List<string> ListOfManagerName()
        {
            try
            {
                _dataAccessLayer.Connect();
                const string SQLQUERY = "SELECT FirstName" +
                    " FROM User_Details u " +
                    " INNER JOIN User_Role ur " +
                    " ON u.UserId = ur.UserId" +
                    " WHERE ur.RoleId =0";
                List<string> ManagerNameList = new List<string>();
                using(var reader = _dataAccessLayer.GetAllData(SQLQUERY))
                {
                    while (reader.Read())
                    {
                        ManagerNameList.Add(reader["FirstName"].ToString());
                    } 
                }
                return ManagerNameList;
            }
            finally { _dataAccessLayer.Dispose(); }
        }
    }
}
