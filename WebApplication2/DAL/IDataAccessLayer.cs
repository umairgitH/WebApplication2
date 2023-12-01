using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2
{
    public interface IDataAccessLayer
    {
        string Connect();
        string Connect(string connectionString);
        void DisConnect();
        DataTable GetData(string sql, List<SqlParameter> parameters);
        SqlDataReader GetData2(string sql, List<SqlParameter> parameters);
        void ExecuteNonQueryData(string sql, List<SqlParameter> parameters);
        void ExecuteNonQueryUsingProcedures(string procedure, List<SqlParameter> parameters);
    }
}
