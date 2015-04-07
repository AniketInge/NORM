using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NORM.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class NormEntityAttribute :Attribute
    {
        private string _connection;
        public string ConnectionString { get; set; }

        public string Connection
        {
            get { return _connection; }
            set
            {
                _connection = value;
                ConnectionString = ConfigurationManager.ConnectionStrings[_connection].ConnectionString;
            }
        }
    }
}
