using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication2.Enum;
using WebApplication2.Models;

namespace WebApplication2.DAL
{
    //public class EmployeeDAL : UserDAL, IEmployeeDAL
    //private IuserLoginDAL loginDAL;
    //public EmployeeDAL(IDataAccessLayer _layer, IuserLoginDAL iuserLogin) : base(_layer)
    //{

    //    loginDAL = iuserLogin;
    //}

    public class EmployeeDAL : UserDAL, IEmployeeDAL
    {
        private IuserLoginDAL loginDAL;
        public EmployeeDAL(IDataAccessLayer _layer, IuserLoginDAL iuserLogin):base(_layer) {

            loginDAL = iuserLogin;
        }
      
        public bool RegisterEmployee(User user)
        {
            
            try{
                _layer.Connect();

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                  new SqlParameter("@FirstName", user.FirstName),
                  new SqlParameter("@LastName", user.LastName),
                  new SqlParameter("@PhoneNum", user.PhoneNum),
                  new SqlParameter("@Email", user.Email),
                  new SqlParameter("@Password", user.Password),
                  new SqlParameter("@ManagerName", user.ManagerName),
                  new SqlParameter("@RoleId", (int) RoleEnum.Employee)
                };
                _layer.ExecuteNonQueryUsingProcedures("RegisterUser", parameters);
                
                //_layer.ExecuteNonQueryData("RegisterUser", parameters);

                return true;

            }

            catch(Exception ex){

                Console.WriteLine($"Error in registering the data: {ex.Message}");
                return false;
            }
            finally{
                _layer.DisConnect();
            }
           
        }

        //TO UPDATE
        public bool Enroll(User user, Trainning trainning, Enrollment enrollment)
        {
            try
            {
                _layer.Connect();

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@EnrollmentDate", enrollment.EnrollmentDate),
                    new SqlParameter("@NIC", enrollment.NIC),
                    new SqlParameter("@Department", enrollment.Department),
                    new SqlParameter("@DocumentPath", enrollment.DocumentPath),
                    new SqlParameter("@TrainingId", trainning.TrainingId),
                    new SqlParameter("@UserId",user.UserId)
                };

                //state name of cloumn to enter in table
                string sqlQuery = " insert into Enrollment ";
                sqlQuery += "values(@EnrollmentDate, @NIC, @Department, @DocumentPath, @TrainingId, @UserId)";

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in enrolling the employee: {ex.Message}");
                return false;
            }
            finally
            {
                _layer.DisConnect( );   
            }
        }

        //TO IMPLEMENT
        public DataTable DisplayTrainning(Trainning trainning)
        {
            return _layer.GetData("", new List<SqlParameter>());     
        }

    }
}

