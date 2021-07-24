namespace cn.justwin.Web
{
    using System;
    using System.Text;

    public static class StringHelper
    {
        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterSpecial(string str)
        {
            string[] strArray = new string[] { 
                "'", "<", ">", "%", "\"\"", ",", ".", ">=", "=<", "-", "_", ";", "||", "[", "]", "&", 
                "/", "-", "|", " "
             };
            StringBuilder builder = new StringBuilder(str);
            foreach (string str2 in strArray)
            {
                builder.Replace(str2, string.Empty);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 根据数据转换为数据库in()指定的格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetArrayToInStr(string[] str)
        {
            string str2 = "";
            foreach (string str3 in str)
            {
                str2 = str2 + string.Format("'{0}',", str3);
            }
            if (str2.Length > 1)
            {
                return str2.Substring(0, str2.Length - 1);
            }
            return "''";
        }
    }
}

