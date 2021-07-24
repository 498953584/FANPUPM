namespace com.jwsoft.pm.data
{
    using System;
    using System.Collections;

    public class SqlStringConstructor
    {
        public static string GetConditionClause(Hashtable queryItems)
        {
            int num = 0;
            string str = "";
            foreach (DictionaryEntry entry in queryItems)
            {
                if (num == 0)
                {
                    str = " Where ";
                }
                else
                {
                    str = str + " And ";
                }
                if ((entry.Value.GetType().ToString() == "System.String") || (entry.Value.GetType().ToString() == "System.DateTime"))
                {
                    str = str + entry.Key.ToString() + " Like " + GetQuotedString("%" + entry.Value.ToString() + "%");
                }
                else
                {
                    str = str + entry.Key.ToString() + "= " + entry.Value.ToString();
                }
                num++;
            }
            return str;
        }

        public static string GetConditionClause(Hashtable queryItems, string type)
        {
            int num = 0;
            string str = "";
            foreach (DictionaryEntry entry in queryItems)
            {
                if (num == 0)
                {
                    str = " Where ";
                }
                else
                {
                    str = str + " " + type + " ";
                }
                if ((entry.Value.GetType().ToString() == "System.String") || (entry.Value.GetType().ToString() == "System.DateTime"))
                {
                    str = str + entry.Key.ToString() + " Like " + GetQuotedString("%" + entry.Value.ToString() + "%");
                }
                else
                {
                    str = str + entry.Key.ToString() + "= " + entry.Value.ToString();
                }
                num++;
            }
            return str;
        }

        public static string GetQuotedString(string pStr)
        {
            return ("'" + pStr.Replace("'", "''") + "'");
        }
    }
}

