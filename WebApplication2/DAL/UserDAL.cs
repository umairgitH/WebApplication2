using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.DAL
{
    public  class UserDAL : IuserLoginDAL
    {
        protected IDataAccessLayer _layer;
        public UserDAL(IDataAccessLayer layer)
        { 
            _layer = layer;
        }

        public SqlDataReader GetUser2(User user)
        {
            try
            {
                _layer.Connect();


                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Email", user.Email),
                    new SqlParameter("@Password", user.Password),
                    new SqlParameter("@UserId", user.UserId)
                };

                string sqlQuery1 = "select u.Email, u.Password, r.UserId, r.RoleId "
                    + "from User_Details u"
                    + " Inner Join User_Role r "
                    + "ON u.UserId=r.UserId "
                    + "where and (u.Email=@Email and u.Password=@Password)";

                //string sqlQuery = "select Email, Password from User_Details" +
                //    " where Email=@Email and Password=@Password";



                return _layer.GetData2(sqlQuery1, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in retrieving the data: {ex.Message}");
                return null;
            }
            finally
            {
                _layer.DisConnect();
            }

        }

        public DataTable GetUser(User user)
        {
            try
            {
                _layer.Connect();


                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Email", user.Email),
                    new SqlParameter("@Password", user.Password),
                    new SqlParameter("@UserId", user.UserId)
                };

                string sqlQuery1 = "select u.Email, u.Password, r.RoleId " 
                    + "from User_Details u"
                    + " Inner Join User_Role r "
                    + "ON u.UserId=r.UserId "
                    + "where and (u.Email=@Email and u.Password=@Password)";

                //string sqlQuery = "select Email, Password from User_Details" +
                //    " where Email=@Email and Password=@Password";

              

                return _layer.GetData(sqlQuery1, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine ($"Error in retrieving the data: {ex.Message}");
                return null;
            }
            finally
            {
                _layer.DisConnect();
            }
           

        }
           
    }
}
