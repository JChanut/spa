using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Qoveo.Impact.Helper
{
    /// <summary>
    /// Class that convert anything IEnumerable to a DataTable
    /// </summary>
    public static class LinqToDataTable
    {
        /// <summary>
        /// Method that convert anything IEnumerable to a DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            var props = typeof(T).GetProperties();

            var dt = new DataTable();

            dt.Columns.AddRange(
                props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
                );

            items.ToList().ForEach(
                i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
                );

            return dt;
        }
    }
}