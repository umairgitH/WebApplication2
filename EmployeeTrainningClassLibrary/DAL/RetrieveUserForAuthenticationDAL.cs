using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
        public SqlDataReader RetrieveUser(User user)
        {
            _dataAccessLayer.Connect();

            string[] propertyToInclude = { "Email", "Password" };
            List<SqlParameter> parameters = _dataAccessLayer.PopulateSqlParameter(user, propertyToInclude);

            const string SQL_QUERY = "select u.Email, u.Password, u.UserId, r.RoleId "
                + "from User_Details u"
                + " Inner Join User_Role r "
                + "ON u.UserId=r.UserId "
                + "where u.Email=@Email and u.Password=@Password ";

            return _dataAccessLayer.GetData(SQL_QUERY, parameters);
        }
    }
}
