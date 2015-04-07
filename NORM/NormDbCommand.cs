using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NORM.M1.DbCommand.Helpers;

namespace NORM.M1.DbCommand
{
    [Export(typeof(NORM.INormDbCommand))]
    public class NormSqlServerDbCommand : INormDbCommand
    {
        public NormSqlServerDbCommand() { }

        /// <summary>
        /// Executes a command and returns the number of rows affected. 
        /// </summary>
        /// <param name="command">SQL Command</param>
        /// <param name="parameters">Parameters for SQL Command</param>
        /// <returns>Number of rows affected.</returns>
        public int ExecuteNonQuery(string command, object parameters)
        {
            var numberOfRowsAffected = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(command))
                {
                    cmd.CommandTimeout = int.MaxValue;

                    var properties = parameters.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                    }
                    connection.Open();
                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return numberOfRowsAffected;
        }

        /// <summary>
        /// Executes command and returns the first row from the result set - or default(T)
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="command">SQL Command</param>
        /// <param name="parameters">Parameters for SQL Command</param>
        /// <returns>First row from result set or default(T)</returns>
        public T ExecuteQuery<T>(string command, object parameters) where T : class, new()
        {
            var dt = new DataTable();
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(command, connection))
                {
                    cmd.CommandTimeout = int.MaxValue;

                    var properties = parameters.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        cmd.Parameters.AddWithValue("@"+property.Name, property.GetValue(parameters));
                    }
                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
            }

            var data = dt.ToList<T>();
            return data.FirstOrDefault();
        }

        /// <summary>
        /// Executes a stored procedure and returns the first row of the result set - or default(T)
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="procName">Stored Procedure Name</param>
        /// <param name="parameters">Parameters for the stored procedure</param>
        /// <returns>First row from result set or default(T)</returns>
        public T ExecuteStoredProc<T>(string procName, object parameters) where T :class, new()
        {
            var dt = new DataTable();
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(procName, connection))
                {
                    cmd.CommandTimeout = int.MaxValue;
                    cmd.CommandType = CommandType.StoredProcedure;

                    var properties = parameters.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                    }
                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
            }

            var data = dt.ToList<T>();
            return data.FirstOrDefault();
        }

        /// <summary>
        /// Executes a query and returns the list of T
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="command">SQL Command</param>
        /// <param name="parameters">Parameters to the SQL Command</param>
        /// <returns>List of T</returns>
        public IList<T> ListExecuteQuery<T>(string command, object parameters) where T : class, new()
        {
            var dt = new DataTable();
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(command, connection))
                {
                    cmd.CommandTimeout = int.MaxValue;

                    var properties = parameters.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                    }
                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
            }

            var data = dt.ToList<T>();
            return data;
        }

        /// <summary>
        /// Executes a stored procedure and returns a list of T
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="procName">Stored Procedure Name</param>
        /// <param name="parameters">Parameters for the stored procedure</param>
        /// <returns>List of T</returns>
        public IList<T> ListExecuteStoredProc<T>(string procName, object parameters) where T :class, new()
        {
            var dt = new DataTable();
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(procName, connection))
                {
                    cmd.CommandTimeout = int.MaxValue;
                    cmd.CommandType = CommandType.StoredProcedure;

                    var properties = parameters.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                    }
                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
            }

            var data = dt.ToList<T>();
            return data;
        }

        public string ConnectionString { get; set; }
    }
}
