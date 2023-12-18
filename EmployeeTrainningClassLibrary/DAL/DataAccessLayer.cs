using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Reflection;
using EmployeeTrainningClassLibrary.Enum;

namespace EmployeeTrainningClassLibrary.DAL

{
    public class DataAccessLayer : IDataAccessLayer
    {
        public SqlConnection Connection { get; private set; }
        public string Connect()
        {
            var connectionString = ConfigurationManager.AppSettings["connectionString"];
            if (!string.IsNullOrEmpty(connectionString))
            {
                Connection = new SqlConnection(connectionString);
                Connection.Open();
                return "DB Connect: OK";
            }
            else
            {
                return "cannot connect... find connection string";
            }
        }
        public string Connect(string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                Connection = new SqlConnection(connectionString);
                Connection.Open();
                return "DB Connect: OK";
            }
            else
            {
                return "Unable to find the connection string ";
            }
        }
        public void Dispose()
        {
            if (Connection != null && Connection.State.Equals(ConnectionState.Open))
            {
                Connection.Close();
            }
        }
        public SqlDataReader GetAllData(string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {
                return cmd.ExecuteReader();
            }
        }
        public SqlDataReader GetData(string sql, List<SqlParameter> parameters)
        {
            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {
                cmd.Parameters.AddRange(parameters.ToArray());
                return cmd.ExecuteReader();
            }
        }
        public void ExecuteNonQueryData(string sql, List<SqlParameter> parameters)
        {
            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {
                if (parameters != null && parameters.Count > 0)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }
                cmd.ExecuteNonQuery();
            }
        }
        public void ExecuteNonQueryUsingProcedures(string procedureName, List<SqlParameter> parameters)
        {
            using (SqlCommand command = new SqlCommand(procedureName, Connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null && parameters.Count > 0)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }
                command.ExecuteNonQuery();
            }
        }
        public List<SqlParameter> PopulateSqlParameter(Object obj, string[] includedParameters)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (string propertyName in includedParameters)
            {
                PropertyInfo property = obj.GetType().GetProperty(propertyName);
                if (property != null)
                {
                    parameters.Add(new SqlParameter($"@{property.Name}", property.GetValue(obj)));
                }
            }
            return parameters;
        }

    }
}