using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NORM.M1.DbCommand.Helpers
{
    public static class DataTableExtensions
    {
        public static IList<T> ToList<T>(this DataTable table) where T :class, new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            return (from object row in table.Rows select CreateItemFromRow<T>((DataRow) row, properties)).ToList();
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : class, new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                property.SetValue(item, row[property.Name], null);
            }
            return item;
        }
    }
}
