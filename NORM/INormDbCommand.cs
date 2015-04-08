using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NORM
{
    public interface INormDbCommand
    {
        int ExecuteNonQuery(string command, object parameters);
        T ExecuteQuery<T>(string command, object parameters) where T : class, new();
        T ExecuteStoredProc<T>(string procName, object parameters) where T : class, new();

        IList<T> ListExecuteQuery<T>(string command, object parameters) where T :class, new();
        IList<T> ListExecuteStoredProc<T>(string procName, object parameters) where T : class, new();

        DataTable ExecuteQuery_DT<T>(string command, object parameters) where T : class, new();

        DataTable ExecuteStoredProc_DT<T>(string procName, object parameters) where T : class, new(); 
        string ConnectionString { get; set; }
    }
}
