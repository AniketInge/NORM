using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NormAttribute :Attribute
    {
        public string ColumnName { get; set; }
        public NormAttribute()
        {
        }
    }
}
