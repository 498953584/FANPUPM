namespace cn.justwin.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    /// <summary>
    /// CSV格式帮助类
    /// </summary>
    public static class CsvHelper
    {
        /// <summary>
        /// 把集合转换为CSV格式
        /// </summary>
        /// <param name="lst">字符串集合</param>
        /// <returns>CSV格式</returns>
        public static string ToCsv(this IEnumerable<string> lst)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in lst)
            {
                builder.Append(",").Append(str);
            }
            if (builder.Length > 0)
            {
                builder.Remove(0, 1);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 将CSV格式转换为字符串集合
        /// </summary>
        /// <param name="csv">CSV格式的字符串</param>
        /// <returns>字符串集合</returns>
        public static IList<string> ToList(string csv)
        {
            return (from s in csv.Split(new char[] { ',' })
                where s.Length > 0
                select s).ToList<string>();
        }
    }
}

