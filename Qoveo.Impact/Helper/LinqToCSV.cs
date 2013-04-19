using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Qoveo.Impact.Helper
{
    /// <summary>
    /// Class that rendering anything IEnumerable to a CSV formated string
    /// </summary>
    public static class LinqToCSV
    {
        /// <summary>
        /// Method that rendering anything IEnumerable to a CSV formated string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string ToCsv<T>(this IEnumerable<T> items)
            where T : class
        {
            var csvBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();
            
            // First Line is header row
            string header = string.Join(";", properties.Select(a => a.Name.ToCsvValue()).ToArray());
            csvBuilder.AppendLine(header);

            foreach (T item in items)
            {
                string line = string.Join(";", properties.Select(p => p.GetValue(item, null).ToCsvValue()).ToArray());
                csvBuilder.AppendLine(line);
            }
            return csvBuilder.ToString();
        }

        private static string ToCsvValue<T>(this T item)
        {
            if (item == null) return "\"\"";

            if (item is string)
            {
                return string.Format("\"{0}\"", item.ToString().Replace("\"", "\\\""));
            }
            double dummy;
            if (double.TryParse(item.ToString(), out dummy))
            {
                return string.Format("{0}", item);
            }
            return string.Format("\"{0}\"", item);
        }
    }
}