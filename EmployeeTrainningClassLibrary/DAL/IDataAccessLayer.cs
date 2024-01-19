using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTrainningClassLibrary.DAL
{
    public interface IDataAccessLayer : IDisposable
    {
        string Connect();
        string Connect(string connectionString);
        Task <SqlDataReader> GetData(string sql, List<SqlParameter> parameters);
        SqlDataReader GetAllData(string sql);
        void ExecuteNonQueryData(string sql, List<SqlParameter> parameters);
        void ExecuteNonQueryUsingProcedures(string procedure, List<SqlParameter> parameters);
        List<SqlParameter> PopulateSqlParameter(Object obj, string[] includedParameters);
    }
}
