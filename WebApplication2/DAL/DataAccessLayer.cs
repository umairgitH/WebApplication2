using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication2.DAL
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

        public void DisConnect()
        {
            if (Connection != null && Connection.State.Equals(ConnectionState.Open))
            {
                Connection.Close();
            }
        }

        public DataTable GetData(string sql, List<SqlParameter> parameters)
        {
            DataTable dt = null;

            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {
                cmd.Parameters.AddRange(parameters.ToArray());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
        }
        public SqlDataReader GetData2(string sql, List<SqlParameter> parameters)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, Connection))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    return cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in retrieving data and {ex.Message}");
                return null;
            }
        }
        public void ExecuteNonQueryData(string sql,List<SqlParameter> parameters)
        {
            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {
                if (parameters != null && parameters.Count > 0)
                {
                    cmd.Parameters.AddRange (parameters.ToArray());
                }
                //used to insert update or delete ->  returns the number of rows affected by th query
                cmd.ExecuteNonQuery();
            }
        }
        public void ExecuteNonQueryUsingProcedures(string procedure, List<SqlParameter> parameters)
        {
            using (SqlCommand command = new SqlCommand(procedure, Connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
                if(parameters != null && parameters.Count > 0)
                {
                    command.Parameters.AddRange (parameters.ToArray());
                }
                command.ExecuteNonQuery();
            }
        }
    }
}