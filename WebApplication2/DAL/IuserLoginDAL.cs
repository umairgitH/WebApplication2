using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.DAL
{
    public interface IuserLoginDAL
    {
        DataTable GetUser(User user);
        SqlDataReader GetUser2(User user);
    }
}
