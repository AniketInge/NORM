using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace NORM
{
    public class NormDbConnection 
    {
        private string ApplicationName { get; set; }
        private string ConnectionString { get; set; }
        private SqlConnectionStringBuilder sb { get; set; }

        [Import]
        private INormDbCommand _currentDbCommand;

        private void Compose()
        {

            var catalog = new DirectoryCatalog(".","*.dll");
            var container = new CompositionContainer(catalog);
            container.SatisfyImportsOnce(this);
        }
        public NormDbConnection()
        {
            var connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            if(connStr == null) throw new ConfigurationErrorsException("Connection string was not supplied to NORM & NORM could" +
                                                                       " not find a connection string named 'DefaultConnection' in "+ AppDomain.CurrentDomain.SetupInformation.ConfigurationFile); 
            ApplicationName = "NormApplication";
            sb = new SqlConnectionStringBuilder(connStr);
            sb.ApplicationName = sb.ApplicationName??ApplicationName;
            ConnectionString = sb.ToString();

            Compose();
        }
        public NormDbConnection(string connectionString)
        {
            ApplicationName = "NormApplication";
            sb = new SqlConnectionStringBuilder(connectionString);
            sb.ApplicationName = sb.ApplicationName ?? ApplicationName;
            ConnectionString = sb.ToString();

            Compose();
        }

        public NormDbConnection(string connectionString, string applicationName)
        {
            ApplicationName = applicationName;
            sb = new SqlConnectionStringBuilder(connectionString);
            sb.ApplicationName = ApplicationName;
            ConnectionString = sb.ToString();

            Compose();
        }

        public INormDbCommand Current
        {
            get {
                _currentDbCommand.ConnectionString = ConnectionString;
                return _currentDbCommand;
            }
        }
    }
}
