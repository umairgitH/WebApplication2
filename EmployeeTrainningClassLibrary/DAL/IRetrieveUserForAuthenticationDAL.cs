using EmployeeTrainningClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeTrainningClassLibrary.DAL
{
    public interface IRetrieveUserForAuthenticationDAL
    {
        SqlDataReader RetrieveUser(User user);
    }
}