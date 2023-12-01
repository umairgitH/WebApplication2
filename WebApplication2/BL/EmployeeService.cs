using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication2.DAL;
using WebApplication2.Models;

namespace WebApplication2.BL
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDAL _employeeDAL;
        
        public EmployeeService(IEmployeeDAL employeeDAL) {

            _employeeDAL = employeeDAL;
        }
        //public DataTable GetUser(User user)
        //{
        //    //we will need to get this from contoller
        //    // user = new User
        //    //{
        //    //    Email = "umair@gmail.com",
        //    //    Password = "12345678"
        //    //};

        //    //then retrieve this data
        //    //DataTable userData = _employeeDAL.GetUser(user);

        //    //then do some check(row>0)
        //    //then validate user (returnToActionLink("employeeHome"))

        //    return _employeeDAL.GetUser(user);
        //}

        public bool GetUser(User user)
        {
            using (SqlDataReader reader = _employeeDAL.GetUser2(user))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HttpContext context =HttpContext.Current;
                        string SessionUserId = (string)(context.Session["UserId"]);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public bool RegisterEmployee(User user)
        {

            return _employeeDAL.RegisterEmployee(user);
            
        }
        public bool Enroll(User user, Trainning trainning, Enrollment enrollment)
        {
            return _employeeDAL.Enroll(user, trainning, enrollment);
        }

        public DataTable DisplayTrainning(Trainning trainning)
        {
            return _employeeDAL.DisplayTrainning(trainning);
        }
    }
}