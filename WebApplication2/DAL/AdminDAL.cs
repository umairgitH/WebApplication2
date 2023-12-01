using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.DAL
{
    public class AdminDAL : UserDAL, IAdminDAL
    {
        private IuserLoginDAL loginDAL;
        public AdminDAL(IDataAccessLayer _layer, IuserLoginDAL iuserLogin) : base(_layer)
        {

            loginDAL = iuserLogin;
        }
        public DataTable GetTrainnig(Trainning trainning)
        {
            try
            {
                _layer.Connect();

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Description", trainning.Description),
                    new SqlParameter("@Capacity", trainning.Capacity),
                    new SqlParameter("@PriorityDepartment", trainning.PriorityDepartment)
                };

                string sqlQuery = "select Description, Capacity, PriorityDepartment from Trainning_Details ";
                sqlQuery += " where Description= @Description and Capacity=@Capacity and PriorityDepartment= @PriorityDepartment ";

                return _layer.GetData(sqlQuery, parameters);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in retrieving the trainning: {ex.Message}");
                DataTable dt = null;
                return dt;
                
            }
            finally
            {
                _layer.DisConnect();
            }
        }
        public bool AddTrainnig(Trainning trainning)
        {
            try
            {
                _layer.Connect();

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Description", trainning.Description),
                    new SqlParameter("@Capacity", trainning.Capacity),
                    new SqlParameter("@PriorityDepartment", trainning.PriorityDepartment)
                };

                string sqlQuery = "insert into Trainning_Details "
                    + "values(@Description,@Capacity, @PriorityDepartment)";

                _layer.ExecuteNonQueryData(sqlQuery, parameters);

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine( "Erroe in inserting new trainnig" + ex.ToString());
                return false;
            }
            finally
            {
                _layer.DisConnect();
            }
        }
        public bool UpdateTrainnig(Trainning trainning)
        {
            try
            {
                _layer.Connect();

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Description", trainning.Description),
                    new SqlParameter("@Capacity", trainning.Capacity),
                    new SqlParameter("@PriorityDepartment", trainning.PriorityDepartment)
                };

                string sqlQuery = "update [Trainning_Details] "
                    + "set Description =@Description, Capacity=@Capacity, PriorityDepartment=@PriorityDepartment";

                _layer.ExecuteNonQueryData(sqlQuery, parameters);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in updating new trainnig" + ex.ToString());
                return false;
            }
            finally
            {
                _layer.DisConnect();
            }
        }

        //TODO
        public DataTable EmployeeSelection(User user, Trainning trainning, Enrollment enrollment)
        {
            throw new NotImplementedException();
        }

    }
}