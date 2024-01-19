using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeTrainningClassLibrary.DAL
{
    public class RetrieveUserForAuthenticationDAL : IRetrieveUserForAuthenticationDAL
    {
        protected IDataAccessLayer _dataAccessLayer;
        public RetrieveUserForAuthenticationDAL(IDataAccessLayer layer)
        {
            _dataAccessLayer = layer;
        }
        public async Task<(bool isAuthenticated, int UserId, int RoleId)> RetrieveUserAsync(LoginModel user)
        {
            //int c = 1 / int.Parse("0");
            _dataAccessLayer.Connect();
            string[] propertyToInclude = { "Email", "Password" };
            List<SqlParameter> parameters = _dataAccessLayer.PopulateSqlParameter(user, propertyToInclude);
            const string SQL_QUERY = "select u.Email, u.Password, u.UserId, r.RoleId "
                                    + "from User_Details u"
                                    + " Inner Join User_Role r "
                                    + "ON u.UserId=r.UserId "
                                    + "where u.Email=@Email and u.Password=@Password ";

            using (var reader = await _dataAccessLayer.GetData(SQL_QUERY, parameters))
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
        //public async Task<(bool isAuthenticated, int UserId, int RoleId)> RetrieveUserAsync(LoginModel user)
        //{
        //    //int c = 1 / int.Parse("0");
        //    _dataAccessLayer.Connect();
        //    List<SqlParameter> parameters = new List<SqlParameter>();
        //    parameters.Add(new SqlParameter("@Email", user.Email));
        //    const string SQL_QUERY = "select u.Email, u.Password, u.UserId, r.RoleId "
        //                            + "from User_Details u"
        //                            + " Inner Join User_Role r "
        //                            + "ON u.UserId=r.UserId "
        //                            + "where u.Email=@Email";

        //    using (var reader = await _dataAccessLayer.GetData(SQL_QUERY, parameters))
        //    {
        //        if (reader.HasRows)
        //        {
        //            reader.Read();
        //            string storedPassword = reader["Password"].ToString();
        //            if (IsPasswordValid(user.Password, storedPassword))
        //            {
        //                int UserId = Convert.ToInt32(reader["UserId"]);
        //                int RoleId = Convert.ToInt32(reader["RoleId"]);
        //                return (isAuthenticated: true, UserId, RoleId);
        //            }
        //            else { return (isAuthenticated: false, UserId: 0, RoleId: 0); }
        //        }
        //        else
        //        {
        //            return (isAuthenticated: false, UserId: 0, RoleId: 0);
        //        }
        //    }

        //}
        //private bool IsPasswordValid(string inputPassword, string storedPassword)
        //{
        //    return BCrypt.Net.BCrypt.EnhancedVerify(inputPassword, storedPassword);
        //}
    }
}


